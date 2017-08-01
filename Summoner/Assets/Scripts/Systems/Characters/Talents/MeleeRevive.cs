using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRevive : BaseSkill {

	public float range;
	public float rWithHealth;


	MeleeRevive(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}
}
