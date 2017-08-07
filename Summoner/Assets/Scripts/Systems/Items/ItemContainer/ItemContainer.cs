using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{

	public BaseItem item;
	public int itemQuantity;

	void Awake(){
		GetComponent<SpriteRenderer> ().sprite = item.image;
	}

	void OnTriggerEnter (Collider other)
	{
		Player p = other.GetComponent<Player> ();
		if (p != null) {
			p.inventory.AddItem (item, itemQuantity);
			Destroy (gameObject);

		}

	}
}
