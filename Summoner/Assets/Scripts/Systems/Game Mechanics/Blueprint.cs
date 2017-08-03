using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="Trailblazer/Blueprint", fileName="New Blueprint", order = 0)]
public class Blueprint : ScriptableObject{
	public string blueprintName;
	public Sprite image;
	public BlueprintCategories myCategory;
}