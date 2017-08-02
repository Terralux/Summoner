using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustHealthOverTime : MonoBehaviour {

	private float duration;
	public float Duration {
		get { return duration; } set { duration = value; }
	}

	private int tickRate;
	public int TickRate {
		get { return tickRate; } set { tickRate = value; }
	}

	private int valuePerTick;
	public int ValuePerTick {
		get { return valuePerTick; } set { valuePerTick = value; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
