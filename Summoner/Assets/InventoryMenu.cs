using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : BaseMenu {

	public static InventoryMenu instance;

	public static Inventory playerInventory;

	public void SetInventory(){
		playerInventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().inventory;
	}

	#region implemented abstract members of BaseMenu

	public override void Hide () {
		instance.gameObject.SetActive (false);
		if (playerInventory.changeOccurred != null) {
			playerInventory.changeOccurred -= UpdateInventoryViewer;
		}
	}

	public override void Show () {
		instance.gameObject.SetActive (true);
		if (playerInventory.changeOccurred == null) {
			playerInventory.changeOccurred += UpdateInventoryViewer;
		}
	}

	public static void UpdateInventoryViewer(){
		
	}

	#endregion
}