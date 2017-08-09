using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrap : BaseSkill {

	public float range;
	public float duration;
	public int dmgPerTick;
	public int tickRate;
	public float pDuration;
	public GameObject poisonTrap;

	public PoisonTrap(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}

	public void PlaceTrap(Player player) {
		// When skill is used first, call this method. I.e define placement behavior here or elsewhere
		player.SpawnSkillPrefab(poisonTrap, range);
	}

	public void ExecuteAction(Entity target) {
		// When trap is triggered change status of player to poisoned. Define poison behavior elsewhere?
		AdjustHealthOverTime aHOT = target.gameObject.AddComponent<AdjustHealthOverTime>();
		aHOT.Duration = duration;
		aHOT.TickRate = tickRate;
		aHOT.ValuePerTick = dmgPerTick;
	}
}
