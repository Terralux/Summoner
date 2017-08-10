using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Invention Collection", menuName = "Trailblazer/Blueprint/Invention Collection", order = 0)]
public class InventionCollection : ScriptableObject {
	[SerializeField]
	public List<Invention> inventions = new List<Invention>();

	public void EditInvention(Invention prev, Invention neo){
		if(IsValidInvention(neo)){
			inventions[inventions.IndexOf(prev)] = neo;
		}
	}

	public bool AddNewInvention(Invention i){
		if(IsValidInvention(i)){
			inventions.Add(i);
			return true;
		}
		return false;
	}

	private bool IsValidInvention(Invention i){

		if(inventions.Contains(i)){
			return false;
		}

		foreach(Invention invention in inventions){
			if(invention.Material1 == i.Material1 || invention.Material1 == i.Material2 || invention.Material1 == i.Material3){
				if(invention.Material2 == i.Material1 || invention.Material2 == i.Material2 || invention.Material2 == i.Material3){
					if(invention.Material3 == i.Material1 || invention.Material3 == i.Material2 || invention.Material3 == i.Material3){
						return false;
					}
				}
			}
		}

		return true;
	}
}