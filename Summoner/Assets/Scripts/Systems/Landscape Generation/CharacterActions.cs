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
			float magnitude = dir.magnitude;
			Vector3 temp = Camera.main.transform.TransformDirection(new Vector3(dir.x, 0, dir.y));
			dir = new Vector2(temp.x, temp.z).normalized * magnitude;

			rb.velocity = new Vector3 (dir.x * stats.moveSpeed, rb.velocity.y, dir.y * stats.moveSpeed);
			rb.transform.LookAt(rb.transform.position + new Vector3(dir.x, 0, dir.y));
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