using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats {
	public float moveSpeed;
	public float rotSpeed;
	public float jumpForce;

	[HideInInspector]
	protected int baseDamage = 3;
	
	public int maxHP = 100;
	private int curHP = 100;

	/// <summary>
	/// Adjusts the health.
	/// </summary>
	/// <returns><c>true</c>, if health reached 0, <c>false</c> otherwise.</returns>
	/// <param name="amount">Amount.</param>
	public bool AdjustHealth(int amount){
		curHP += amount;

		if(curHP < 1){
			curHP = 0;
			return true;
		}
		if(curHP > maxHP){
			curHP = maxHP;
		}
		return false;
	}
}