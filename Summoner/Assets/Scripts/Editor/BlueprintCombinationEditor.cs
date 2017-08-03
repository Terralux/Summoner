using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlueprintCombinationEditor : EditorWindow {

	public string myString;
	bool myBool;
	float myFloat;

	private static InventionCollection inventionCollection;

	private Blueprint ingredient1;
	private Blueprint ingredient2;
	private Blueprint ingredient3;

	private BaseItem result;

	private bool currentlyInCombinationTab = false;

	[MenuItem("Window/Trailblazer/Blueprint Combination Editor %&b")]
	static void Init(){
		BlueprintCombinationEditor window = (BlueprintCombinationEditor)EditorWindow.GetWindow(typeof(BlueprintCombinationEditor));
		inventionCollection = (InventionCollection) AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/BlueprintCombinations.asset", typeof(InventionCollection));
		window.Show();
	}

	void OnGUI(){
		if(inventionCollection == null){
			GUILayout.Label("Your invention collection is missing, contact a programmer!");
			return;
		}

		currentlyInCombinationTab = GUILayout.Toolbar (currentlyInCombinationTab? 1 : 0, new string[] {"Blueprint", "Blueprint Combinations"}) > 0;

		if(currentlyInCombinationTab){
			GUILayout.BeginHorizontal();

			GUILayout.BeginVertical();

			foreach(Invention i in inventionCollection.inventions){
				if(i.result != null){
					GUILayout.Button(i.result.itemName);
				}else{
					inventionCollection.inventions.Remove(i);
				}
			}
			
			GUILayout.EndVertical();

			GUILayout.BeginVertical();

			GUILayout.Label("Blueprint Combination", EditorStyles.boldLabel);

			ingredient1 = (Blueprint) EditorGUILayout.ObjectField("Blueprint 1", ingredient1, typeof(Blueprint), false);
			ingredient2 = (Blueprint) EditorGUILayout.ObjectField("Blueprint 2", ingredient2, typeof(Blueprint), false);
			ingredient3 = (Blueprint) EditorGUILayout.ObjectField("Blueprint 3", ingredient3, typeof(Blueprint), false);

			GUILayout.Space(10);

			result = (BaseItem) EditorGUILayout.ObjectField("Result", result, typeof(BaseItem), false);

			if(result != null && ingredient1 != null && ingredient2 != null && ingredient3 != null){
				if(result != ingredient1 && result != ingredient2 && result != ingredient3){
					if(ingredient1 != ingredient2 && ingredient1 != ingredient3 && ingredient2 != ingredient3){
						if(GUILayout.Button("Add Invention")){
							inventionCollection.inventions.Add(new Invention (ingredient1, ingredient2, ingredient3, result));
						}
					}
				}
			}

			GUILayout.EndVertical();

			GUILayout.EndHorizontal();
		}else{
			
		}
	}
}