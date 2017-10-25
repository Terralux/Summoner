using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Utility", menuName = "Trailblazer/Items/Utility", order=5)]
public class Utility : Useables {
	#region implemented abstract members of Useables

	public override void OnActivateFromMenu ()
	{
		throw new System.NotImplementedException ();
	}

	#endregion

}