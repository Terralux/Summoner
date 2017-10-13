using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellOfLife : AutomaticStructure {

	public static WellOfLife instance;

	public int citizenCount;
	public static CitySetup citySetup;
	public float radius;

	void Awake () {
		if (instance != null) {
			Destroy (instance);
		} else {
			instance = this;
			citySetup = new CitySetup (transform.position, radius);
		}
	}

	#region implemented abstract members of AutomaticStructure

	protected override void Update (){
		if (isActive) {
			Player.instance.stats.AdjustHealth (1);
		}
	}

	#endregion

	void OnDrawGizmos() {
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}