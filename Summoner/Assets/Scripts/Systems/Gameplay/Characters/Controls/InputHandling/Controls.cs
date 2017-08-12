using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(Player))]
public class Controls : MonoBehaviour {

	private CharacterActions actions;

	void Awake(){
		actions = new CharacterActions (GetComponent<Rigidbody> (), 
			GetComponent<Player> (), 
			GetComponentInChildren<Animator>()
		);
	}

	/*
	 * Left stick to move character
	 * right stick to move camera
	 * A to jump
	 * Y to use item
	 * Right Trigger to attack
	 * Left Trigger to look in camera direction
	 * 
	 * Bumpers Left and Right to shuffle across Item Bar
	 * 
	 * Holding a directional button changes the regular attack to use of the selected skill
	 * 
	 * 
	*/

	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		float DH = Input.GetAxis ("DPadHorizontal");
		float DV = Input.GetAxis ("DPadVertical");

		float RT = Input.GetAxis ("RT");
		float LT = Input.GetAxis ("LT");

		actions.Movement (new Vector2 (h, v), LT);

		if (Input.GetButtonDown("A")) {
			actions.Jump ();
		}

		if(Input.GetButtonDown("LB")){
			//Move left on Item menu
		}
		if(Input.GetButtonDown("RB")){
			//Move right on Item menu
		}

		if(Input.GetButtonDown("Y")){
			//Use selected Item
			actions.Interact();
		}

		if(DH > 0.1f || DH < -0.1f || DV > 0.1f || DV < -0.1f){
			if (RT > 0.2f) {
				//Do Regular attack
			}
		}else{
			if(Mathf.Abs(DH) > Mathf.Abs(DV)){
				if(DH > 0){
					if (RT > 0.2f) {
						//Do skill on D-Pad Left
					}
				}else{
					if (RT > 0.2f) {
						//Do skill on D-Pad Right
					}
				}
			}else{
				if(DV > 0){
					if (RT > 0.2f) {
						//Do skill on D-Pad Down
					}
				}else{
					if (RT > 0.2f) {
						//Do skill on D-Pad Up
					}
				}
			}
		}
	}
}