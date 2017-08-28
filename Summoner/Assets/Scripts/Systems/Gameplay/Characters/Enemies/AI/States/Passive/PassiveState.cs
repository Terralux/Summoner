using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class PassiveState : State{


	//public override void Action ();

	/*void Update(){
		if (!idle) {
			StartCoroutine (Idle());
		}
		if (idle) {
			StartCoroutine (Walk());
		}
	}

	IEnumerator Walk(){
		anim.SetFloat ("Speed", 1);
		yield return new WaitForSeconds (6);
		idle = false;
	}

	IEnumerator Idle(){
		anim.SetFloat ("Speed", 0.1f);
		yield return new WaitForSeconds (5);
		idle = true;
	}*/
}
