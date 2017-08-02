﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable, CreateAssetMenu(fileName="New Weapon", menuName = "Trailblazer/Items/Weapon", order=2)]
public partial class Weapon : BaseItem {
	public GameObject weaponPrefab;

	protected int currentExperience = 0;
	protected int experienceToNextLevel = 100;
	protected int currentLevel = 1;
	protected int currentUpgradePoints = 0;
	protected int weaponDamage = 1;

	protected WeaponAttributes attributes;

	public List<WeaponUpgrade> upgradeReq = new List<WeaponUpgrade>();

	public bool isReadyForUpgrade = false;

	public void Init(){
		attributes = new WeaponAttributes();
	}

	public static Weapon CreateInstance()
	{
		Weapon w = ScriptableObject.CreateInstance<Weapon>();
		w.Init();
		return w;
	}

	public void AddExperience(int gainedExperience){
		currentExperience += gainedExperience;
		if(currentExperience >= experienceToNextLevel){
			currentExperience = currentExperience - experienceToNextLevel;
			LevelUp();
		}
	}

	public void LevelUp(){
		currentLevel++;
		currentUpgradePoints += 3;
		experienceToNextLevel = (int)(experienceToNextLevel * 1.1f);
	}

	public void AddAffinity(ElementalAffinity e){
		if (currentUpgradePoints > 0) {
			attributes.Upgrade (e);
			currentUpgradePoints--;

			foreach (WeaponUpgrade wUp in upgradeReq) {
				if (wUp.CheckRequirements (attributes)) {
					isReadyForUpgrade = true;
				}
			}
		}
	}

}

public partial class Weapon{
	public static Weapon none{
		get{
			return new Weapon();
		}
	}
}