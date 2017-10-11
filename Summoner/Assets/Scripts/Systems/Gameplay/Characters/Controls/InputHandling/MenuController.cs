using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	public static MenuController instance;

	public enum MenuState{
		
	}

	private MenuState currentState;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	void Update () {
		
	}
}