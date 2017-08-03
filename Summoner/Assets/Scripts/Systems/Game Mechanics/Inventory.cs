using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

	[Range(0f,100f)]
	public int maxSlots;
	private int slotsUsed;
	Dictionary<BaseItem, int> items = new Dictionary<BaseItem, int>();

	public void AddItem(BaseItem item) {
		if (!ExistsInInventory (item)) {
			items.Add (item, 1);
			AdjustSlotsUsed (1);
		} 
	}

	public void RemoveItem(BaseItem item) {
		if (ExistsInInventory (item)) {
			items.Remove (item);
			AdjustSlotsUsed (-1);
		}
	}

	public void AdjustQuantity(BaseItem item, int value) {
		if (ExistsInInventory (item)) {
			items [item] += value;
		}
	}

	public bool ExistsInInventory(BaseItem item) {
		if (items.ContainsKey (item)) {
			return true;
		}
		return false;
	}

	private void AdjustSlotsUsed(int value) {
		slotsUsed += value;
	}

}