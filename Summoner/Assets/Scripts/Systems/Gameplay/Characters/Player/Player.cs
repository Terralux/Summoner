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
	public static Inventory inventory;
	public HashSet<Blueprint> collectedBlueprints = new HashSet<Blueprint>();

	public BaseItem[] testItems;

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
		collectedBlueprints.Add (blueprint);
	}

	public void RemoveBlueprint(Blueprint blueprint) {
		collectedBlueprints.Remove (blueprint);
	}

	public Inventory GetInventory(){
		if (inventory == null) {
			inventory = new Inventory ();

			for (int i = 0; i < testItems.Length * 4; i++) {
				inventory.AddItem (testItems [Random.Range (0, testItems.Length)], Random.Range (3, 13));
			}
		}

		return inventory;
	}
}