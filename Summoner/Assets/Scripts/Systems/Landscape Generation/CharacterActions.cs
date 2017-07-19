using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions {

	public bool isGrounded;
	Vector3 moveDir;

	private Rigidbody rb;
	private CharacterStats stats;

	public CharacterActions (Rigidbody rb, CharacterStats stats){
		this.stats = stats;
		this.rb = rb;
	}

	public void Movement (Vector2 dir){
		Debug.Log ("movement");
		if (isGrounded) {
			rb.velocity = new Vector3 (dir.x * stats.moveSpeed, rb.velocity.y, dir.y * stats.moveSpeed);
		}
		CheckIfGrounded ();
	}

	public void jump(){
		Debug.Log ("jump");
		if (isGrounded) {
			rb.velocity += Vector3.up * stats.jumpForce;
			isGrounded = false;
		}
	}

	public void CheckIfGrounded(){
		RaycastHit hit = new RaycastHit();

		if (Physics.Raycast (rb.position + Vector3.up * 0.1f, Vector3.down, out hit, 3f)) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}
	}
}