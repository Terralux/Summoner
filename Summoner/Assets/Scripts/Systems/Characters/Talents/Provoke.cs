using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provoke : BaseSkill {

	public float range;
	public float duration;

	public Provoke(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
	}
		
}
