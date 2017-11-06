using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookGameplay : UtilityGameplay {	

	#region implemented abstract members of UtilityGameplay

	public override void Awake (){
		Debug.Log ("I received a Grappling Hook");
	}

	#endregion
}