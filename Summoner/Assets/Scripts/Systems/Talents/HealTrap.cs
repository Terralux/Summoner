using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTrap : BaseSkill {

	public float range;
	public float duration;
	public int hAmount;
	public GameObject healTrap;

	public HealTrap(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon){
		
	}

	public void PlaceTrap(Player player) {
		player.SpawnSkillPrefab (healTrap, range);
	}

	public void ExecuteAction(Entity target) {
		if (target as Player != null) {
			(target as Player).stats.AdjustHealth (hAmount);
		} else if (target as Enemy != null) {
			(target as Enemy).stats.AdjustHealth (hAmount);
		}
	}
}
