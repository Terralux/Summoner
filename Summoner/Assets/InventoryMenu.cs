using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : BaseMenu {

	public static InventoryMenu instance;

	public static Inventory playerInventory;

	void Awake () {
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
		instance.Hide ();
	}

	#region implemented abstract members of BaseMenu

	public override void Hide () {
		instance.gameObject.SetActive (false);
		playerInventory.changeOccurred -= UpdateInventoryViewer;
	}

	public override void Show () {
		instance.gameObject.SetActive (true);
		playerInventory.changeOccurred += UpdateInventoryViewer;
	}

	public static void UpdateInventoryViewer(){
		
	}

	#endregion
}