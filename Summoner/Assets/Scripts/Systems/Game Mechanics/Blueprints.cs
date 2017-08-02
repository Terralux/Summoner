using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Blueprint Collection", menuName = "Trailblazer/Blueprint Collection", order = 0)]
public class Blueprints : ScriptableObject {
	[SerializeField]
	public List<BlueprintMats> blueprints;
}