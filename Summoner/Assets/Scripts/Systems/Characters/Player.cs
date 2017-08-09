using System.Collections;
using UnityEngine;

[System.Serializable]
public class Player : Entity {
	public CharacterStats stats;
	public bool hasWeapon;

	public Weapon equipped;

	public Player(){
		stats = new CharacterStats();
		hasWeapon = equipped != null;
	}

	public void SpawnSkillPrefab(GameObject prefab, float offset) {
		Instantiate (prefab, transform.position + (transform.forward * offset), Quaternion.LookRotation(transform.forward, Vector3.up));
	}
}