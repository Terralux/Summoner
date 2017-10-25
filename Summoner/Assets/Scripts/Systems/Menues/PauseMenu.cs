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
		Hide();
	}

	public override void Show(){
		instance.gameObject.SetActive(true);
		InputHandler.BEvent().becameActive += Hide;
	}

	public override void Hide(){
		instance.gameObject.SetActive(false);
		InputHandler.BEvent().becameActive -= Hide;
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