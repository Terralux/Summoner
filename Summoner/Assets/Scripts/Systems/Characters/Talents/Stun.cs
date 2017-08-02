using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : BaseSkill {

	public float range;
	public float sDuration;
	public float angle;

	public Stun(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}

	public void ExecuteAction(List<Entity> targets) {
		foreach (Entity e in targets) {
			// Set targets hit as stun. Define stun behavior elsewhere?
			e.SetStunned(sDuration);
		}
	}
}
