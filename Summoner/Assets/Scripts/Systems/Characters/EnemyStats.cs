using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats {

	public float moveSpeed;
	public float jumpForce;
	public float rotSpeed;

	public int hp;
	public float baseDmg;
	public int expValue;

	private ElementalAffinity elemDmg;

	public EnemyStats (ElementalAffinity elemDmg){
		this.elemDmg = elemDmg;
	}
}
