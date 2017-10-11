using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTreasure : Interactive {

	protected bool hasBeenUsed;
	public abstract override void OnInteract ();

}
