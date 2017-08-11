using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Entity {
	public CharacterStats stats;
	public TalentTree talentTree;
	public bool hasWeapon;

	public Weapon equipped;
	public Inventory inventory;

	public Player(){
		stats = new CharacterStats();
		inventory = new Inventory ();
		talentTree = new TalentTree ();
		hasWeapon = equipped != null;
	}

	public void SpawnSkillPrefab(GameObject prefab, float offset) {
		Instantiate (prefab, transform.position + (transform.forward * offset), Quaternion.LookRotation(transform.forward, Vector3.up));
	}
}