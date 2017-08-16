using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGrid : MonoBehaviour {

	public GameObject itemContainer;
	private UIItem uiItem;

	public void LoadItemsFromInventory(List<InventorySlot> items) {

		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}

		foreach (InventorySlot slot in items) {
			GameObject go = Instantiate (itemContainer);
			go.transform.parent = gameObject.transform;
			uiItem = go.AddComponent<UIItem> ();
			uiItem.slot = slot;
			go.transform.localScale = new Vector3 (1,1,1);
		}
	}
}
