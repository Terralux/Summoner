using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : Placable {

	public GameObject structure; 

	public void Update(){
		if (Input.GetKey (KeyCode.B)) {
			PlaceStructure ();
		}
	}

	public void PlaceStructure(){
		MeshCollider mesh = new MeshCollider();
		if (mesh.GetComponent<MeshCollider> ().sharedMesh.vertices.Length > 0) {
			Instantiate (structure, Random.onUnitSphere, Quaternion.identity);
		}
	}
}
