using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStructure : Interactive {
	public bool isPowered;

	public int durability;
	protected int currentDurability;

	public delegate void VoidEvent(BaseStructure structure);
	public VoidEvent OnShutdown;

	void Awake(){
		if(WellOfLife.instance != null){
			WellOfLife.citySetup.RegisterStructure (this);
		}
	}
}

public abstract class InteractiveStructure : BaseStructure {

}

public abstract class AutomaticStructure : BaseStructure {
	protected bool isActive = false;

	public override void OnInteract(){
		isActive = !isActive;
	}

	protected abstract void Update();
}