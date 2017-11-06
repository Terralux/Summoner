using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName="New Utility", menuName = "Trailblazer/Items/Utility", order=5)]
public class Utility : Useables {

	public UtilityGameplay assignedUtility;

	#region implemented abstract members of Useables

	public override void OnActivateFromMenu (){
		Player.instance.gameObject.AddComponent (assignedUtility.GetType ());
	}

	#endregion

}