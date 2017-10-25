using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory {

	public static int maxSlots = 40;
	private int usedSlots = 0;
	public static int maxStackSize = 100;

	public List<InventorySlot> inventorySlots = new List<InventorySlot> ();
	private InventorySorter inventorySorter = new InventorySorter ();

	public delegate void OnInventoryChanged();
	public OnInventoryChanged changeOccurred;

	public void AddItem (BaseItem item, int quantity)
	{
		if (IsInventoryFull ()) {
			Debug.LogWarning ("Inventory is full");
			return;
		}

		if (item as Resource == null && item as Decoration == null) {
			inventorySlots.Add (new InventorySlot (item, 1));
		} else {
			InventorySlot inventorySlot = FindInInventory (item);
			if (inventorySlot != null) {
				HandleQuantityAndStacks (inventorySlot, quantity);
			} else {
				inventorySlots.Add (new InventorySlot (item, quantity));
			}
		}

		UpdateSlotsUsed ();
	}

	public void RemoveItem (BaseItem item)
	{
		InventorySlot inventorySlot = FindInInventory (item);
		if (inventorySlot != null) {
			inventorySlots.Remove (inventorySlot);
			UpdateSlotsUsed ();
			inventorySlots.Sort ();
		}
	}

	private void HandleQuantityAndStacks (InventorySlot item, int quantity)
	{
		int slots = CalcSlotsUsedByQuantity (item, quantity);

		if (IsRoomFor (slots)) {
			item.quantity += quantity;
		} else {
			slots = CalcRemainder ();
			if (slots > 0) {
				item.quantity += slots * maxStackSize;
			}
		} 
	}

	public void UpdateSlotsUsed ()
	{
		usedSlots = 0;
		foreach (InventorySlot slot in inventorySlots) {
			if (slot.item as Resource != null || slot.item as Decoration != null) {
				usedSlots += slot.GetSlotsFilled ();
			} else {
				usedSlots++;
			}
		}

		if (changeOccurred != null) {
			changeOccurred ();
		}
	}

	public InventorySlot FindInInventory (BaseItem item)
	{
		foreach (InventorySlot slot in inventorySlots) {
			if (slot.item == item) {
				return slot;
			}
		}
		return null;
	}

	private void AdjustQuantity (BaseItem item, int value)
	{
		InventorySlot inventorySlot = FindInInventory (item);

		if (inventorySlot != null) {
			inventorySlot.quantity += value;

			if (inventorySlot.quantity <= 0) {
				RemoveItem (item);
			}
		}

		if (changeOccurred != null) {
			changeOccurred ();
		}
	}

	private bool IsInventoryFull ()
	{
		return usedSlots >= maxSlots;
	}

	private int CalcSlotsUsedByQuantity (InventorySlot inventorySlot, int quantity)
	{
		return ((quantity + inventorySlot.quantity) / maxStackSize);
	}

	private bool IsRoomFor (int slots)
	{
		return ((usedSlots + slots) <= maxSlots);
	}

	private int CalcRemainder ()
	{
		return maxSlots - usedSlots;
	}

	public int GetNumberOfItems ()
	{
		return usedSlots;
	}

	public int GetQuantityOfItem (BaseItem item)
	{
		return FindInInventory (item).quantity;
	}

	public void SortItemsByStandardOrder ()
	{
		inventorySorter.SortByType (inventorySlots);
	}

	public void SortItemsBySelectOrder (string type)
	{
		inventorySorter.SortByType (inventorySlots, type, false);
	}

	private void DebugPrintItems ()
	{
		Debug.Log ("Inventory contains: ");
		foreach (InventorySlot slot in inventorySlots) {
			if (slot.item.GetType () == typeof(Structure) || slot.item.GetType () == typeof(Decoration)
			    || slot.item.GetType () == typeof(Utility) || slot.item.GetType () == typeof(Vehicle)) {
				Debug.Log (slot.item.itemName);
			} else {
				Debug.Log (slot.item.itemName);
			}
		}
	}

	public void RemoveInventorySlot(InventorySlot slot){
		for (int i = 0; i < inventorySlots.Count; i++) {
			if (inventorySlots[i] == slot) {
				inventorySlots.RemoveAt (i);
				return;
			}
		}
	}
}

public class InventorySlot
{
	public BaseItem item;
	public int quantity;

	public InventorySlot (BaseItem item, int quantity)
	{
		this.item = item;
		this.quantity = quantity;
	}

	public int GetSlotsFilled (){
		return (quantity / Inventory.maxStackSize) + 1;
	}
}