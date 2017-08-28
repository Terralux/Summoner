using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMaster : MonoBehaviour{

	private List<PassiveState> passState = new List<PassiveState>();
	private List<OffensiveState> offState = new List<OffensiveState>();
	private List<DefensiveState> defState = new List<DefensiveState>();

	private bool playerIsDetected = true;

	void Awake() {
		Animator anim = GetComponent<Animator> ();
		Rigidbody rb = GetComponent<Rigidbody> ();
		NavMeshAgent agent = GetComponent<NavMeshAgent> ();

		State[] states = GetComponents<State> ();
		foreach (State state in states) {
			if (state as PassiveState != null) {
				passState.Add ( (state as PassiveState ));
			} else if (state as OffensiveState != null) {
				offState.Add ( (state as OffensiveState) );
			} else if (state as DefensiveState != null) {
				defState.Add ( (state as DefensiveState ));
			}
			state.Init (anim, rb, agent);
			state.enabled = false;

			if (state as Idling != null) {
				state.enabled = true;
				state.endOfState += OnStateEnded;
			}
		}
	}

	/*
	void Start() {
		foreach (PassiveState state in passState) {
			if (state as Idling != null) {
				state.enabled = true;
			}
		}
	}
	*/

	public void OnStateEnded(State someState){
		someState.endOfState -= OnStateEnded;
		someState.enabled = false;

		if (someState as PassiveState != null) {
			int stateIndex = 0;

			while (passState [stateIndex] == someState) {
				stateIndex = Random.Range (0, passState.Count);
			}

			passState [stateIndex].endOfState += OnStateEnded;
			passState [stateIndex].enabled = true;
		}
		else if (playerIsDetected) {
			Debug.Log (playerIsDetected);
			if (someState as Chasing != null) {
				someState.endOfState += OnStateEnded;
				someState.enabled = true;
			}
		}


		/*if(playerInSight || underAttack){
			inCombat = true;
			OffensiveState
				Chase
				attack
				skillz
		}else if(HP < 50% && combat){
			DefensiveState
				Block
				Heal
				Flee
		}else{
			passivstate
				idle
				patrol
		}
		  
		 */
	}
}
