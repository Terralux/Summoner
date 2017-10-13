using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	public bool canInteract = false;

	private List<Interactive> interactables = new List<Interactive>();

	int index = 0;

	void Awake(){
		InputHandler.Y.becameActive += Interact;
	}

	void OnTriggerEnter(Collider col){
		Interactive i = col.GetComponent<Interactive> ();
		if (i != null) {
			if (i as AutomaticStructure != null) {
				i.OnInteract ();
			}

			interactables.Add (col.GetComponent<Interactive> ());

			if (interactables.Count == 1) {
				InputHandler.A.becameActive -= PlayerController.instance.Jump;
				InputHandler.A.becameActive += Interact;
			}
		}
	}

	void OnTriggerExit(Collider col){
		Interactive i = col.GetComponent<Interactive> ();
		if (i != null) {
			if (i as AutomaticStructure != null) {
				i.OnInteract ();
			}

			interactables.Remove (col.GetComponent<Interactive> ());

			if (interactables.Count == 0) {
				InputHandler.A.becameActive -= Interact;
				InputHandler.A.becameActive += PlayerController.instance.Jump;
			}
		}
	}

	void Update(){
		if (interactables.Count > 0) {

			canInteract = true;

			index = 0;
			float minimumRange = -1f;

			for (int i = 0; i < interactables.Count; i++) {
				float dot = Vector3.Dot (Camera.main.transform.forward, (interactables [i].transform.position - transform.position).normalized);
				if (minimumRange <= dot) {
					index = i;
					minimumRange = dot;
				}
			}
		} else {
			canInteract = false;
		}
	}

	public void Interact(){
		if (interactables.Count > 0) {
			interactables [index].OnInteract ();
		}
	}

	void OnGUI(){
		GUI.Box (new Rect (10, 10, 100, 30), (interactables.Count > 0 ? "Can interact" : "No interaction"));
	}
}