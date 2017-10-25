using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(Player))]
public class PlayerController : MonoBehaviour {

	public static PlayerController instance;

	private BaseCharacterMovement actions;

	private static PlaceableObjectHandler placeObjectHandler;

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}

		actions = new BaseCharacterMovement (GetComponent<Rigidbody> (), 
			GetComponent<Player> (), 
			GetComponentInChildren<Animator>()
		);
	}

	void Start(){
		InputHandler.StartEvent ().becameActive += TriggerPauseMenu;
		InputHandler.LeftTriggerEvent ().getValue += actions.AdjustMovementBehavior;
		InputHandler.LeftStickEvent ().getValue += actions.Movement;
		InputHandler.AEvent ().becameActive += Jump;
		InputHandler.YEvent ().becameActive += CheckItemUsage;
		InputHandler.BEvent ().becameActive += CheckForDodge;
		InputHandler.XEvent ().becameActive += Attack;

		InputHandler.LBEvent ().becameActive += HotbarNavigationLeft;
		InputHandler.RBEvent ().becameActive += HotbarNavigationRight;

		InputHandler.RightTriggerEvent ().becameActive += ChangeHotbar;
	}

	public void TriggerPauseMenu(){
		PauseMenu.instance.Show ();
		ResetPlayerControls ();
	}

	public void ResetPlayerControls(){
		
	}

	public void CheckItemUsage(){
		ActiveItemHotbarHandler.UseItem();
	}

	public void Jump(){
		if (placeObjectHandler != null) {
			placeObjectHandler.PlaceObject ();
		} else {
			actions.Jump ();
		}
	}

	public void HotbarNavigationLeft(){
		ActiveItemHotbarHandler.MoveSelector (false);
	}

	public void HotbarNavigationRight(){
		ActiveItemHotbarHandler.MoveSelector (true);
	}

	public void ChangeHotbar() {
		ActiveItemHotbarHandler.SwitchActiveHotbar ();
	}

	public void Attack(){
		Vector2 input = InputHandler.DPadEvent ().input;

		if(input.x < 0.1f && input.x > -0.1f && input.y < 0.1f && input.y > -0.1f){
			Debug.Log ("Attack!");
		}else{
			CheckTalents (input);
		}
	}

	void CheckTalents(Vector2 input){
		if(Mathf.Abs(input.x) > Mathf.Abs(input.y)){
			if(input.x > 0){
				Debug.Log ("Talent Right!");
			}else{
				Debug.Log ("Talent Left!");
			}
		}else{
			if(input.y > 0){
				Debug.Log ("Talent Up!");
			}else{
				Debug.Log ("Talent Down!");
			}
		}
	}

	public void CheckForDodge(){
		if (placeObjectHandler != null) {
			Destroy (placeObjectHandler);
		} else {
			//Dodge roll
		}
	}

	void FixedUpdate(){
		actions.CheckIfGrounded ();
	}

	public void PlaceObject(Placable p){
		if (placeObjectHandler == null) {
			placeObjectHandler = gameObject.AddComponent<PlaceableObjectHandler> ();
			placeObjectHandler.Init (p, this);
		}
	}
}