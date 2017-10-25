using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : BaseMenu {

	public static InventoryMenu instance;
	public static Inventory playerInventory;

	public GameObject itemSocketPrefab;

	public Transform inventorySlotParent;
	private List<InventoryMenuSlot> slots = new List<InventoryMenuSlot> ();
	private static int columns = 6;

	private int currentButtonIndex = 0;

	void Awake(){
		if(instance == null){
			instance = this;
		}else{
			Destroy(this);
		}

		for (int i = 0; i < Inventory.maxSlots; i++) {
			slots.Add(Instantiate (itemSocketPrefab, inventorySlotParent).GetComponent<InventoryMenuSlot>());
		}

		Hide();
	}

	public void SetInventory(){
		playerInventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().GetInventory ();
	}

	#region implemented abstract members of BaseMenu

	public override void Hide () {
		currentButtonIndex = 0;
		instance.gameObject.SetActive (false);

		if (playerInventory != null) {
			if (playerInventory.changeOccurred != null) {
				playerInventory.changeOccurred -= UpdateInventoryViewer;
			}
		}
	}

	public override void Show () {
		SetInventory ();
		UpdateInventoryViewer ();

		instance.gameObject.SetActive (true);

		if (playerInventory.changeOccurred == null) {
			playerInventory.changeOccurred += UpdateInventoryViewer;
		}
	}

	public void UpdateInventoryViewer(){
		for (int i = 0; i < Inventory.maxSlots; i++) {
			if (i < playerInventory.inventorySlots.Count) {
				slots [i].SetInventorySlot (playerInventory.inventorySlots [i]);
				slots [i].MarkInventorySlot (currentButtonIndex == i);
			} else {
				slots [i].SetInventorySlot (null);
				slots [i].MarkInventorySlot (false);
			}
		}
	}

	#endregion

	public void MoveHorizontally(bool isMovingRight){

		slots [currentButtonIndex].MarkInventorySlot (false);

		if (isMovingRight) {
			MoveRight ();
		} else {
			MoveLeft ();
		}

		slots [currentButtonIndex].MarkInventorySlot (true);
	}

	public void MoveVertically(bool isMovingUp){

		slots [currentButtonIndex].MarkInventorySlot (false);

		if (isMovingUp) {
			MoveUp ();
		} else {
			MoveDown ();
		}

		slots [currentButtonIndex].MarkInventorySlot (true);
	}

	public void MoveUp(){
		currentButtonIndex -= columns;
		if (currentButtonIndex < 0) {
			currentButtonIndex = playerInventory.inventorySlots.Count - 1;
		}
	}

	public void MoveDown(){
		currentButtonIndex += columns;
		if (currentButtonIndex >= playerInventory.inventorySlots.Count) {
			currentButtonIndex = 0;
		}
	}

	public void MoveLeft(){
		currentButtonIndex--;
		if (currentButtonIndex < 0) {
			currentButtonIndex = playerInventory.inventorySlots.Count - 1;
		}
	}

	public void MoveRight(){
		currentButtonIndex++;
		if (currentButtonIndex >= playerInventory.inventorySlots.Count) {
			currentButtonIndex = 0;
		}
	}

	public void Activate(){
		slots [currentButtonIndex].Activate();
		//GetComponent<BaseMenu> ().Hide ();
	}

	public void Return(){
		PauseMenu.instance.Show ();
		Hide ();
		//GetComponent<BaseMenu> ().Hide ();
	}

	void OnEnable(){
		InputHandler.LeftStickEvent ().horizontalAnalogEvent.becameActive += MoveHorizontally;
		InputHandler.LeftStickEvent ().verticalAnalogEvent.becameActive += MoveVertically;

		InputHandler.DPadEvent ().horizontalAnalogEvent.becameActive += MoveHorizontally;
		InputHandler.DPadEvent ().verticalAnalogEvent.becameActive += MoveVertically;

		InputHandler.BEvent().becameActive += Return;
		InputHandler.AEvent().becameActive += Activate;
	}

	void OnDisable(){
		InputHandler.LeftStickEvent ().horizontalAnalogEvent.becameActive -= MoveHorizontally;
		InputHandler.LeftStickEvent ().verticalAnalogEvent.becameActive -= MoveVertically;

		InputHandler.DPadEvent ().horizontalAnalogEvent.becameActive -= MoveHorizontally;
		InputHandler.DPadEvent ().verticalAnalogEvent.becameActive -= MoveVertically;

		InputHandler.BEvent().becameActive -= Return;
		InputHandler.AEvent().becameActive -= Activate;
	}
}