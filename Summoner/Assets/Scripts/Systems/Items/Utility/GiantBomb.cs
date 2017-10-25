using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBomb : BaseUtility {
	#region implemented abstract members of BaseUtility
	public override void OnControlOverride ()
	{
		throw new System.NotImplementedException ();
	}
	public override void OnActivation ()
	{
		throw new System.NotImplementedException ();
	}
	public override void OnDeActivation ()
	{
		throw new System.NotImplementedException ();
	}
	#endregion

	void OnBombPlaced() {
		
	}

	void OnBombDetonation() {
		
	}
}
