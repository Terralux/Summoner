using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestStructure : InteractiveStructure {

	#region implemented abstract members of Interactive
	public override void OnInteract (){
		Debug.Log ("Hello world I am a Building! :D");
		Debug.Log (isPowered);
	}
	#endregion
}