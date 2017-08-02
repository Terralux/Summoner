using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrap : BaseSkill {

	public float range;
	public float duration;
	public int pDmg;
	public float pDuration;

	public PoisonTrap(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}
}
