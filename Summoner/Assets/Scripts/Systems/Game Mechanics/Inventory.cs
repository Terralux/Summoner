using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

	[Range(0f,40f)]
	public int maxSlots;
	private int usedSlots;
	private Dictionary<BaseItem, int> items;

	public Inventory() {
		items = new Dictionary<BaseItem, int> ();
	}

	public void AddItem(BaseItem item, int quantity) {
		if (item as Resource == null && item as Decoration == null) {
			items.Add (item, 1);
			usedSlots++;
		} else {
			if (ExistsInInventory (item)) {
				items [item] += quantity;
			} else {
				items.Add (item, quantity);
				usedSlots++;
			}
		}
	}

	public void RemoveItem(BaseItem item) {
		if (ExistsInInventory (item)) {
			items.Remove (item);
			usedSlots--;
		}
	}

	public bool ExistsInInventory(BaseItem item) {
		return items.ContainsKey (item);
	}

	public void AdjustQuantity(BaseItem item, int value) {
		if (ExistsInInventory (item)) {
			items [item] += value;
			if (items [item] == 0) {
				RemoveItem (item);
			}
		}
	}

}