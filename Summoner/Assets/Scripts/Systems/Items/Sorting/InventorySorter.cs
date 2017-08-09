using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySorter
{

	// Types: Placable, Utility, Weapon, Resource
	// IMPORTANT: If class names change for items, refactoring here is necessary!
	// TODO: Implement sorting of weapons into: melee & ranged respectively

	private string orderByType;


	// Sort Inventory by type in the order: Placables, Utilities, Weapons, Resources
	public List<InventorySlot> SortByType (List<InventorySlot> inventory, string type = "Placable", bool useStandardSort = true)
	{
		// Sorts into type order: R, P, U, W
		inventory.Sort (
			delegate(InventorySlot i1, InventorySlot i2) {

				if ( (IsUsableOrPlacable (i1) && IsUsableOrPlacable (i2)) || SlotIsWeapon(i1) && SlotIsWeapon(i2)) {
					// Compare parent class names for both
					return i1.item.GetType ().BaseType.FullName.CompareTo (i2.item.GetType ().BaseType.FullName);
				} else if (IsUsableOrPlacable (i1) || SlotIsWeapon(i1)) {
					// Compare parent class name of i1 with current class name of i2
					return i1.item.GetType ().BaseType.FullName.CompareTo (i2.item.GetType ().FullName);
				} else if (IsUsableOrPlacable (i2) || SlotIsWeapon(i2)) {
					// Compare current class name of i1 with parent class name of i2
					return i1.item.GetType ().FullName.CompareTo (i2.item.GetType ().BaseType.FullName);
				} else {
					// Compare current class names for both
					return i1.item.GetType ().FullName.CompareTo (i2.item.GetType ().FullName);
				}
			}	
		); 

		inventory = SortSubTypes (inventory);


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
		List<InventorySlot> slotsToMove = inventory.FindAll (IsSelectType);
		inventory.RemoveAll (IsSelectType);
		inventory.AddRange (slotsToMove);
		return inventory;
	}

	private List<InventorySlot> SortSubTypes(List<InventorySlot> inventory) {

		// Swap positions of all weapons found in melee -> range order
		List<InventorySlot> weaponsToMove = inventory.FindAll (SlotIsWeapon);
		weaponsToMove.Sort (
			delegate(InventorySlot i1, InventorySlot i2) {
				return i1.item.GetType ().FullName.CompareTo (i2.item.GetType ().FullName);
			});
		// Find position to insert new sorted list of our weapons
		int j = inventory.FindIndex (SlotIsWeapon);
		// Remove all weapons from inventory
		inventory.RemoveAll (SlotIsWeapon);
		//Insert sorted weapons into inventory at position where old list was
		if (j >= 0) {
			inventory.InsertRange (j, weaponsToMove);
		}

		List<InventorySlot> useablesToMove = inventory.FindAll (SlotIsUseable);
		useablesToMove.Sort (
			delegate(InventorySlot i1, InventorySlot i2) {
				return i1.item.GetType().FullName.CompareTo(i2.item.GetType().FullName);
			}
		);

		int i = inventory.FindIndex (SlotIsUseable);
		inventory.RemoveAll (SlotIsUseable);

		if (i >= 0) {
			inventory.InsertRange (i, useablesToMove);
		}

		return inventory;
	}

	private bool SlotIsWeapon(InventorySlot slot) {
		return slot.item.GetType ().BaseType == typeof(Weapon);
	}

	private bool SlotIsUseable(InventorySlot slot) {
		return slot.item.GetType ().BaseType == typeof(Useables);
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
