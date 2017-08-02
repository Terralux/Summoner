using System.Collections;
using UnityEngine;

[System.Serializable]
public class Player : Entity {
	public CharacterStats stats;
	public Inventory inventory;

	private bool ValidateWeaponSlot{
		get{
			return equipped != null;
		}
		set{
			hasWeapon = value;
		}
	}
	private bool hasWeapon;

	public Weapon equipped;

	public Player(){
		stats = new CharacterStats();
		inventory = new Inventory ();
		hasWeapon = equipped != null;
	}

	public void SpawnSkillPrefab(GameObject prefab, float offset) {
		Instantiate (prefab, transform.position + (transform.forward * offset), Quaternion.LookRotation(transform.forward, Vector3.up));
	}
}