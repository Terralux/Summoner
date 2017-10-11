using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHandler : MonoBehaviour {

	private List<ContentHolder> contentHolders = new List<ContentHolder>();

	void Awake () {
		foreach (ContentHolder ch in GetComponentsInChildren<ContentHolder>()) {
			contentHolders.Add (ch);
		}
	}

	public void AddItem(BaseItem bi){
		for (int i = 0; i < contentHolders.Count; i++) {
			if (!contentHolders [i].IsOccupied ()) {
				contentHolders [i].AddContent (bi);
				return;
			}
		}
	}

	public void UpdateSelectedCell(int index, bool isCurrentlySelected){
		if (contentHolders.Count < 1) {
			StartCoroutine (WaitForRaceCondition (index, isCurrentlySelected));
		}

		for (int i = 0; i < contentHolders.Count; i++) {
			if (contentHolders [i].transform.GetSiblingIndex () == index && isCurrentlySelected) {
				contentHolders [i].SetSelected (true);
			} else {
				contentHolders [i].SetSelected (false);
			}
		}
	}

	private IEnumerator WaitForRaceCondition(int index, bool isCurrentlySelected){
		yield return new WaitForSeconds (0.5f);
		UpdateSelectedCell (index, isCurrentlySelected);
	}

	public bool UseItem(int index){
		for (int i = 0; i < contentHolders.Count; i++) {
			if (contentHolders [i].transform.GetSiblingIndex () == index) {
				if (!contentHolders [i].IsOccupied ()) {
					Debug.Log ("You can't use this slot as no item is present");
					return false;
				} else {
					return contentHolders [i].Use ();
				}
			}
		}
		return false;
	}
}