using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

	[Range (0f, 40f)]
	public int maxSlots = 40;
	private int usedSlots = 0;
	public int maxStackSize = 100;
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
		return usedSlots >= maxSlots;
	}

	private bool IsStackFull (BaseItem item)
	{
		return items [item] <= maxStackSize;
	}

	private int CalcSlotsUsedByQuantity(BaseItem item, int quantity) {
		return (quantity + items[item]) / maxStackSize;
	}

	private bool IsRoomFor(int slots) {
		return ( (usedSlots + slots) < maxSlots);
	}

	private int CalcRemainder() {
		return maxSlots - usedSlots;
	}


	private void HandleQuantityAndStacks (BaseItem item, int quantity)
	{

		int slots = CalcSlotsUsedByQuantity (item, quantity);

		if (IsRoomFor (slots)) {
			items [item] += quantity;
			if (items [item] > maxStackSize) {
				usedSlots++;
			}
		} else {
			slots = CalcRemainder ();
			if (slots > 0) {
				items [item] += slots * maxStackSize;
				usedSlots += slots;
			}
		}

	}

	public int GetNumberOfItems() {
		return usedSlots;
	}

	public int GetQuantityOfItem(BaseItem item) {
		return items [item];
	}
}