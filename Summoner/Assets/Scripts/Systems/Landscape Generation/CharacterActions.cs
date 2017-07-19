using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : CharacterStats{

	bool isGrounded;

	void start (){
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void movement (){
		if (!isGrounded) {
			transform.position = transform.position + Vector3.up * jumpForce;
		}
	}

	public void ćheckIfGrounded(){
		RaycastHit hit = new RaycastHit();

		if (Physics.Raycast (transform.position + Vector3.up * 0.1f, Vector3.down, out hit, 0.1f)) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}
	}
}
