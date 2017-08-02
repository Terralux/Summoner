using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blueprint combinations", menuName = "Trailblazer/Blueprint Combo", order = 0)]
public class BlueprintCombinations : BlueprintCombos {
	[SerializeField]
	public List<BlueprintCombos> combos = new List<BlueprintCombos>();

}