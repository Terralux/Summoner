using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

	[Range (0f, 40f)]
	public int maxSlots;
	private int usedSlots;
	public int maxQuantity;
	private Dictionary<BaseItem, int> items;

	public Inventory ()
	{
		items = new Dictionary<BaseItem, int> ();
	}

	public void AddItem (BaseItem item, int quantity)
	{

		if (IsInventoryFull ()) {
			Debug.LogWarning ("Inventory is full");
			return;
		}

		if (item as Resource == null && item as Decoration == null) {
			items.Add (item, 1);
			usedSlots++;
		} else {
			if (ExistsInInventory (item)) {
				HandleQuantityAndStacks (item, quantity);
			} else {
				items.Add (item, quantity);
				usedSlots++;
			}
		}
	}

	public void RemoveItem (BaseItem item)
	{
		if (ExistsInInventory (item)) {
			items.Remove (item);
			usedSlots--;
		}
	}

	private bool ExistsInInventory (BaseItem item)
	{
		return items.ContainsKey (item);
	}

	private void AdjustQuantity (BaseItem item, int value)
	{
		if (ExistsInInventory (item)) {
			items [item] += value;
			if (items [item] == 0) {
				RemoveItem (item);
			}
		}
	}

	private bool IsInventoryFull ()
	{
		return usedSlots < maxSlots;
	}

	private bool IsStackFull (BaseItem item)
	{
		return items [item] <= maxQuantity;
	}

	private void HandleQuantityAndStacks (BaseItem item, int quantity)
	{
		foreach (BaseItem i in items.Keys) {
			if (i == item) {
				if (quantity > 0) {
					if (items [i] < maxQuantity) {
						if (quantity + items [i] <= maxQuantity) {
							items [item] += quantity;
							quantity = 0;
						} else {
							int remainder = (items [i] + quantity) - maxQuantity;
							items [item] = maxQuantity;
							quantity = remainder;
						}
					}
				}
			}
		}
		if (quantity > 0) {
			items.Add (item, quantity);
		}
	}

	public int GetNumberOfItems() {
		return usedSlots;
	}

	public int GetQuantityOfItem(BaseItem item) {
		return items [item];
	}
}