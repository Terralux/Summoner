using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : BaseMenu {

	public static PauseMenu instance;

	void Awake(){
		if(instance == null){
			instance = this;
		}else{
			Destroy(this);
		}
		InventoryMenu.instance = GetComponentInChildren<InventoryMenu> ();
		InventoryMenu.instance.SetInventory ();
		InventoryMenu.instance.Hide ();
		Hide();
	}

	void Update(){
		if(Input.GetButtonDown("Start")){
			instance.Hide ();

			foreach (Transform t in GetComponentsInChildren<Transform>()) {
				
			}
		}
	}

	public override void Show(){
		instance.gameObject.SetActive(true);
	}

	public override void Hide(){
		instance.gameObject.SetActive(false);
	}

	public void ShowCharacterTab(){
		
	}

	public void ShowInventoryTab(){
		InventoryMenu.instance.Show ();
	}

	public void ShowOptionsTab(){
		
	}
		
	public void ExitGameSession(){
		Application.Quit ();
	}
}