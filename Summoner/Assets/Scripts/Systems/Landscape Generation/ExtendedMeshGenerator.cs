using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedMeshGenerator {

	private MeshGenerator mg;

	public void ExtendedMeshGeneration(MeshGenerator newMG, MeshGenerator.Cube cube){
		mg = newMG;
		MeshGenerator.Node[] points;
		switch (cube.configuration) {
		case 255:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], true);
			break;
		default:
			Debug.LogWarning("Still working on this one!");
			break;
		}
	}

	private void CreateQuad(MeshGenerator.Node center1, MeshGenerator.Node center2, MeshGenerator.Node edge1, MeshGenerator.Node edge2){
		CreateTriangle (center1, edge1, center2);
		CreateTriangle (center1, center2, edge2);
	}

	private void CreateCornerMesh(MeshGenerator.Node center, MeshGenerator.Node n1, MeshGenerator.Node n2, MeshGenerator.Node n3, bool closeCorner){
		CreateTriangle (center, n1, n2);
		CreateTriangle (center, n2, n3);
		CreateTriangle (center, n3, n1);

		if (closeCorner) {
			CreateTriangle (n3, n2, n1);
		}
	}

	void AssignVertices(MeshGenerator.Node[] points) {
		for (int i = 0; i < points.Length; i ++) {
			if (points[i].vertexIndex == -1) {
				points[i].vertexIndex = mg.vertices.Count;
				mg.vertices.Add(points[i].position);
			}
		}
	}

	void CreateTriangle(MeshGenerator.Node a, MeshGenerator.Node b, MeshGenerator.Node c) {
		mg.triangles.Add(a.vertexIndex);
		mg.triangles.Add(b.vertexIndex);
		mg.triangles.Add(c.vertexIndex);
	}
}
