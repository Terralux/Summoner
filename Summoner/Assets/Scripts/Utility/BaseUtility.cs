using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUtility : MonoBehaviour {

	public abstract void OnControlOverride();
	public abstract void OnActivation ();
	public abstract void OnDeActivation ();

}
