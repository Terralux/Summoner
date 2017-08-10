using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BaseSkill {

	public float duration;
	[Range(0f, 0.9f)]
	public float bFactor;

	public Block(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		
	}

	public int ExecuteAction(int dmgValue) {
		return (Mathf.RoundToInt(dmgValue * ( 1 - bFactor ) ));		
	}
}
