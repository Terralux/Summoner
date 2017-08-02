using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributes {
	public Hashtable elementalAffinities = new Hashtable();

	public WeaponAttributes(){
		foreach(Elementals e in System.Enum.GetValues(typeof(Elementals))){
			elementalAffinities.Add(e.ToString(), new ElementalAffinity(e, 0));
		}

	}
	public void Upgrade(ElementalAffinity e){
		(elementalAffinities [e.targetElement.ToString ()] as ElementalAffinity).value += e.value;
	}

	public bool IsLessThan(WeaponAttributes wAtt){
		foreach(Elementals e in System.Enum.GetValues(typeof(Elementals))){
			if((elementalAffinities [e.ToString ()] as ElementalAffinity).value > 
				(wAtt.elementalAffinities[e.ToString()] as ElementalAffinity).value){
				return false;
			}
		}
		return true;
	}
}