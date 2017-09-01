using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemDetails : MonoBehaviour
{

	public BaseItem item;
	public GameObject attributeGrid;

	void Start() {
		
	}

	public void GetDetailsFromItem(BaseItem item) {
		gameObject.SetActive (true);
		this.item = item;
		transform.Find ("Left Cell").Find("Icon").GetComponent<Image> ().sprite = item.image;
		transform.Find ("Left Cell").Find ("Icon").GetComponent<Image> ().color = new Color32 (255,255,255,255);
		transform.Find ("Middle Cell").Find("Name").GetComponent<Text> ().text = item.itemName;
		transform.Find ("Middle Cell").Find("Description").GetComponent<Text> ().text = item.description;

		if (item as Weapon != null) {
			ToggleAttributeGrid (true);
			//GetAttributesFromWeapon ();
		} else {
			ToggleAttributeGrid (false);
		}
	}

	public void GetAttributesFromWeapon () {
		if (item as Weapon != null) {
			WeaponAttributes wa = (item as Weapon).attributes;
			string[] arr = System.Enum.GetNames (typeof(Elementals));
			Debug.Log (arr.Length);
			AttributeUIContainer[] containers = GetAttributeSlots ();
			for (int i = 0; i < arr.Length; i++) {
				string element = arr [i];
				int value = (wa.elementalAffinities [element] as ElementalAffinity).value;
				containers [i].attImage.text = element;
				containers [i].attValue.text = value + "";
			} 
		}
	}

	public AttributeUIContainer[] GetAttributeSlots () {
		AttributeUIContainer[] containers = GetComponentsInChildren<AttributeUIContainer> ();
		return containers;
	}

	private void ToggleAttributeGrid(bool value) {
		attributeGrid.SetActive (value);
	}

}
