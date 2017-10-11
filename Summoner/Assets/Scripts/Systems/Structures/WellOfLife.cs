using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellOfLife : AutomaticStructure {

	public CitySetup citySetup;
	public float radius;

	void Awake () {
		citySetup = new CitySetup(transform.position, radius);
	}

	#region implemented abstract members of AutomaticStructure

	public override void Update (){
		if (isActive) {
			Player.instance.stats.AdjustHealth (1);
		}
	}

	#endregion

	void OnDrawGizmos() {
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}