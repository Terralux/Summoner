using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTrap : BaseSkill {

	public float range;
	public float force;
	public float duration;
	public GameObject magnetTrap;

	public MagnetTrap(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}

	public void PlaceTrap(Player player) {
		player.SpawnSkillPrefab (magnetTrap, range);
	}

	public void ExecuteAction(List<Entity> targets, Vector3 targetPos) {
		foreach (Entity e in targets) {
			//e.gameObject.AddComponent<Magnet> ();
		}
	}
}
