using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : BaseSkill {

	public float range;
	public float sDuration;
	public int targets;
	public float angle;

	public Stun(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
	}
}
