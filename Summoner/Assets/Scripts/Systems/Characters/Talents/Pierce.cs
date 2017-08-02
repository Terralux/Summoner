using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : BaseSkill {

	public float range;
	public int numTargsHit;
	public int dmgMod;

	public Pierce(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {}
}
