using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemHotbarHandler : MonoBehaviour {

	private static ActiveItemHotbarHandler instance;

	public ContentHandler placables;
	public ContentHandler utility;

	private LayoutElement placablesLayout;
	private RectTransform placablesRect;
	private LayoutElement utilityLayout;
	private RectTransform utilityRect;

	private float minWidth;
	private float maxWidth;
	private float minHeightPref;
	private float maxHeightPref;

	private bool utilityBarIsSelected = false;

	private static int selectedObjectIndex = 0;

	void Start () {
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}

		placablesLayout = placables.GetComponent<LayoutElement> ();
		utilityLayout = utility.GetComponent<LayoutElement> ();
		placablesRect = placables.GetComponent<RectTransform> ();
		utilityRect = utility.GetComponent<RectTransform> ();

		if (utilityRect.sizeDelta.x > placablesRect.sizeDelta.x) {
			minWidth = placablesRect.sizeDelta.x;
			maxWidth = utilityRect.sizeDelta.x;

			minHeightPref = placablesLayout.preferredHeight;
			maxHeightPref = utilityLayout.preferredHeight;
		} else {
			minWidth = utilityRect.sizeDelta.x;
			maxWidth = placablesRect.sizeDelta.x;

			minHeightPref = utilityLayout.preferredHeight;
			maxHeightPref = placablesLayout.preferredHeight;
		}

		UpdateGraphics ();
	}

	public static void SwitchActiveHotbar(){
		if (instance.utilityBarIsSelected) {
			instance.utility.transform.SetAsFirstSibling ();

			instance.utilityLayout.preferredHeight = instance.minHeightPref;
			instance.utilityRect.sizeDelta = new Vector2 (instance.minWidth, instance.utilityRect.sizeDelta.y);

			instance.placablesLayout.preferredHeight = instance.maxHeightPref;
			instance.placablesRect.sizeDelta = new Vector2 (instance.maxWidth, instance.utilityRect.sizeDelta.y);
		} else {
			instance.placables.transform.SetAsFirstSibling ();

			instance.placablesLayout.preferredHeight = instance.minHeightPref;
			instance.placablesRect.sizeDelta = new Vector2 (instance.minWidth, instance.utilityRect.sizeDelta.y);

			instance.utilityLayout.preferredHeight = instance.maxHeightPref;
			instance.utilityRect.sizeDelta = new Vector2 (instance.maxWidth, instance.utilityRect.sizeDelta.y);
		}

		instance.utilityBarIsSelected = !instance.utilityBarIsSelected;
		UpdateGraphics ();
	}

	public static void AddItem(BaseItem bi){
		if (bi as Placable != null) {
			instance.placables.AddItem (bi);
		} else {
			instance.utility.AddItem (bi);
		}
	}

	public static void MoveSelector(bool isMovingRight){
		if (isMovingRight) {
			selectedObjectIndex = selectedObjectIndex + 1; 
			selectedObjectIndex	= selectedObjectIndex % (instance.utilityBarIsSelected ? instance.utility.transform.childCount : instance.placables.transform.childCount);
		} else {
			selectedObjectIndex = selectedObjectIndex - 1;

			if (selectedObjectIndex < 0) {
				selectedObjectIndex = (instance.utilityBarIsSelected ? instance.utility.transform.childCount : instance.placables.transform.childCount) - 1;
			}

			selectedObjectIndex = selectedObjectIndex % (instance.utilityBarIsSelected ? instance.utility.transform.childCount : instance.placables.transform.childCount);
		}
		UpdateGraphics ();
	}

	private static void UpdateGraphics(){
		instance.utility.UpdateSelectedCell (selectedObjectIndex, instance.utilityBarIsSelected);
		instance.placables.UpdateSelectedCell (selectedObjectIndex, !instance.utilityBarIsSelected);
	}

	public static void UseItem(){
		if (instance.utilityBarIsSelected) {
			if (instance.utility.UseItem (selectedObjectIndex)) {
				PlayerController.SetPlayerState (PlayerController.PlayerState.CAN_MOVE);
			}
		} else {
			if (instance.placables.UseItem (selectedObjectIndex)) {
				PlayerController.SetPlayerState (PlayerController.PlayerState.CAN_MOVE);
			}
		}
	}
}