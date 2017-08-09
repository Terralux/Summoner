using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRevive : BaseSkill {

	public float range;
	[Range(0f,1f)]
	public float rWithHealthPercent;


	MeleeRevive(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}

	public void ExecuteAction(List<Player> targets) {
		foreach (Player p in targets) {
			p.stats.AdjustHealth (Mathf.RoundToInt (p.stats.maxHP * rWithHealthPercent));
			p.Resurrect ();
		}
	}
}
