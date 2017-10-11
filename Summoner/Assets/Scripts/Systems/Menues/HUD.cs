using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : BaseMenu{
	public static HUD instance;

	void Awake(){
		if(instance == null){
			instance = this;
		}else{
			Destroy(this);
		}
	}

	public override void Show(){
		instance.gameObject.SetActive(true);
	}

	public override void Hide(){
		instance.gameObject.SetActive(false);
	}
}