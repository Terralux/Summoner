using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{

	public BaseItem item;

	void OnTriggerEnter (Collider other)
	{
		Player p = other.GetComponent<Player> ();
		if (p != null) {
			p.inventory.AddItem (item, 1);
			Destroy (gameObject);

			Debug.Log ("Number of items in inventory is: " + p.inventory.GetNumberOfItems () );
			Debug.Log ("Quantity: " + item.name + " x " + p.inventory.GetQuantityOfItem(item));
		}

	}
}
