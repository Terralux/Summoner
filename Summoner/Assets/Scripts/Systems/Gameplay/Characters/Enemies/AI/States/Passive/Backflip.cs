using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backflip : PassiveState {

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

	public override void Action(){
		anim.SetTrigger ("Test");
		endOfState (this);
	}

	public void OnEnable(){
		if (anim != null) {
			Action ();
		}
	}
}
