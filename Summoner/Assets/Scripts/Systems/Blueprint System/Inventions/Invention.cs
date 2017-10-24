using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
[CreateAssetMenu(fileName = "New Invention", menuName = "Trailblazer/Blueprint/Invention", order = 3)]
public class Invention : ScriptableObject {
	public Blueprint Material1;
	public Blueprint Material2;
	public Blueprint Material3;

	public BaseItem result;

	public Invention(Blueprint ingredient1, Blueprint ingredient2, Blueprint ingredient3, BaseItem result){
		Material1 = ingredient1;
		Material2 = ingredient2;
		Material3 = ingredient3;

		this.result = result;
	}

	public bool FindMatch(HashSet<Blueprint> bluePrintPatternFromPlayer) {
		// Possibly find a better way of preparing the collection for the comparer method?
		HashSet<Blueprint> localBlueprintPattern = new HashSet<Blueprint> ();
		localBlueprintPattern.Add (Material1);
		localBlueprintPattern.Add (Material2);
		localBlueprintPattern.Add (Material3);
		return ContainsSameBlueprints (bluePrintPatternFromPlayer, localBlueprintPattern);
	}

	bool ContainsSameBlueprints(HashSet<Blueprint> foreignBlueprintPattern, HashSet<Blueprint> localBlueprintPattern) {
		// Every blueprint in local collection must exist in foreign collection and their counts must be the same (3)
		// Using LINQ
		return localBlueprintPattern.All(foreignBlueprintPattern.Contains) && localBlueprintPattern.Count == foreignBlueprintPattern.Count;
	}
}