using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Entity {

	public static Player instance;

	public int discoveryLevel = 0;
	public CharacterStats stats;
	public TalentTree talentTree;
	public bool hasWeapon;

	public Weapon equipped;
	private static Inventory inventory;

	private List<Blueprint> collectedBlueprints = new List<Blueprint>();

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}

	public void SpawnSkillPrefab(GameObject prefab, float offset) {
		Instantiate (prefab, transform.position + (transform.forward * offset), Quaternion.LookRotation(transform.forward, Vector3.up));
	}

	public void AddBlueprint(Blueprint blueprint){
		if(!collectedBlueprints.Contains(blueprint)){
			collectedBlueprints.Add (blueprint);
		}
	}

	public Inventory GetInventory(){
		if (inventory == null) {
			inventory = new Inventory ();
		}

		return inventory;
	}
}