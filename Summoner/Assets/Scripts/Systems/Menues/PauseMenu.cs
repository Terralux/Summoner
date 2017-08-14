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
	}

	public override void Hide(){
		instance.gameObject.SetActive(false);
	}
}