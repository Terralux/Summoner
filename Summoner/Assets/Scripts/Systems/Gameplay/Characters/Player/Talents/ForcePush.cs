using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : BaseSkill {

	public int force;

	public ForcePush(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}

	public void ExecuteAction(List<Entity> targets, Vector3 direction) {
		foreach (Entity e in targets) {
			Rigidbody rb = e.GetComponent<Rigidbody> ();
			rb.velocity = direction.normalized * force;
		}
	}
}
