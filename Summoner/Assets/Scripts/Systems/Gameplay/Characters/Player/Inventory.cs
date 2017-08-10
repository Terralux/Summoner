using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

	[Range(0f,40f)]
	public int capacity;
	public List<BaseItem> items;

	public void AddItem(BaseItem item) {
		items.Add (item);
	}

	public void RemoveItem(BaseItem item) {
		items.Remove (item);
	}

}