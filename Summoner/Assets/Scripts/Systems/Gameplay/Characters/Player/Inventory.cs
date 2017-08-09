using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{

	[Range (0f, 40f)]
	public int maxSlots = 40;
	private int usedSlots = 0;
	public static int maxStackSize = 100;

	private List<InventorySlot> items = new List<InventorySlot> ();
	private InventorySorter inventorySorter = new InventorySorter ();

	public void AddItem (BaseItem item, int quantity)
	{

		if (IsInventoryFull ()) {
			Debug.LogWarning ("Inventory is full");
			return;
		}

		if (item as Resource == null && item as Decoration == null) {
			items.Add (new InventorySlot (item, 1));
		} else {
			InventorySlot inventorySlot = FindInInventory (item);
			if (inventorySlot != null) {
				HandleQuantityAndStacks (inventorySlot, quantity);
			} else {
				items.Add (new InventorySlot (item, quantity));
			}
		}

		UpdateSlotsUsed ();
	}

	public void RemoveItem (BaseItem item)
	{
		InventorySlot inventorySlot = FindInInventory (item);
		if (inventorySlot != null) {
			items.Remove (inventorySlot);
			UpdateSlotsUsed ();
			items.Sort ();
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
		foreach (InventorySlot slot in items) {
			if (slot.item as Resource != null || slot.item as Decoration != null) {
				usedSlots += slot.GetSlotsFilled ();
			} else {
				usedSlots++;
			}
		}
	}

	public InventorySlot FindInInventory (BaseItem item)
	{
		foreach (InventorySlot slot in items) {
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
		inventorySorter.SortByType (items);
	}

	public void SortItemsBySelectOrder (string type)
	{
		inventorySorter.SortByType (items, type, false);
	}

	private void DebugPrintItems (List<InventorySlot> inv)
	{
		Debug.Log ("Inventory contains: ");
		foreach (InventorySlot slot in inv) {
			if (slot.item.GetType () == typeof(Structure) || slot.item.GetType () == typeof(Decoration)
			    || slot.item.GetType () == typeof(Utility) || slot.item.GetType () == typeof(Vehicle)) {
				Debug.Log (slot.item.itemName);
			} else {
				Debug.Log (slot.item.itemName);
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

	public int GetSlotsFilled ()
	{
		return (quantity / Inventory.maxStackSize) + 1;
	}
}