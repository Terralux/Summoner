using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributes : MonoBehaviour {

	int baseDmg;
	float durability;

	private ElementalAffinity elemDmg;

	public WeaponAttributes (ElementalAffinity elemDmg){
		this.elemDmg = elemDmg;
	}
}
