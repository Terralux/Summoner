using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStructure : MonoBehaviour {

	private Transform player;
	public GameObject structure;

	public Structure myStructure;

	public Color accept;
	public Color decline;

	[Range(0,5)]
	public int x;
	[Range(0.0f, 5.0f)]
	public float y;
	[Range(0,5)]
	public int z;

	private bool isAccepted;

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

		if (Input.GetKeyDown (KeyCode.K)) {
			isAccepted = !isAccepted;
		}
	}

	public void PlaceStructure(){
		structure.SetActive (!structure.activeSelf);
		structure.GetComponent<MeshFilter>().mesh = myStructure.prefab.GetComponent<MeshFilter>().mesh;
		structure.GetComponent<MeshRenderer> ().material.color = isAccepted ? accept : decline;
	}

	public void OnTriggerEnter(Collider Terrain){
		/*if (!Terrain) {
			structure.GetComponent<Renderer>().material = mats [2];
		} else {
			structure.GetComponent<Renderer>().material = mats [1];

		}*/
	}
}
