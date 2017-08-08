using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	public EnemyStats stats;

	public Enemy(){
		stats = new EnemyStats();
	}
}