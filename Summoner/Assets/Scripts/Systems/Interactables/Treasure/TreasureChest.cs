using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : BaseTreasure {

	public BaseItem item;
	public int quantity;

	#region implemented abstract members of BaseTreasure

	public override void OnInteract ()
	{
		if (!hasBeenUsed) {
			Player.instance.inventory.AddItem (item, quantity);
			hasBeenUsed = true;
			if(quantity > 1) {  
				Debug.Log (item.itemName + " x " + quantity + " was added to inventory."); 
			} else {
				Debug.Log (item.itemName + " was added to inventory");	
			}
		}
	}

	#endregion

}
