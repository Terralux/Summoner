using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Weapon : MaterialComponentItem {
	public GameObject weaponPrefab;

	protected int currentExperience = 0;
	protected int experienceToNextLevel = 100;
	protected int currentLevel = 1;
	protected int currentUpgradePoints = 0;
	protected int weaponDamage = 1;

	protected WeaponAttributes attributes;

	public List<WeaponUpgrade> upgradeReq = new List<WeaponUpgrade>();

	private void Init(){
		attributes = new WeaponAttributes();
	}

	public static new Weapon CreateInstance(){
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

	public void Upgrade(ElementalAffinity e){
		if (currentUpgradePoints > 0) {
			attributes.Upgrade (e);
			currentUpgradePoints--;
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