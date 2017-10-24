using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InventionStructure : InteractiveStructure
{

	// Public fields
	public int numberOfItemsNeeded = 3;

	// Private fields
	HashSet<Invention> inventionsDiscovered = new HashSet<Invention> ();
	InventionCollection inventionCollection = (InventionCollection)AssetDatabase.LoadAssetAtPath ("Assets/ScriptableObjects/BlueprintCombinations.asset", typeof(InventionCollection));

	#region implemented abstract members of Interactive

	public override void OnInteract () {
		// Toggle UI for combining blue prints

	}

	#endregion

	/// <summary>
	/// Method to find an invention that matches the combination of blueprints built by the player using appropriate UI. Call the method on submit
	/// </summary>
	/// <returns>Invention. If no match returns null</returns>
	/// <param name="blueprintPattern">Blueprint pattern.</param>
	public Invention FindMatch(HashSet<Blueprint> blueprintPattern) {
		foreach (Invention invention in inventionCollection.inventions) {
			if (invention.FindMatch (blueprintPattern)) {
				return invention;
			}
		}
		return null;
	}

}
