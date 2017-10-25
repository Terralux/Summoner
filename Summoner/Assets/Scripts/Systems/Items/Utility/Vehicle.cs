using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Vehicle", menuName = "Trailblazer/Items/Vehicle", order=4)]
public class Vehicle : Useables {
	#region implemented abstract members of Useables

	public override void OnActivateFromMenu ()
	{
		// Choose one: Attach a script to player or instantiate prefab (prefab has behavior)
	}

	#endregion


}