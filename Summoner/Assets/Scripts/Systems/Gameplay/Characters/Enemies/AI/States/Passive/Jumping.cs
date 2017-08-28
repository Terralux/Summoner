using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : PassiveState {
	#region implemented abstract members of State
	public override void Action ()
	{
		throw new System.NotImplementedException ();
	}
	public override void Init (Animator anim, Rigidbody rb, UnityEngine.AI.NavMeshAgent agent)
	{
		throw new System.NotImplementedException ();
	}
	#endregion
	
}
