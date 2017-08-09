using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySorter
{

	// Types: Placable, Utility, Weapon, Resource
	// IMPORTANT: If class names change for items, refactoring here is necessary!

	private string orderByType;


	// Sort Inventory by type in the order: Placables, Utilities, Weapons, Resources
	public List<InventorySlot> SortByType (List<InventorySlot> inventory, string type = "Placable", bool useStandardSort = true)
	{
		// Sorts into type order: R, P, U, W
		inventory.Sort (
			delegate(InventorySlot i1, InventorySlot i2) {

				if (IsUsableOrPlacable (i1) && IsUsableOrPlacable (i2)) {
					// Compare parent class names for both
					return i1.item.GetType ().BaseType.FullName.CompareTo (i2.item.GetType ().BaseType.FullName);
				} else if (IsUsableOrPlacable (i1)) {
					// Compare parent class name of i1 with current class name of i2
					return i1.item.GetType ().BaseType.FullName.CompareTo (i2.item.GetType ().FullName);
				} else if (IsUsableOrPlacable (i2)) {
					// Compare current class name of i1 with parent class name of i2
					return i1.item.GetType ().FullName.CompareTo (i2.item.GetType ().BaseType.FullName);
				} else {
					// Compare current class names for both
					return i1.item.GetType ().FullName.CompareTo (i2.item.GetType ().FullName);
				}
			}	
		); 


		if (useStandardSort) {
			return SortItemsByStandardOrder (inventory);
		} else {
			return SortItemsBySelectOrder (inventory, type);
		}

	}

	// Sort items not slots in Inventory A-Z
	public void SortAlphabetically (List<InventorySlot> inventory)
	{
		inventory.Sort (
			delegate(InventorySlot i1, InventorySlot i2) { 
				return i1.item.itemName.CompareTo (i2.item.itemName); 
			});
	}
	// Moves all resources (slots) in inventory last
	private List<InventorySlot> SortItemsByStandardOrder (List<InventorySlot> inventory)
	{
		List<InventorySlot> resourcesToMove = inventory.FindAll (IsResourceAndNotPlacable);
		inventory.RemoveAll (IsResourceAndNotPlacable);
		inventory.AddRange (resourcesToMove);
		return inventory;
	}

	private List<InventorySlot> SortItemsBySelectOrder (List<InventorySlot> inventory, string type = "Placable")
	{
		orderByType = type;
		List<InventorySlot> resourcesToMove = inventory.FindAll (IsSelectType);
		inventory.RemoveAll (IsSelectType);
		inventory.AddRange (resourcesToMove);
		return inventory;
	}

	private bool IsSelectType (InventorySlot slot)
	{
		
		if (IsUsableOrPlacable (slot)) {
			return slot.item.GetType ().BaseType.FullName != orderByType;
		} else {
			return slot.item.GetType ().FullName != orderByType;
		} 

	}

	private bool IsResourceAndNotPlacable (InventorySlot slot)
	{
		return ((slot.item is Resource) && !(slot.item is Placable));
	}

	private bool IsUsableOrPlacable (InventorySlot slot)
	{
		return (slot.item is Useables || slot.item is Placable);
	}

}
