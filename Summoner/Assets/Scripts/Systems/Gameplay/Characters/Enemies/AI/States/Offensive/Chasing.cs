using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chasing : OffensiveState {

	Transform target;
	bool playerInSight;

	public float viewDistance;
	public float viewAngle;

	#region implemented abstract members of State

	public override void Action ()
	{
		//target = GameObject.FindGameObjectWithTag ("Player").transform;
		anim.SetFloat ("Speed", 1);



		if(playerInSight){
			agent.SetDestination (target.position);
		}
	}

	public override void Init (Animator anim, Rigidbody rb, NavMeshAgent agent)
	{
		this.anim = anim;
		this.rb = rb;
		this.agent = agent;
	}

	#endregion

	public void Update(){
		//detectPlayer (transform.position, 2);
		if(!playerInSight){
			endOfState (this);
		}
	}

	public void detectPlayer(Vector3 center, float radius){
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		Vector3 distance = target.position - transform.position;
		float angle = Vector3.Angle (distance, transform.forward);

		if (angle < (viewAngle / 2)) {
			RaycastHit hit;

			if (Physics.Raycast (transform.position, distance, out hit, viewDistance)) {
				if (hit.collider.CompareTag ("Player")) {
					playerInSight = true;
					Action ();
				}
			}
		} else{
			Collider[] col = Physics.OverlapSphere (center, radius);
			for (int i = 0; i < col.Length; i++) {
				if (col[i].CompareTag("Player") ) {
					playerInSight = true;
					Action ();
				}
			}
		}
	}

	public void OnEnable(){
		if (anim != null) {
			detectPlayer (transform.position, 2);
		}
	}

}
