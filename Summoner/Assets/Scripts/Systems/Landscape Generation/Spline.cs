using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour {

	public BezierCurve bc;

	[Range(0.1f, 3)]
	public float width;
	[Range(0.1f, 3)]
	public float height;

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			GenerateCurve();
		}
	}

	void GenerateCurve () {
		bc = new BezierCurve(new Vector3[]{
			new Vector3(0,0,0), 
			new Vector3(10,5,0), 
			new Vector3(20,15,0), 
			new Vector3(35,20,0)
		});

		ExtrudeShape es = new ExtrudeShape();
		//Vertices for extrusion shape
		es.verts = new Vector2[]{
			new Vector2(-2,0),
			new Vector2(-1.1f,0.9f),
			new Vector2(0,1),
			new Vector2(1.1f,0.9f),
			new Vector2(2,0)
		};
		es.normals = new Vector2[]{
			Vector2.up,
			Vector2.up,
			Vector2.up,
			Vector2.up,
			Vector2.up
		};
		es.uCoords = new float[]{
			0,
			0,
			0,
			0,
			0
		};
		bc.Extrude(GetComponent<MeshFilter>().sharedMesh, es, new OrientedPoint[]{
			new OrientedPoint(new Vector3(0,0,0),Quaternion.identity, 0),
			new OrientedPoint(new Vector3(0,5,10),Quaternion.identity, 1),
			new OrientedPoint(new Vector3(0,15,20),Quaternion.identity, 2),
			new OrientedPoint(new Vector3(0,20,25),Quaternion.identity, 3)
		});
	}
}