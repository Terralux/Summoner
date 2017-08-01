using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : BaseSkill {

	public float duration;
	public int counterDmgMod;

	public Counter(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}
}
