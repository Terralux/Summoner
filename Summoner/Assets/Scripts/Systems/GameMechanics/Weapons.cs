using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Weapons : Items{
	Sprite image;
	string name;
	GameObject wepPrefab;

	int exp;
	int expTNL;
	int lvl;
	int feedPoints;

	private WeaponAttributes atr;

	public Weapons(WeaponAttributes atr){
		this.atr = atr;
	}
}