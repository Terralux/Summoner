using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStructure : MonoBehaviour {

	public bool isPowered;
	public float interactRadius;

	public abstract void OnInteract();

	void OnTriggerEnter(Collider col) {
		if (col.CompareTag ("Player")) {
			Debug.Log ("Press X to interact with me!");
		}
	}

	void OnTriggerStay(Collider col) {
		if (col.CompareTag ("Player")) {
			// ! Test Input : Input goes in the actual controller instead !
			if (Input.GetKeyDown (KeyCode.X)) {
				OnInteract ();
			}
		}
	}
}
