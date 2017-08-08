using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStructure : MonoBehaviour {

	private Transform player;
	public GameObject structure; 
	[Range(0,5)]
	public int x;
	[Range(0.0f, 5.0f)]
	public float y;
	[Range(0,5)]
	public int z;


	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	public void Update(){
		structure.transform.position = new Vector3 (
			((int)player.position.x),			
			((int)player.position.y),
			((int)player.position.z)
		) + (player.TransformDirection(new Vector3(x, y, z)));

		if (Input.GetKeyDown (KeyCode.B)) {
			PlaceStructure ();
		}
	}

	public void PlaceStructure(){
		//WorldManager.instance.dimension;
	}
}
