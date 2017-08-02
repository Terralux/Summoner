using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseWall : BaseSkill {

	public float health;
	public float duration;
	public float range;
	public GameObject wallPrefab;

	public RaiseWall(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}

	public void ExecuteAction(Player player) {
		player.SpawnSkillPrefab (wallPrefab, range);
	}
}
