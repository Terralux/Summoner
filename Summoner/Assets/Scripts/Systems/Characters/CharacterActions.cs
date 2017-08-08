using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions {

	public bool isGrounded;
	Vector3 moveDir;

	private Rigidbody rb;
	private Player player;

	public CharacterActions (Rigidbody rb, Player player){
		this.player = player;
		this.rb = rb;
	}

	#region movement
	public void Movement (Vector2 dir){
		if (isGrounded) {
			float magnitude = dir.magnitude;
			Vector3 temp = Camera.main.transform.TransformDirection(new Vector3(dir.x, 0, dir.y));

			dir = new Vector2(temp.x, temp.z).normalized * magnitude;

			rb.velocity = new Vector3 (dir.x * player.stats.moveSpeed, rb.velocity.y, dir.y * player.stats.moveSpeed);
			rb.transform.LookAt(rb.transform.position + new Vector3(dir.x, 0, dir.y));
		}
		CheckIfGrounded ();
	}
	#endregion 

	#region jump
	public void Jump(){
		if (isGrounded) {
			rb.velocity += Vector3.up * player.stats.jumpForce;
			isGrounded = false;
		}
	}
	#endregion

	public void TakeDamage(){
			
	}

	public void DealDamage(){
	
	}

	public void CheckIfGrounded(){
		RaycastHit hit = new RaycastHit();

		if (Physics.Raycast (rb.position + Vector3.up * 0.1f, Vector3.down, out hit, 1f)) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}
	}
}