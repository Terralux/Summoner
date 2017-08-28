using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Patrol : PassiveState {

	float maxRadius = 5;
	bool hasDestination;
	float wanderTimer = 3;
	float timer = 0;
	float delay = 0;

	private Vector3 origin;

	public override void Init (Animator anim, Rigidbody rb, NavMeshAgent agent)
	{
		this.anim = anim;
		this.rb = rb;
		this.agent = agent;
		origin = transform.position;
	}


	public void Update(){
		if (Vector3.Distance(agent.destination, transform.position) < 0.5f) {
			endOfState (this);
		}
	}

	public override void Action(){
		anim.SetFloat ("Speed", 1);
		//Debug.Log (anim);
		Vector3 enemyPos = RandomNavSphere (origin, maxRadius, -1);
		//Debug.Log (enemyPos);
		agent.SetDestination (enemyPos);
		//Debug.Log (agent);
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask){
		Vector3 randDistance = Random.insideUnitSphere * distance;

		randDistance += origin;

		NavMeshHit hit;

		NavMesh.SamplePosition (randDistance, out hit, distance, layerMask);

		return hit.position;
	}

	public void OnEnable(){
		timer = 0;
		delay = Random.Range (8, 10);

		if (anim != null) {
			Action ();	
		}
	}
}	
