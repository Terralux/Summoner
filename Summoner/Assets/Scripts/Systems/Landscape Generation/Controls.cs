using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : CharacterActions {
	
	// Update is called once per frame
	void Update () {
		//CharacterActions actions = GetComponent<CharacterActions>();
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		if (Input.GetKey (KeyCode.Space)) {
			movement ();
		}
	}
}
