using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributes : MonoBehaviour {
	public Hashtable elementalAffinities = new Hashtable();

	public WeaponAttributes(){
		foreach(Elementals e in System.Enum.GetValues(typeof(Elementals))){
			elementalAffinities.Add(e.ToString(), new ElementalAffinity(e, 0));
		}
	}
}