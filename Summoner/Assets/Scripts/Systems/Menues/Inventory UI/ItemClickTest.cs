using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickTest : MonoBehaviour {

	private Button btn;
	public UI_ItemDetails itemDetails;

	void Start() {
		btn = GetComponent<Button> ();
		btn.onClick.AddListener (ButtonAction);
		itemDetails = GameObject.Find ("Info Grid").GetComponent<UI_ItemDetails> ();
	}

	private void ButtonAction() {
		UIItem uiItem = GetComponentInParent<UIItem> ();
		itemDetails.GetDetailsFromItem (uiItem.slot.item);
	}

}
