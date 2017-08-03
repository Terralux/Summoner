using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats : CharacterStats {

	[SerializeField]
	private float damageModifier;

	[SerializeField]
	private int expValue{
		get;
	}

	public Hashtable elementalAffinities = new Hashtable();

	public EnemyStats(){
		foreach(Elementals e in System.Enum.GetValues(typeof(Elementals))){
			elementalAffinities.Add(e.ToString(), new ElementalAffinity(e, 0));
		}
	}

	public int GetDamageValue(){
		int damage = (int)(damageModifier * (float)baseDamage);
		return damage;
	}

	public bool TakeDamage(int damageValue, ElementalAffinity elementalAffinity){
		if(elementalAffinities.ContainsKey(elementalAffinity.targetElement.ToString())){
			float affinity = ((float)((ElementalAffinity)elementalAffinities[elementalAffinity.targetElement.ToString()]).value / 256f) - ((float)elementalAffinity.value / 256f);
			damageValue = (int)(damageValue * affinity);
		}
		return AdjustHealth(damageValue);
	}
}