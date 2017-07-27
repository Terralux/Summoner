using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="Trailblazer/Blueprint Material", fileName="New Blueprint Material", order = 0)]
public class BlueprintMats : ScriptableObject{
	public string blueprintName;
	public Sprite image;
	public Category myCategory;
}

public enum Category{
	MachineParts,
	Vegetation,
	StructureParts,
	WeaponParts,
	MysteryObjects
}