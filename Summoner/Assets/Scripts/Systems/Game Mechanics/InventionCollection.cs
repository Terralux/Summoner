using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Invention Collection", menuName = "Trailblazer/Blueprints", order = 0)]
public class InventionCollection : ScriptableObject {
	[SerializeField]
	public List<Invention> inventions = new List<Invention>();
}