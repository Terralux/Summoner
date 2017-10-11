using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellOfLife : MonoBehaviour {

	public CitySetup citySetup;
	public float radius;

	// Use this for initialization
	void Awake () {
		citySetup = new CitySetup(transform.position, radius);
	}

	void OnDrawGizmos() {
		Gizmos.DrawSphere (transform.position, radius);
	}
}