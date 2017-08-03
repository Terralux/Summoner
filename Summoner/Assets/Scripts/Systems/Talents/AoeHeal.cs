using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeHeal : BaseSkill {

	public float range;
	public int hAmount;

	public AoeHeal(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon){
		
	}
		
	public void ExecuteAction(List<Entity> targets) {
		foreach (Entity e in targets) {
			//If entity is player: Adjust health of players by + hAmount
			if (e as Player != null) {
				(e as Player).stats.AdjustHealth (hAmount);
			}
		}
	}

}
