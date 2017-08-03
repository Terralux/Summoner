using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : BaseSkill {

	public float range;
	public int numTargsHit;
	public int dmgMod;
	private int damage;
	public int thrustSpeed;

	public Pierce(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		// Fetch damage values to determine how much damage CA does to targets. Placeholder for now
		damage = 1 * dmgMod;
	}

	public void ExecuteAction(List<Entity> targets, Entity user, Vector3 direction, bool isRanged) {
		foreach (Entity e in targets) {
			if (e as Player != null) {
				(e as Player).stats.AdjustHealth (-damage);
			} else if (e as Enemy != null) {
				(e as Enemy).stats.AdjustHealth (-damage);
			}
		}
		if (!isRanged) {
			Rigidbody rb = user.GetComponent<Rigidbody> ();
			rb.velocity = direction * thrustSpeed;
		}
	}
}
