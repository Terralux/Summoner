using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectHandler : MonoBehaviour {

	public Placable myPlacable;

	private float offsetZ = 2f;
	private float maxDistance = 20f;

	private GameObject placableInstance;
	private float fraction;

	private PlayerController pc;

	public void Init(Placable neoObject, PlayerController pc){
		this.pc = pc;
		myPlacable = neoObject;
		placableInstance = Instantiate (myPlacable.prefab, transform.position + transform.forward * offsetZ + Vector3.up * 0.05f, Quaternion.identity);
	}

	void Update(){
		fraction = Vector3.Distance (placableInstance.transform.position, transform.position + transform.forward * offsetZ + Vector3.up * 0.05f) / maxDistance;
		placableInstance.transform.position = Vector3.Lerp (placableInstance.transform.position, transform.position + transform.forward * offsetZ + Vector3.up * 0.05f, fraction);
	}

	public void PlaceObject(){
		//TODO insert code to handle adding object to Chunk(s)

		RaycastHit hit;
		Ray ray = new Ray (transform.position + transform.forward * offsetZ + Vector3.up * 0.05f, Vector3.down);

		if (Physics.Raycast (ray, out hit, 3f)) {
			Vector3 position = transform.position + transform.forward * offsetZ + Vector3.up * 0.05f;
			placableInstance.transform.position = new Vector3 (position.x, hit.point.y, position.z);
			Destroy(this);
		}
	}
}