using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BaseSkill {

	public float duration;
	public int bFactor;

	public Block(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
	}
}
