using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEntrance : BaseStructure {
	#region implemented abstract members of Interactive

	public float npcArrivalRate;
	public List<Citizen> pendingImmigrants = new List<Citizen> ();

	public override void OnInteract ()
	{
		Debug.Log ("No new immigrants. This building does not have an interaction :D");
	}

	#endregion

	void Awake() {
		if (isPowered) {
			InvokeRepeating ("NewArrival", npcArrivalRate, npcArrivalRate);
		}
	}

	void NewArrival() {
		pendingImmigrants.Add (new Citizen ());
	}

	void Shutdown(){
		CancelInvoke ();
		OnShutdown (this);
	}
}
