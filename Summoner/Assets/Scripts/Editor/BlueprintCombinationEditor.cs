using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlueprintCombinationEditor : EditorWindow {

	public string myString;
	bool myBool;
	float myFloat;

	private static InventionCollection inventionCollection;
	private static BlueprintsCollection blueprintCollection;

	private Blueprint ingredient1;
	private Blueprint ingredient2;
	private Blueprint ingredient3;

	private BaseItem result;

	private string blueprintName;
	private Sprite icon;
	private BlueprintCategories category;

	private bool currentlyInCombinationTab = false;
	private Invention currentlySelected;
	private Blueprint currentlySelectedBlueprint;

	[MenuItem("Window/Trailblazer/Blueprint Combination Editor %&b")]
	static void Init(){
		BlueprintCombinationEditor window = (BlueprintCombinationEditor)EditorWindow.GetWindow(typeof(BlueprintCombinationEditor));
		blueprintCollection = (BlueprintsCollection) AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/BlueprintCollection.asset", typeof(BlueprintsCollection));
		inventionCollection = (InventionCollection) AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/BlueprintCombinations.asset", typeof(InventionCollection));
		window.currentlySelected = null;
		window.currentlySelectedBlueprint = null;
		window.Show();
	}

	void OnGUI(){
		currentlyInCombinationTab = GUILayout.Toolbar (currentlyInCombinationTab? 1 : 0, new string[] {"Blueprint", "Blueprint Combinations"}) > 0;

		if(currentlyInCombinationTab){

			if(inventionCollection == null){
				GUILayout.Label("Your invention collection is missing, contact a programmer!");
				return;
			}

			GUILayout.BeginHorizontal();

			GUILayout.BeginVertical(GUILayout.Width((this.position.size.x / 3) - 20));

			foreach(Invention i in inventionCollection.inventions){
				if(i.result != null){
					if(i.result.itemName != "" && i.result.itemName.Trim() != ""){
						if(i == currentlySelected){
							GUILayout.Label(i.result.itemName);
						}else{
							if(GUILayout.Button(i.result.itemName)){
								SelectInvention(i);
							}
						}
					}else{
						if(i == currentlySelected){
							GUILayout.Label("UNNAMED: " + i.result.name);
						}else{
							if(GUILayout.Button("UNNAMED: " + i.result.name)){
								SelectInvention(i);
							}
						}
					}
				}else{
					inventionCollection.inventions.Remove(i);
				}
			}
			
			GUILayout.EndVertical();

			GUILayout.BeginVertical(GUILayout.Width((this.position.size.x / 3) * 2));

			GUILayout.Label("Blueprint Combination", EditorStyles.boldLabel);

			ingredient1 = (Blueprint) EditorGUILayout.ObjectField("Blueprint 1", ingredient1, typeof(Blueprint), false);
			ingredient2 = (Blueprint) EditorGUILayout.ObjectField("Blueprint 2", ingredient2, typeof(Blueprint), false);
			ingredient3 = (Blueprint) EditorGUILayout.ObjectField("Blueprint 3", ingredient3, typeof(Blueprint), false);

			GUILayout.Space(10);

			result = (BaseItem) EditorGUILayout.ObjectField("Result", result, typeof(BaseItem), false);

			if(result != null && ingredient1 != null && ingredient2 != null && ingredient3 != null){
				if(result != ingredient1 && result != ingredient2 && result != ingredient3){
					if(ingredient1 != ingredient2 && ingredient1 != ingredient3 && ingredient2 != ingredient3){
						if(currentlySelected != null){
							if(GUILayout.Button("Edit Invention")){
								inventionCollection.EditInvention(currentlySelected, new Invention (ingredient1, ingredient2, ingredient3, result));
								
								currentlySelected = null;

								ingredient1 = null;
								ingredient2 = null;
								ingredient3 = null;

								result = null;
							}
							if(GUILayout.Button("Delete Invention")){
								inventionCollection.inventions.Remove(currentlySelected);
								currentlySelected = null;

								ingredient1 = null;
								ingredient2 = null;
								ingredient3 = null;

								result = null;
							}
						}else{
							if(GUILayout.Button("Add Invention")){
								if(inventionCollection.AddNewInvention(new Invention (ingredient1, ingredient2, ingredient3, result))){
									currentlySelected = null;

									ingredient1 = null;
									ingredient2 = null;
									ingredient3 = null;

									result = null;
								}
							}
						}
					}
				}
			}

			GUILayout.EndVertical();

			GUILayout.EndHorizontal();
		}else{

			if(blueprintCollection == null){
				GUILayout.Label("Your blueprint collection is missing, contact a programmer!");
				return;
			}

			GUILayout.BeginHorizontal();

			GUILayout.BeginVertical(GUILayout.Width((this.position.size.x / 3) - 20));

			foreach(Blueprint b in blueprintCollection.blueprints){
				if(b != null){
					if(b.blueprintName != "" && b.blueprintName.Trim() != ""){
						if(b == currentlySelectedBlueprint){
							GUILayout.Label(b.blueprintName);
						}else{
							if(GUILayout.Button(b.blueprintName)){
								SelectBlueprint(b);
							}
						}
					}else{
						if(b == currentlySelectedBlueprint){
							GUILayout.Label("UNNAMED: " + b.name);
						}else{
							if(GUILayout.Button("UNNAMED: " + b.name)){
								SelectBlueprint(b);
							}
						}
					}
				}else{
					blueprintCollection.blueprints.Remove(b);
				}
			}

			GUILayout.EndVertical();

			GUILayout.BeginVertical(GUILayout.Width((this.position.size.x / 3) * 2));

			GUILayout.Label("Blueprint Combination", EditorStyles.boldLabel);

			blueprintName = EditorGUILayout.TextField("Blueprint name", blueprintName);
			icon = (Sprite) EditorGUILayout.ObjectField("Blueprint Icon", icon, typeof(Sprite), false);
			category = (BlueprintCategories) EditorGUILayout.EnumPopup("Blueprint category", category);

			if(currentlySelectedBlueprint != null){
				if(GUILayout.Button("Edit Blueprint")){
					blueprintCollection.EditBlueprint(currentlySelectedBlueprint, new Blueprint (blueprintName, icon, category));

					currentlySelectedBlueprint = null;

					blueprintName = "";
					icon = null;
					category = BlueprintCategories.MACHINEPARTS;
				}
				if(GUILayout.Button("Delete Blueprint")){
					blueprintCollection.blueprints.Remove(currentlySelectedBlueprint);
					currentlySelectedBlueprint = null;

					blueprintName = "";
					icon = null;
					category = BlueprintCategories.MACHINEPARTS;
				}
			}else{
				if(GUILayout.Button("Add Blueprint")){
					if(blueprintCollection.AddNewBlueprint(new Blueprint (blueprintName, icon, category))){
						currentlySelectedBlueprint = null;

						blueprintName = "";
						icon = null;
						category = BlueprintCategories.MACHINEPARTS;
					}
				}
			}

			GUILayout.EndVertical();

			GUILayout.EndHorizontal();
		}
	}

	private void SelectInvention(Invention i){
		ingredient1 = i.Material1;
		ingredient2 = i.Material2;
		ingredient3 = i.Material3;

		result = i.result;

		currentlySelected = i;
	}

	private void SelectBlueprint(Blueprint b){
		currentlySelectedBlueprint = b;
	}
}