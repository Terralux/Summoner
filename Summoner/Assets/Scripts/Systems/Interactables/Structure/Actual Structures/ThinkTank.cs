using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkTank : AutomaticAndInteractiveStructure {

	public HashSet<Blueprint> blueprintCollection;

	#region implemented abstract members of AdvancedStructure

	protected override void Update ()
	{
		if (isActive) {
			AbsorbBlueprintsFromPlayer ();
		}
	}

	public override void OnActionInteract ()
	{
		// Toggle menu that shows blueprint combination options
	}

	#endregion

	void AbsorbBlueprintsFromPlayer() {
		foreach (Blueprint blueprint in Player.instance.collectedBlueprints) {
			int count = blueprintCollection.Count;
			blueprintCollection.Add (blueprint);
			// If a new blueprint was inserted (if already exists it's discarded) then remove from Player collection
			if (blueprintCollection.Count > count) {
				Player.instance.RemoveBlueprint (blueprint);
			}
		}
	}

	void MergeBlueprints() {
		// Blueprint combination into a new structure
	}

}
