using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuTest : MonoBehaviour {

	public InventoryUI ui;
	public bool isShowing = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.I)) {
			if (!isShowing) {
				ui.Show ();
				isShowing = !isShowing;
			} else {
				ui.Hide ();
				isShowing = !isShowing;
			}
		}
		
	}
}
