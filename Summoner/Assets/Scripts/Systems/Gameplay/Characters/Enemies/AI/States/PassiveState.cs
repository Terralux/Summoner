using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveState : State{

	public bool idle = true;

	void Update(){
		if (idle) {
			StartCoroutine ("Walk");
			idle = false;
		}
		if (!idle) {
			StartCoroutine ("Idle");
			idle = true;
		}
	}

	IEnumerator Walk(){
		yield return new WaitForSeconds (6);
		anim.SetFloat ("Speed", 1);


	}

	IEnumerator Idle(){
		yield return new WaitForSeconds (5);
		anim.SetFloat ("Speed", 0.1f);
	}
}
