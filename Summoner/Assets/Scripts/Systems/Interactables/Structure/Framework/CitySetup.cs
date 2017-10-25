using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySetup {

	Vector3 cityCenter;
	float radius;

	public List<BaseStructure> structuresWithinReach;

	public CitySetup(Vector3 cityCenter, float radius) {
		this.cityCenter = cityCenter;
		this.radius = radius;

		structuresWithinReach = new List<BaseStructure> ();
		UpdateStructureStates ();
	}

	public void AdjustStructureRegisterRange(float range){
		radius = range;
		UpdateStructureStates ();
	}

	void StructuresInsideCircle() {
		Collider[] hitColliders = Physics.OverlapSphere(cityCenter, radius);
		foreach (Collider col in hitColliders) {
			BaseStructure structure = col.gameObject.GetComponent<BaseStructure> ();
			if (structure != null) {
				structuresWithinReach.Add (structure);
			}
		}
	}

	void PowerReachableStructures() {
		foreach (BaseStructure structure in structuresWithinReach) {
			structure.isPowered = true;
		}
	}

	public void UpdateStructureStates() {
		foreach (BaseStructure structure in structuresWithinReach) {
			structure.isPowered = false;
		}
		structuresWithinReach.Clear ();
		StructuresInsideCircle ();
		PowerReachableStructures ();
	}

	public void RegisterStructure(BaseStructure structure){
		structure.OnShutdown += RemoveStructure;
	}

	public void RemoveStructure(BaseStructure structure){
		structuresWithinReach.Remove (structure);
	}

}