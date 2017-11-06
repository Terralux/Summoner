using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentHolder : MonoBehaviour {

	private GameObject child;
	private Image imageTarget;

	private Color idleColor;
	private Color selectedColor;

	private BaseItem myItem;

	void Awake(){
		idleColor = GetComponent<Image> ().color;
		selectedColor = Color.red;
	}

	public void AddContent(BaseItem bi){
		if(transform.childCount < 1){
			child = new GameObject ("Content");
			child.transform.SetParent (transform);
			imageTarget = child.AddComponent<Image> ();
		}

		imageTarget.sprite = bi.image;
		myItem = bi;
	}

	public void Clear(){
		if (imageTarget != null) {
			Destroy (imageTarget.sprite = null);
		}
	}

	public bool IsOccupied(){
		return (imageTarget != null);
	}

	public void SetSelected(bool setSelected){
		if (setSelected) {
			GetComponent<Image> ().color = selectedColor;
		} else {
			GetComponent<Image> ().color = idleColor;
		}
	}

	/// <summary>
	/// Use this item. returns true if successful
	/// </summary>
	public bool Use(){
		if (myItem as Placable != null) {
			PlayerController.instance.PlaceObject (myItem as Placable);
			return true;
			//Activate placable behavior
		}else if (myItem as Useables != null) {
			//Activate useable behavior
			(myItem as Useables).OnActivateFromMenu();
			return true;
		}

		return false;
	}
}