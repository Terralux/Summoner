using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStructure : MonoBehaviour {

	public GameObject structure; 

	public void Update(){
		if (Input.GetKeyDown (KeyCode.B)) {
			PlaceStructure ();
		}
	}

	public void PlaceStructure(){
		/*MeshCollider mesh = new MeshCollider();
		if (mesh.GetComponent<MeshFilter> ().sharedMesh.vertices.Length > 0) {
			Instantiate (structure, Random.onUnitSphere, Quaternion.identity);
		}
		if (mesh.GetComponent<MeshFilter> ().sharedMesh != null) {
			Instantiate (structure, Random.onUnitSphere, Quaternion.identity);
		}*/
		Instantiate (structure, transform.position, Quaternion.identity);
	}
}
