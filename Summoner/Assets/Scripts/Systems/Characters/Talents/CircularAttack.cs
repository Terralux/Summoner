using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularAttack : BaseSkill {

	public float range;
	public int dmgMod;

	public CircularAttack(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon){
	}
}
