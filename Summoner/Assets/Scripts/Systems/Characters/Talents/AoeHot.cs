using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeHot : BaseSkill {

	public float range;
	public float hPercent;
	public float duration;

	public AoeHot(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}
}
