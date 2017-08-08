using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySorter
{

	// Sort Inventory by type in the order: Placables, Utilities, Weapons, Resources
	public void SortByType (List<InventorySlot> inventory)
	{
		
	}
	// Sort Inventory A-Z
	public void SortAlphabetically (List<InventorySlot> inventory)
	{
		inventory.Sort (
			delegate(InventorySlot i1, InventorySlot i2) { 
				return i1.item.name.CompareTo (i2.item.name); 
			});
	}

	// Sort Placables into Decorations & Structures, Weapons into Melee & Ranged, Usables into Utility & Vehicles
	private void SubTypeSort ()
	{
		
	}

}
