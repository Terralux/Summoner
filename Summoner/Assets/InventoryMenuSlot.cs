using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuSlot : MonoBehaviour {
	public InventorySlot mySlot;
	private Image myImage;

	public void SetInventorySlot(InventorySlot slot){
		mySlot = slot;
		myImage.sprite = mySlot.item.image;
	}

	public void RemoveItem(){
		mySlot.Remove ();
	}

	public void Move(){
		
	}
}