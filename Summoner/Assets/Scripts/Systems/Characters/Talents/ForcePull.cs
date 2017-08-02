using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePull : BaseSkill {

	public float range;

	public ForcePull(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {}
}
