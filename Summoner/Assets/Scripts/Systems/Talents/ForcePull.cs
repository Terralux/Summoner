using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePull : BaseSkill {

	public float range;
	public float force;

	public ForcePull(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}

	public void ExecuteAction(Vector3 userPos, Entity target) {
		Rigidbody rb = target.GetComponent<Rigidbody> ();
		rb.velocity = userPos * force;
	}
}
