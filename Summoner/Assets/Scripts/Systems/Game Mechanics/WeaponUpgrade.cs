using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponUpgrade{

	public WeaponAttributes wepAtt;
	public Weapon weapon;

	bool requirementsMet = false;

	public bool CheckRequirements(WeaponAttributes wAtt){
		return (requirementsMet = wepAtt.IsLessThan (wAtt));
	}
}
