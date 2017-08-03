using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeHot : BaseSkill {

	public float range;
	public int hAmount;
	private int hPerTick;
	public int tickRate;
	public float duration;

	public AoeHot(string name, float cooldown, int skillIndex, Sprite icon):base(name, cooldown, skillIndex, icon) {
		hPerTick = Mathf.RoundToInt (hAmount / duration);
	}

	public void ExecuteAction(List<Entity> targets) {
		foreach (Entity e in targets) {
			AdjustHealthOverTime aHOT = e.gameObject.AddComponent<AdjustHealthOverTime> ();
			aHOT.Duration = duration;
			aHOT.TickRate = tickRate;
			aHOT.ValuePerTick = hPerTick;
		}
	}
}
