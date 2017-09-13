using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterMovement {

	private bool isGrounded;
	private Vector3 moveDir;

	private Rigidbody rb;
	private Animator anim;

	private bool hasLockedViewToCamera = false;

	public BaseCharacterMovement (Rigidbody rb, Player player, Animator anim){
		this.rb = rb;
		this.anim = anim;
	}

	public void AdjustMovementBehavior(bool lockViewToCamera){
		hasLockedViewToCamera = lockViewToCamera;
	}

	#region movement
	public void Movement (Vector2 dir){
		if (isGrounded) {
			float magnitude = dir.magnitude;
			Vector3 temp = Camera.main.transform.TransformDirection (new Vector3 (dir.x, 0, dir.y));

			dir = new Vector2 (temp.x, temp.z).normalized * magnitude;

			if (hasLockedViewToCamera) {
				Vector3 lookDir = Camera.main.transform.forward;
				rb.transform.LookAt (rb.transform.position + new Vector3 (lookDir.x, 0, lookDir.z));
			} else {
				rb.transform.LookAt (rb.transform.position + new Vector3 (dir.x, 0, dir.y));
			}
		}

		if(dir.magnitude > 1){
			dir = dir.normalized;
		}

		anim.SetFloat ("Speed", dir.magnitude);
		anim.SetTrigger ("IsFalling");
	}
	#endregion

	#region jump
	public void Jump(){
		if (isGrounded) {
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
}