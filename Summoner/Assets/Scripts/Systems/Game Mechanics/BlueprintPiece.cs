using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "All Blueprints", menuName = "Blueprints", order = 0)]
public class BlueprintPiece : ScriptableObject {

	[SerializeField]
	public List<BlueprintMats> blueprints;
}
