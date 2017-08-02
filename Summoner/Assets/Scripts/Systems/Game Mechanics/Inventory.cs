using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

	[Range(0f,40f)]
	public int capacity;
	public List<BaseItem> inventory;

	public void AddItem(BaseItem item) {
		inventory.Add (item);
	}

	public void RemoveItem(BaseItem item) {
		inventory.Remove (item);
	}

	public List<BaseItem> GetInventory() {
		return inventory;
	}

}
