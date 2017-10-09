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
	private List<Blueprint> collectedBlueprints = new List<Blueprint>();

	void Start () {
		if (InventoryMenu.instance != null) {
			InventoryMenu.playerInventory = inventory;
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
}