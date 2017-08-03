using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSkill {

	public string name;
	public float cooldown;
	public int skillIndex;
	public Sprite icon;


	public BaseSkill(string name, float cooldown, int skillIndex, Sprite icon){
		this.name = name;
		this.cooldown = cooldown;
		this.skillIndex = skillIndex;
		this.icon = icon;
	}

}