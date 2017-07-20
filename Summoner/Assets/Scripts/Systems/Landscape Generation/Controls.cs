using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	public CharacterStats stats;
	private CharacterActions actions;

	void Awake(){
		actions = new CharacterActions (GetComponent<Rigidbody> (), stats);
	}

	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		actions.Movement (new Vector2 (h, v));

		if (Input.GetKeyDown (KeyCode.Q)) {
			actions.jump ();
		}
	}

	void OnGUI(){
		GUI.Box (new Rect (10, 10, 100, 30), actions.isGrounded + "");
	}
}