using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions {

	public bool isGrounded;
	Vector3 moveDir;

	private Rigidbody rb;
	//private Player player;
	private Animator anim;

	public CharacterActions (Rigidbody rb, Player player, Animator anim){
		//this.player = player;
		this.rb = rb;
		this.anim = anim;
	}

	#region movement
	public void Movement (Vector2 dir, float LT){
		if (isGrounded) {
			float magnitude = dir.magnitude;
			Vector3 temp = Camera.main.transform.TransformDirection(new Vector3(dir.x, 0, dir.y));

			dir = new Vector2(temp.x, temp.z).normalized * magnitude;

			//rb.velocity = new Vector3 (dir.x * player.stats.moveSpeed, rb.velocity.y * Time.deltaTime, dir.y * player.stats.moveSpeed);

			if(LT > 0.2f){
				Vector3 lookDir = Camera.main.transform.forward;
				rb.transform.LookAt(rb.transform.position + new Vector3(lookDir.x, 0, lookDir.z));
			}else{
				rb.transform.LookAt(rb.transform.position + new Vector3(dir.x, 0, dir.y));
			}
		}

		if(dir.magnitude > 1){
			dir = dir.normalized;
		}

		anim.SetFloat ("Speed", dir.magnitude);
		anim.SetTrigger ("IsFalling");

		CheckIfGrounded ();
	}
	#endregion 

	#region jump
	public void Jump(){
		if (isGrounded) {
			//rb.velocity += Vector3.up * player.stats.jumpForce;
			anim.SetBool ("Jump", true);
			isGrounded = false;
		}
	}
	#endregion

	public void CheckIfGrounded(){
		RaycastHit hit = new RaycastHit();

		if (Physics.Raycast (rb.position + Vector3.up * 0.1f, Vector3.down, out hit, 1f)) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}
	}

	public void Interact(){
		anim.SetTrigger("Interact");
	}
}