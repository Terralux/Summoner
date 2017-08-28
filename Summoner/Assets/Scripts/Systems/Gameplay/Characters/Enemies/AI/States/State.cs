using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour {

	//public EnemyStats stats;
	protected Rigidbody rb;
	protected Animator anim;
	protected NavMeshAgent agent;
	//public List<State> states;

	public delegate void StateEnd(State someState);
	public StateEnd endOfState;

	public abstract void Action ();

	public abstract void Init (Animator anim, Rigidbody rb, NavMeshAgent agent);
}

