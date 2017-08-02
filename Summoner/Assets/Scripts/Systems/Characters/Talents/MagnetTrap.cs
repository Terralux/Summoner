using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTrap : BaseSkill {

	public float range;
	public float force;
	public float duration;

	public MagnetTrap(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}
}
