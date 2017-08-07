using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Blueprint Collection", menuName = "Trailblazer/Blueprint/Blueprint Collection", order = 0)]
public class BlueprintsCollection : ScriptableObject {
	[SerializeField]
	public List<Blueprint> blueprints = new List<Blueprint>();

	public void EditBlueprint(Blueprint prev, Blueprint neo){
		if(IsValidBlueprint(neo)){
			blueprints[blueprints.IndexOf(prev)] = neo;
		}
	}

	public bool AddNewBlueprint(Blueprint neo){
		if(IsValidBlueprint(neo)){
			blueprints.Add(neo);
			return true;
		}
		return false;
	}

	private bool IsValidBlueprint(Blueprint neo){
		foreach(Blueprint b in blueprints){
			if(neo.blueprintName == b.blueprintName){
				return false;
			}
		}
		return true;
	}
}