using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponUpgrade{

	public WeaponAttributes upgradeRequirements;
	public Weapon weapon;

	private bool requirementsMet = false;

	public bool CheckRequirements(WeaponAttributes wAtt){
		return (requirementsMet = upgradeRequirements.IsLessThan (wAtt));
	}
}
