using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTrap : BaseSkill {

	public float range;
	public float duration;
	public int hAmount;

	public HealTrap(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon){
		
	}
}
