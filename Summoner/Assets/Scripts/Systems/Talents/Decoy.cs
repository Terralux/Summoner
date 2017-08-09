using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : BaseSkill {

	public float range;
	public float duration;
	public int health;
	public GameObject decoyPrefab;

	public Decoy(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}

	public void ExecuteAction(Player player) {
		player.SpawnSkillPrefab (decoyPrefab, range);
	}
}
