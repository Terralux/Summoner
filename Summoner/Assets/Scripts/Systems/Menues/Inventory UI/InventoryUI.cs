using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : BaseMenu
{

	// Singleton
	private static InventoryUI instance;
	private Inventory inventory;
	private InventoryGrid grid;
	private int oldCount = 0;

	public GameObject itemInfo;

	#region implemented abstract members of BaseMenu

	public override void Hide ()
	{
		gameObject.SetActive (false);
	}

	public override void Show ()
	{
		gameObject.SetActive (true);
	}

	#endregion

	void Awake ()
	{
		//Singleton
		if (instance != null) {
			Destroy (instance);
		} else {
			instance = this;
		}
	}

	void Start ()
	{
		grid = this.GetComponentInChildren<InventoryGrid> ();
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().inventory;
	}

	void Update ()
	{
		if (inventory != null) {
			if (inventory.items.Count > oldCount) {
				grid.LoadItemsFromInventory (inventory.items);
				oldCount = inventory.items.Count;
			}
		}
	}

	public void SortInventory ()
	{
		if (inventory != null) {
			inventory.SortItemsByStandardOrder ();
			grid.LoadItemsFromInventory (inventory.items);
		}
	}

}
