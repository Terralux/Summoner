using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestStructure : BaseStructure {
	#region implemented abstract members of BaseStructure

	public override void OnInteract ()
	{
		Debug.Log ("You are interacting with me!");
	}

	#endregion

}