using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idling : PassiveState {

	float timer = 0;
	float delay = 100;

	#region implemented abstract members of State

	public override void Init (Animator anim, Rigidbody rb, UnityEngine.AI.NavMeshAgent agent)
	{
		this.anim = anim;
		this.rb = rb;
		this.agent = agent;
	}

	#endregion

	public void Update(){
		timer += Time.deltaTime;

		if (timer >= delay) {
			endOfState (this);
		}

	}

	public override void Action(){
		anim.SetFloat ("Speed", 0.1f);
	}

	public void OnEnable(){
		delay = Random.Range (3, 8);
		timer = 0;

		if (anim != null) {
			Action ();
		}
	}
}
