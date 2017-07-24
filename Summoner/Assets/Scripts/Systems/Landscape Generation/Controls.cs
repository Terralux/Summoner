using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider))]
public class Controls : MonoBehaviour {

	public CharacterStats stats;
	private CharacterActions actions;

	void Awake(){
		actions = new CharacterActions (GetComponent<Rigidbody> (), stats);
	}

	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		actions.Movement (new Vector2 (0, v));

		if (Input.GetButtonDown("Jump")) {
			actions.jump ();
		}
	}

	void OnGUI(){
		GUI.Box (new Rect (10, 10, 100, 30), actions.isGrounded + "");
	}
}