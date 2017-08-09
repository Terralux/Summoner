using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlueprintCombinationEditor : EditorWindow {

	public string myString;
	bool myBool;
	float myFloat;

	private static InventionCollection inventions;

	private Blueprint ingredient1;
	private Blueprint ingredient2;
	private Blueprint ingredient3;

	[MenuItem("Window/Trailblazer/Blueprint Combination Editor")]
	static void Init(){
		BlueprintCombinationEditor window = (BlueprintCombinationEditor)EditorWindow.GetWindow(typeof(BlueprintCombinationEditor));
		inventions = (InventionCollection) AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/BlueprintCombinations.asset", typeof(InventionCollection));
		window.Show();
	}

	void OnGUI(){
		if(inventions == null){
			return;
		}

		/*
		 * option to add material 1, 2 and 3
		 * option to add a result
		*/

		GUILayout.Label("Blueprint Combination", EditorStyles.boldLabel);

		ingredient1 = (Blueprint) EditorGUI.ObjectField(new Rect(3, 3, position.width - 6, 20), "Find Dependency", ingredient1, typeof(Blueprint));

	}

}