using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentShrine : BaseTreasure {

	#region implemented abstract members of BaseTreasure

	public override void OnInteract ()
	{
		if (!hasBeenUsed) {
			Player.instance.inventory.AddItem (item, quantity);
			hasBeenUsed = true;
			Player.instance.talentTree.AdjustTalentPoints (1);
			Debug.Log ("Skill point was added");
		}
	}

	#endregion
}
