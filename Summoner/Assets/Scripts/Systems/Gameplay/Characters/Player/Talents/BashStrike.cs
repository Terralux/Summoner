using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashStrike : BaseSkill {

	public float knockbackF;
	public int damage;
	public float range;
	public float force;

	public BashStrike(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {

	}

	public void ExecuteAction(Vector3 direction, Entity target) {
		if (target as Enemy != null) {
			(target as Enemy).stats.AdjustHealth (-damage);
		} else if (target as Player != null) {
			(target as Player).stats.AdjustHealth (-damage);
		}

		Rigidbody rb = target.GetComponent<Rigidbody> ();
		rb.velocity = direction.normalized * force;
	}
}
