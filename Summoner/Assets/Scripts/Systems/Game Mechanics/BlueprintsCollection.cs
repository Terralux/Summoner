using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Blueprint Collection", menuName = "Trailblazer/Blueprint Collection", order = 0)]
public class BlueprintsCollection : ScriptableObject {
	[SerializeField]
	public List<Blueprint> blueprints;
}