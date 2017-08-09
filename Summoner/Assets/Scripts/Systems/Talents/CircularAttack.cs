using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularAttack : BaseSkill {

	public float range;
	public int dmgMod;

	public CircularAttack(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon){
		
	}

	public void ExecuteAction(GameObject user, List<GameObject> targets) {
		// Fetch damage values to determine how much damage CA does to targets. Placeholder for now
		int placeholder = 1 * dmgMod;
		foreach (GameObject o in targets) {
			EnemyStats es = o.GetComponent<EnemyStats> ();
			es.AdjustHealth (-placeholder);
		}
	}
}
