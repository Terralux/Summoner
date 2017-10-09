using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(Player))]
public class PlayerController : MonoBehaviour {

	public static PlayerController instance;

	private BaseCharacterMovement actions;

	private bool previousRTState = false;

	private static PlaceableObjectHandler placeObjectHandler;

	/*
	 * Player can either be free roaming allowing for every behavior
	 * Player can be placing an item, allowing for only movement and 
	 * Player can be temporarily stunned or knocked back
	 * Player can be dead
	*/

	public enum PlayerState{
		DEAD,
		STUNNED,
		CAN_MOVE,
		FREE_FORM
	}

	private static PlayerState currentState = PlayerState.FREE_FORM;

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

	/*
	 * Left stick to move character
	 * right stick to move camera
	 * A to jump
	 * Y to use item
	 * Right Trigger to attack
	 * Left Trigger to look in camera direction
	 * 
	 * Bumpers Left and Right to shuffle across Item Bar
	 * 
	 * Holding a directional button changes the regular attack to use of the selected skill
	 * 
	 * 
	*/

	void Update () {
		CheckForMenuOpen ();

		float LT = Input.GetAxis ("LT");

		switch (currentState) {
		case PlayerState.FREE_FORM:

			actions.AdjustMovementBehavior (LT > 0.2f);

			CheckMovement ();
			CheckHotbarNavigation ();
			CheckAttack ();
			break;
		case PlayerState.CAN_MOVE:
			actions.AdjustMovementBehavior (LT > 0.2f);

			CheckMovement ();
			CheckHotbarNavigation ();
			break;
		case PlayerState.STUNNED:
			CheckHotbarNavigation ();
			break;
		case PlayerState.DEAD:
			break;
		}
	}

	void CheckForMenuOpen(){
		if (Input.GetButtonDown ("Start")) {
			//Open Menu
			PauseMenu.instance.Show ();
			this.enabled = false;
		}
	}

	void CheckItemUsage(){
		if(Input.GetButtonDown("Y")){
			//Use selected Item
			ActiveItemHotbarHandler.UseItem();
		}
	}

	void CheckMovement(){
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		actions.Movement (new Vector2 (h, v));

		CheckForJump ();

		CheckForDodge ();
	}

	void CheckForJump(){
		if (Input.GetButtonDown("A")) {
			if (placeObjectHandler != null) {
				placeObjectHandler.PlaceObject ();
			} else {
				actions.Jump ();
			}
		}
	}

	void CheckHotbarNavigation() {
		float RT = Input.GetAxis ("RT");

		if(Input.GetButtonDown("LB")){
			//Move left on Item menu
			ActiveItemHotbarHandler.MoveSelector (false);
		}
		if(Input.GetButtonDown("RB")){
			//Move right on Item menu
			ActiveItemHotbarHandler.MoveSelector (true);
		}

		if (!previousRTState) {
			if (RT > 0.3f) {
				ActiveItemHotbarHandler.SwitchActiveHotbar ();
				previousRTState = true;
				Debug.Log ("switch hotbar");
			}
		} else {
			if (RT < 0.1f) {
				Debug.Log ("button released");
				previousRTState = false;
			}
		}

		CheckItemUsage ();
	}

	void CheckAttack(){
		float DH = Input.GetAxis ("DPadHorizontal");
		float DV = Input.GetAxis ("DPadVertical");

		if (Input.GetButtonDown ("X")) {
			if(DH > 0.1f || DH < -0.1f || DV > 0.1f || DV < -0.1f){
				//Attack
			}else{
				CheckTalents (DH, DV);
			}
		}
	}

	void CheckTalents(float DH, float DV){
		if(Mathf.Abs(DH) > Mathf.Abs(DV)){
			if(DH > 0){
				if (Input.GetButtonDown ("X")) {
					//Do skill on D-Pad Left
				}
			}else{
				if (Input.GetButtonDown ("X")) {
					//Do skill on D-Pad Right
				}
			}
		}else{
			if(DV > 0){
				if (Input.GetButtonDown ("X")) {
					//Do skill on D-Pad Down
				}
			}else{
				if (Input.GetButtonDown ("X")) {
					//Do skill on D-Pad Up
				}
			}
		}
	}

	void CheckForDodge(){
		if (Input.GetButtonDown ("B")) {
			if (placeObjectHandler != null) {
				Destroy (placeObjectHandler);
				SetPlayerState (PlayerState.FREE_FORM);
			} else {
				//Dodge roll
			}
		}
	}

	public static void SetPlayerState(PlayerState neoState){
		currentState = neoState;
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