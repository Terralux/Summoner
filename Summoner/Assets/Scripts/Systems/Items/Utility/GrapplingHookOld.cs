using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookOld : BaseUtility {

	public float maxRange;
	public float travelSpeed;
	private Rigidbody playerRb;
	private bool isHooking;

	Vector3 playerPosition;
	Vector3 travelToPosition;

	private float journey;
	private float fraction;
	private float timeStamp;

	SpringJoint springJoint;

	GameObject surfaceHooked;

	#region implemented abstract members of BaseUtility

	public override void OnControlOverride ()
	{
		// Enable unique control scheme for grappling hook here, etc. aim and movement
	}

	void Awake() {
		playerRb = GetComponent<Rigidbody> ();
		springJoint = GetComponent<SpringJoint> ();
	}

	// test update
	void Update() {
		if (Input.GetButtonDown ("B") && !isHooking) {
			Grapple ();
		} 

		OnGrappling ();
	}

	#endregion

	public void Grapple() {
		playerPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
		Vector3 aimDirection = playerPosition - Camera.main.transform.position;
		aimDirection = new Vector3 (aimDirection.x, 0, aimDirection.z);

		RaycastHit hit;
		if (Physics.Raycast (playerPosition, aimDirection, out hit, maxRange)) {
			//isHooking = true;
			playerRb.useGravity = false;

			//Debug.Log (hit.collider.name);

			Debug.DrawRay (playerPosition, aimDirection * maxRange, Color.magenta, 2f);
			travelToPosition = hit.point;
			journey = Vector3.Distance (hit.collider.transform.position, playerPosition);
			fraction = 0f;
			timeStamp = Time.time;

			GenerateHookedSurface (hit);
		}


	}

	void HookComplete() {
		// Let's clean up when we are done being a hooker ;D ;D ;D ;D 
		playerRb.useGravity = true;
		isHooking = false;
		Destroy (surfaceHooked);
	} 

	void OnDrawGizmos() {
		Gizmos.DrawWireSphere (travelToPosition, 0.8f);
	}


	void OnGrappling() {
		if (isHooking) {
			float distanceCovered = (Time.time - timeStamp) * travelSpeed;
			fraction = distanceCovered / journey;

			if (fraction > 1) {
				fraction = 1f;
				HookComplete ();
			}

			transform.position = Vector3.Lerp (playerPosition, travelToPosition, fraction);
		}
	}

	void GenerateHookedSurface(RaycastHit hit) {
		surfaceHooked = Instantiate (new GameObject (), hit.point, Quaternion.identity);
		Rigidbody surfaceRb = surfaceHooked.AddComponent<Rigidbody> ();
		surfaceRb.isKinematic = true;
		//Debug.Log ("Hooked surface: " +  surfaceHooked.transform.position + ", targetted point: " + hit.point);
		springJoint.connectedBody = surfaceRb;
		Debug.Log (springJoint.connectedBody);
	}
}
