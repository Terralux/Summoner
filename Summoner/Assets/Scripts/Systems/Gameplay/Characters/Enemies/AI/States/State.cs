using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {

	public EnemyStats stats;
	public Rigidbody rb;
	public Animator anim;
	public List<State> states;

	void Awake(){
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}
}
