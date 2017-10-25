using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuSlot : MonoBehaviour {
	public InventorySlot mySlot;
	private Button myButton;

	void Awake(){
		myButton = GetComponent<Button> ();
	}

	public void SetInventorySlot(InventorySlot slot){
		if (slot != null) {
			mySlot = slot;
			myButton.image.sprite = mySlot.item.image;
		} else {
			myButton.image.color = new Color (0, 0, 0, 0);
		}
	}

	public void RemoveItem(){
		
	}

	public void Move(){
		
	}

	public void MarkInventorySlot (bool isMarked){
		myButton.interactable = isMarked;
	}

	public void Activate(){
		myButton.onClick.Invoke ();
		Debug.Log ("I clicked on " + mySlot.item.itemName);
	}
}