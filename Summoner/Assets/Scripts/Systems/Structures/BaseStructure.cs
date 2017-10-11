using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStructure : Interactive {
	public bool isPowered;

	public int durability;
	protected int currentDurability;
}

public abstract class InteractiveStructure : BaseStructure {

}

public abstract class AutomaticStructure : BaseStructure {
	protected bool isActive = false;

	public override void OnInteract(){
		isActive = !isActive;
	}

	public abstract void Update();
}