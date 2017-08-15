using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour {

	public BezierCurve bc;

	[Range(0.1f, 3)]
	public float width;
	[Range(0.1f, 3)]
	public float height;
	[Range(3, 20)]
	public int divisions;

	public Vector3[] points = new Vector3[4];
	//first point is starting position
	//second point is curvature from starting position
	//third point is curvature from second point
	//fourth is estimated goal

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			GenerateCurve();
		}
	}

	void OnDrawGizmos(){
		for(int i = 0; i < points.Length; i++){
			Gizmos.DrawSphere(points[i], 0.2f);
		}
	}

	void GenerateCurve () {
		bc = new BezierCurve(points);

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

		bc.Extrude(GetComponent<MeshFilter>().sharedMesh, es, bc.GeneratePath((float)divisions).ToArray());
		/*
		bc.Extrude(GetComponent<MeshFilter>().sharedMesh, es, new OrientedPoint[]{
			new OrientedPoint(new Vector3(0,0,0),Quaternion.identity, 0),
			new OrientedPoint(new Vector3(0,5,10),Quaternion.identity, 1),
			new OrientedPoint(new Vector3(0,15,20),Quaternion.identity, 2),
			new OrientedPoint(new Vector3(0,20,25),Quaternion.identity, 3)
		});
		*/
	}
}