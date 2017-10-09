using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="Trailblazer/Blueprint/Blueprint", fileName="New Blueprint", order = 0)]
public class Blueprint : ScriptableObject {
	public string blueprintName;
	public Sprite image;
	public BlueprintCategories myCategory;

	public Blueprint (string name, Sprite icon, BlueprintCategories category){
		blueprintName = name;
		image = icon;
		myCategory = category;
	}
}