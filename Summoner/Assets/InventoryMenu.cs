using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : BaseMenu {

	public static InventoryMenu instance;

	public static Inventory playerInventory;

	void Awake(){
		if(instance == null){
			instance = this;
		}else{
			Destroy(this);
		}
		SetInventory ();
		Hide();
	}

	public void SetInventory(){
		playerInventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().GetInventory();
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