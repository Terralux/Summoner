using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIItem : MonoBehaviour {

	public InventorySlot slot;
	private string description;
	public string Description {
		get;
	}
	private Image background;

	public void Awake() {
		
	}

	public void Start() {
		SetupTileColor ();
		SetupIcon ();
	}
		
	private void SetupTileColor() {
		background = gameObject.AddComponent<Image> ();
		if (slot.item as Placable != null) {
			background.color = new Color32 (0,50,0,255);
		} else if (slot.item as Useables != null) {
			background.color = new Color32 (100,0,0,255);
		} else if (slot.item as Weapon != null) {
			background.color = new Color32 (0,0,150,255);
		} else if (slot.item as Resource != null) {
			background.color = new Color32 (100,50,150,255);
		} else {
			background.color = new Color32 (0,0,0,255);
		}

	} 

	void SetupIcon() {
		Image icon = GetComponentInChildren<Image> ();
		foreach (Image i in GetComponentsInChildren<Image>()) {
			if (i.gameObject != gameObject) {
				icon = i;
			}
		}
		icon.sprite = slot.item.image;
	}

}
