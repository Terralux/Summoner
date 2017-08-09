using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : BaseSkill {

	public float duration;
	public int counterDmgMod;

	public Counter(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}

	public void ExecuteAction(Entity target) {
		// Only players can counter?
		if (target as Enemy != null) {
			int enemyDmg = (target as Enemy).stats.GetDamageValue ();
			int counterDmg = enemyDmg * counterDmgMod;
			(target as Enemy).stats.AdjustHealth (-counterDmg);
		} 
	}
}
