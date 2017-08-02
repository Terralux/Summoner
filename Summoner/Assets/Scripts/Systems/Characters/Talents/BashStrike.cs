using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashStrike : BaseSkill {

	public float knockbackF;
	public int damage;
	public float range;

	public BashStrike(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
	}
}
