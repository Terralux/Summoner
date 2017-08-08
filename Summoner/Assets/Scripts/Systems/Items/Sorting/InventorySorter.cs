using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySorter
{

	// Types: Placable, Utility, Weapon, Resource

	// Sort Inventory by type in the order: Placables, Utilities, Weapons, Resources
	public void SortByType (List<InventorySlot> inventory)
	{
		// Sorts into type order: P, R, U, W
		inventory.Sort (
			delegate(InventorySlot i1, InventorySlot i2) {
				return i1.item.GetType().FullName.CompareTo(i2.item.GetType().FullName);
			}	
		); 

		// Swap all [R <-> U,    W <-> R] items
		SwapItemsByTypeOrder(inventory);
		Debug.Log("Done sorting items in type order: P, U, W, R");
	}
	// Sort Inventory A-Z
	public void SortAlphabetically (List<InventorySlot> inventory)
	{
		inventory.Sort (
			delegate(InventorySlot i1, InventorySlot i2) { 
				return i1.item.itemName.CompareTo (i2.item.itemName); 
			});
	}

	// Sort Placables into Decorations & Structures, Weapons into Melee & Ranged, Usables into Utility & Vehicles
	private void SubTypeSort ()
	{
		
	}

	private void SwapItemsByTypeOrder(List<InventorySlot> inventory) {
		List<InventorySlot> weaponsToMove = inventory.FindAll(IsWeapon);
		inventory.RemoveAll (IsWeapon);
		inventory.AddRange (weaponsToMove);
		List<InventorySlot> resourcesToMove = inventory.FindAll(IsResource);
		inventory.RemoveAll (IsResource);
		inventory.AddRange (resourcesToMove);
	}

	private bool IsResource(InventorySlot slot) {
		return slot is Resource;
	}

	private bool IsWeapon(InventorySlot slot) {
		return slot is Weapon;
	}

}
