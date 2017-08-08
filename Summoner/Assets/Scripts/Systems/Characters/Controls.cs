﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider))]
public class Controls : MonoBehaviour {

	public Player player;
	private CharacterActions actions;

	void Awake(){
		actions = new CharacterActions (GetComponent<Rigidbody> (), player);
	}

	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		actions.Movement (new Vector2 (h, v));

		if (Input.GetButtonDown("Jump")) {
			actions.Jump ();
		}
	}
}