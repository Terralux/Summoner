using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySetup {

	public delegate void OnEvent();
	public OnEvent placement;

	Vector3 cityCenter;
	float radius;

	public List<BaseStructure> structuresWithinReach;

	public CitySetup(Vector3 cityCenter, float radius) {
		this.cityCenter = cityCenter;
		this.radius = radius;

		placement += StructuresInsideCircle;
		placement += PowerReachableStructures;
		structuresWithinReach = new List<BaseStructure> ();
	}

	// Delegate Methods

	void StructuresInsideCircle() {
		Collider[] hitColliders = Physics.OverlapSphere(cityCenter, radius);
		foreach (Collider col in hitColliders) {
			structuresWithinReach.Add (col.gameObject.GetComponent<BaseStructure> ());
		}
	}

	void PowerReachableStructures() {
		foreach (BaseStructure structure in structuresWithinReach) {
			structure.isPowered = true;
		}
	}

	// Public methods

	public void ShutdownUnreachableStructures() {
		foreach (BaseStructure structure in structuresWithinReach) {
			structure.isPowered = false;
		}
		structuresWithinReach.Clear ();
		StructuresInsideCircle ();
		PowerReachableStructures ();
	}

}
