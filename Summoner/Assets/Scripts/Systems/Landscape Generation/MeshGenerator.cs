using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshGenerator : MonoBehaviour {

	//public SquareGrid squareGrid;
	public CubeGrid cubeGrid;
	List<Vector3> vertices;
	List<int> triangles;

	[Range(0f,10f)]
	public float xOffset = 1f;

	public void GenerateMesh(List<bool[,]> maps, float squareSize) {
		//squareGrid = new SquareGrid(map, squareSize);
		cubeGrid = new CubeGrid(maps, squareSize);

		vertices = new List<Vector3>();
		triangles = new List<int>();

		for(int y = 0; y < cubeGrid.cubes.GetLength(0); y++){
			for (int x = 0; x < cubeGrid.cubes.GetLength(1); x ++) {
				for (int z = 0; z < cubeGrid.cubes.GetLength(2); z ++) {
					CreateMeshUsingSwitchCase(cubeGrid.cubes[y, x, z]);
				}
			}
		}

		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		Debug.Log (vertices.Count);

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();
	}

	private int total;

	void OnDrawGizmos(){
		if (cubeGrid != null) {
			for (int y = 0; y < cubeGrid.cubes.GetLength(0); y ++) {
				for (int x = 0; x < cubeGrid.cubes.GetLength(1); x ++) {
					for (int z = 0; z < cubeGrid.cubes.GetLength (2); z++) {
						total = 0;
						total += DrawCube (cubeGrid.cubes [y, x, z].topSquare.forwardLeft, 1);
						total += DrawCube (cubeGrid.cubes [y, x, z].topSquare.forwardRight, 2);
						total += DrawCube (cubeGrid.cubes [y, x, z].topSquare.backwardRight, 4);
						total += DrawCube (cubeGrid.cubes [y, x, z].topSquare.backwardLeft, 8);

						total += DrawCube (cubeGrid.cubes [y, x, z].bottomSquare.forwardLeft, 16);
						total += DrawCube (cubeGrid.cubes [y, x, z].bottomSquare.forwardRight, 32);
						total += DrawCube (cubeGrid.cubes [y, x, z].bottomSquare.backwardRight, 64);
						total += DrawCube (cubeGrid.cubes [y, x, z].bottomSquare.backwardLeft, 128);
					}
				}
			}
		}
	}

	void OnGUI(){
		GUI.Box (new Rect (10, 10, 100, 30), total.ToString ());
	}

	int DrawCube(ControlNode node, int index) {
		Gizmos.color = node.active ? Color.white : Color.black;
		Gizmos.DrawCube (node.position, Vector3.one * .1f);
		UnityEditor.Handles.Label (node.position + Vector3.right * xOffset, node.active ? index.ToString () : "0");
		return node.active ? index : 0;
	}

	public void CreateMeshUsingSwitchCase(Cube cube){
		Node[] points;
		switch (cube.configuration) {
		case 1:
			points = new Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], true);
			break;
		case 2:
			points = new Node[] {
				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], true);
			break;
		case 3:
			points = new Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);

			CreateTriangle (points [5], points [2], points [1]);
			CreateTriangle (points [6], points [3], points [2]);

			CreateQuad (points [6], points [1], points [5], points [3]);
			break;
		case 4:
			points = new Node[] {
				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], true);
			break;
		case 5:
			points = new Node[] {
				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points [3], points [7], points [2], points [6]);
			CreateQuad (points [6], points [1], points [5], points [3]);
			CreateQuad (points [7], points [1], points [2], points [5]);
			break;
		case 6:
			points = new Node[] {
				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);

			CreateTriangle (points [5], points [2], points [1]);
			CreateTriangle (points [6], points [3], points [2]);

			CreateQuad (points [6], points [1], points [5], points [3]);
			break;
		case 7:
			points = new Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [2], false);

			CreateQuad (points [3], points [7], points [2], points [6]);
			CreateQuad (points [3], points [5], points [6], points [1]);

			CreateTriangle (points [2], points [1], points [9]);
			CreateTriangle (points [7], points [9], points [5]);

			CreateTriangle (points [1], points [5], points [9]);
			break;
		case 8:
			points = new Node[] {
				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], true);
			break;
		case 9:
			points = new Node[] {
				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);

			CreateTriangle (points [5], points [2], points [1]);
			CreateTriangle (points [6], points [3], points [2]);

			CreateQuad (points [6], points [1], points [5], points [3]);
			break;
		case 10:
			points = new Node[] {
				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points [6], points [2], points [3], points [7]);
			CreateQuad (points [1], points [6], points [3], points [5]);
			CreateQuad (points [7], points [1], points [2], points [5]);
			break;
		case 11:
			points = new Node[] {
				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [2], false);

			CreateTriangle (points [2], points [1], points [9]);
			CreateTriangle (points [7], points [9], points [5]);


			CreateQuad (points [7], points [3], points [6], points [2]);
			CreateQuad (points [3], points [5], points [6], points [1]);

			CreateTriangle (points [1], points [5], points [9]);
			break;
		case 12:
			points = new Node[] {
				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,
				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);

			CreateTriangle (points [5], points [2], points [1]);
			CreateTriangle (points [6], points [3], points [2]);

			CreateQuad (points [6], points [1], points [5], points [3]);
			break;
		case 13:
			points = new Node[] {
				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [2], false);

			CreateTriangle (points [2], points [1], points [9]);
			CreateTriangle (points [7], points [9], points [5]);

			CreateQuad (points [7], points [3], points [6], points [2]);
			CreateQuad (points [3], points [5], points [6], points [1]);

			CreateTriangle (points [1], points [5], points [9]);
			break;
		case 14:
			points = new Node[] {
				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [2], false);

			CreateQuad (points [7], points [3], points [6], points [2]);
			CreateQuad (points [3], points [5], points [6], points [1]);

			CreateTriangle (points [2], points [1], points [9]);
			CreateTriangle (points [7], points [9], points [5]);

			CreateTriangle (points [1], points [5], points [9]);
			break;
		case 15:
			points = new Node[] {
				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [2], false);
			CreateCornerMesh (points [10], points [11], points [3], points [6], false);

			CreateTriangle (points [11], points [6], points [5]);
			CreateTriangle (points [5], points [7], points [9]);
			CreateTriangle (points [9], points [2], points [1]);
			CreateTriangle (points [1], points [3], points [11]);

			CreateQuad (points [3], points [7], points [2], points [6]);
			CreateQuad (points [9], points [11], points [1], points [5]);
			break;
		case 16:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], true);
			break;
		case 17:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);

			CreateTriangle (points [1], points [6], points [2]);
			CreateTriangle (points [1], points [3], points [5]);

			CreateQuad (points [6], points [3], points [5], points [2]);
			break;
		case 18:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points [5], points [1], points [7], points [3]);
			CreateQuad (points [6], points [1], points [2], points [7]);
			CreateQuad (points [6], points [3], points [5], points [2]);
			break;
		case 19:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [7], points [9], false);

			CreateQuad (points [5], points [1], points [7], points [3]);
			CreateQuad (points [5], points [2], points [3], points [6]);

			CreateTriangle (points [7], points [6], points [9]);
			CreateTriangle (points [1], points [9], points [2]);

			CreateTriangle (points [9], points [6], points [2]);
			break;
		case 20:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateTriangle (points [1], points [7], points [6]);
			CreateTriangle (points [1], points [6], points [2]);
			CreateTriangle (points [6], points [5], points [2]);
			CreateTriangle (points [5], points [3], points [2]);
			CreateTriangle (points [3], points [5], points [7]);
			CreateTriangle (points [3], points [7], points [1]);
			break;
		case 21:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);

			CreateTriangle (points [1], points [3], points [5]);
			CreateTriangle (points [1], points [6], points [2]);
			CreateTriangle (points [8], points [3], points [2]);

			CreateQuad (points [6], points [10], points [5], points [9]);
			CreateQuad (points [2], points [9], points [6], points [8]);
			CreateQuad (points [10], points [3], points [5], points [8]);
			break;
		case 22:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [10], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);

			CreateTriangle (points [10], points [9], points [6]);
			CreateTriangle (points [10], points [5], points [8]);
			CreateTriangle (points [2], points [9], points [8]);

			CreateQuad (points [5], points [1], points [6], points [3]);
			CreateQuad (points [1], points [9], points [6], points [2]);
			CreateQuad (points [8], points [3], points [5], points [2]);
			break;
		case 23:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [10], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [1], points [6], points [12], false);

			CreateTriangle (points [10], points [5], points [8]);
			CreateTriangle (points [1], points [12], points [2]);

			CreateQuad (points [1], points [5], points [3], points [6]);
			CreateQuad (points [9], points [6], points [12], points [10]);
			CreateQuad (points [2], points [9], points [12], points [8]);
			CreateQuad (points [3], points [8], points [2], points [5]);
			break;
		case 24:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points [1], points [5], points [6], points [2]);
			CreateQuad (points [1], points [7], points [3], points [6]);
			CreateQuad (points [2], points [7], points [5], points [3]);
			break;
		case 25:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [6], false);

			CreateTriangle (points [6], points [9], points [7]);

			CreateQuad (points [1], points [5], points [6], points [2]);
			CreateQuad (points [5], points [3], points [7], points [2]);
			CreateQuad (points [9], points [3], points [1], points [7]);
			break;
		case 26:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);

			CreateTriangle (points [1], points [11], points [6]);

			CreateQuad (points [1], points [5], points [6], points [2]);
			CreateQuad (points [1], points [9], points [3], points [11]);
			CreateQuad (points [11], points [7], points [10], points [6]);
			CreateQuad (points [9], points [7], points [5], points [10]);
			CreateQuad (points [9], points [2], points [3], points [5]);
			break;
		case 27:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [1], points [11], points [6], false);

			CreateQuad (points [1], points [5], points [6], points [2]);
			CreateQuad (points [1], points [9], points [3], points [11]);
			CreateQuad (points [11], points [7], points [10], points [6]);
			CreateQuad (points [9], points [7], points [5], points [10]);
			CreateQuad (points [9], points [2], points [3], points [5]);
			break;
		case 28:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);

			CreateTriangle (points [7], points [6], points [10]);
			CreateTriangle (points [7], points [9], points [5]);
			CreateTriangle (points [3], points [9], points [10]);

			CreateQuad (points [5], points [1], points [2], points [6]);
			CreateQuad (points [9], points [2], points [3], points [5]);
			CreateQuad (points [1], points [10], points [3], points [6]);
			break;
		case 29:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);
			CreateCornerMesh (points [11], points [1], points [12], points [6], false);

			CreateTriangle (points [7], points [9], points [5]);
			CreateTriangle (points [3], points [9], points [10]);

			CreateQuad (points [5], points [1], points [2], points [6]);
			CreateQuad (points [9], points [2], points [3], points [5]);
			CreateQuad (points [12], points [3], points [1], points [10]);
			CreateQuad (points [12], points [7], points [10], points [6]);
			break;
		case 30:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);

			CreateTriangle (points [9], points [3], points [2]);
			CreateTriangle (points [1], points [13], points [6]);

			CreateQuad (points [6], points [10], points [13], points [7]);
			CreateQuad (points [5], points [1], points [2], points [6]);
			CreateQuad (points [1], points [12], points [3], points [13]);
			CreateQuad (points [12], points [9], points [3], points [10]);
			CreateQuad (points [9], points [5], points [2], points [7]);
			break;
		case 31:
			points = new Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);
			CreateCornerMesh (points [14], points [1], points [13], points [6], false);

			CreateTriangle (points [9], points [3], points [2]);

			CreateQuad (points [6], points [10], points [13], points [7]);
			CreateQuad (points [5], points [1], points [2], points [6]);
			CreateQuad (points [1], points [12], points [3], points [13]);
			CreateQuad (points [12], points [9], points [3], points [10]);
			CreateQuad (points [9], points [5], points [2], points [7]);
			break;
		case 32:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], true);
			break;
		case 33:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points [5], points [1], points [2], points [6]);
			CreateQuad (points [1], points [7], points [3], points [6]);
			CreateQuad (points [2], points [7], points [5], points [3]);
			break;
		case 34:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);

			CreateQuad (points [2], points [6], points [1], points [3]);
			CreateQuad (points [3], points [5], points [6], points [1]);
			break;
		case 35:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [6], points [9], false);

			CreateTriangle (points [6], points [5], points [9]);
			CreateTriangle (points [1], points [3], points [5]);
			CreateTriangle (points [5], points [3], points [9]);

			CreateQuad (points [8], points [1], points [2], points [6]);
			CreateQuad (points [2], points [9], points [8], points [3]);
			break;
		case 36:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points [1], points [5], points [3], points [7]);
			CreateQuad (points [6], points [1], points [2], points [7]);
			CreateQuad (points [6], points [3], points [5], points [2]);
			break;
		case 37:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight, 

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);

			CreateTriangle (points [1], points [7], points [10]);

			CreateQuad (points [9], points [1], points [2], points [10]);
			CreateQuad (points [1], points [5], points [3], points [7]);
			CreateQuad (points [10], points [6], points [7], points [11]);
			CreateQuad (points [6], points [9], points [5], points [11]);
			CreateQuad (points [3], points [9], points [2], points [5]);
			break;
		case 38:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight, 

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [7], points [9], false);

			CreateTriangle (points [7], points [6], points [9]);
			CreateTriangle (points [1], points [9], points [2]);
			CreateTriangle (points [9], points [6], points [2]);

			CreateQuad (points [1], points [5], points [3], points [7]);
			CreateQuad (points [5], points [2], points [3], points [6]);
			break;
		case 39:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight, 

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [7], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [12], false);

			CreateQuad (points [1], points [5], points [3], points [7]);
			CreateQuad (points [9], points [6], points [7], points [12]);
			CreateQuad (points [11], points [1], points [2], points [9]);
			CreateQuad (points [6], points [11], points [5], points [12]);
			CreateQuad (points [3], points [11], points [2], points [5]);
			break;
		case 40:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateTriangle (points [1], points [7], points [6]);
			CreateTriangle (points [7], points [1], points [3]);
			CreateTriangle (points [3], points [5], points [7]);
			CreateTriangle (points [5], points [3], points [2]);
			CreateTriangle (points [2], points [6], points [5]);
			CreateTriangle (points [6], points [2], points [1]);
			break;
		case 41:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [6], false);

			CreateTriangle (points [6], points [10], points [7]);
			CreateTriangle (points [6], points [5], points [9]);
			CreateTriangle (points [3], points [5], points [7]);

			CreateQuad (points [9], points [1], points [2], points [10]);
			CreateQuad (points [1], points [7], points [3], points [10]);
			CreateQuad (points [5], points [2], points [3], points [9]);
			break;
		case 42:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);

			CreateTriangle (points [1], points [10], points [2]);
			CreateTriangle (points [1], points [3], points [9]);
			CreateTriangle (points [5], points [3], points [2]);

			CreateQuad (points [10], points [7], points [9], points [6]);
			CreateQuad (points [3], points [7], points [5], points [9]);
			CreateQuad (points [6], points [2], points [5], points [10]);
			break;
		case 43:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [6], false);

			CreateTriangle (points [1], points [3], points [9]);
			CreateTriangle (points [6], points [5], points [12]);

			CreateQuad (points [12], points [1], points [2], points [10]);
			CreateQuad (points [10], points [7], points [9], points [6]);
			CreateQuad (points [7], points [3], points [9], points [5]);
			CreateQuad (points [2], points [5], points [12], points [3]);
			break;
		case 44:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);

			CreateTriangle (points [7], points [9], points [5]);
			CreateTriangle (points [7], points [6], points [10]);
			CreateTriangle (points [2], points [6], points [5]);

			CreateQuad (points [1], points [9], points [3], points [10]);
			CreateQuad (points [1], points [6], points [10], points [2]);
			CreateQuad (points [3], points [5], points [2], points [9]);
			break;
		case 45:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);
			CreateCornerMesh (points [11], points [12], points [13], points [6], false);

			CreateTriangle (points [5], points [3], points [2]);
			CreateTriangle (points [1], points [10], points [13]);

			CreateQuad (points [1], points [9], points [3], points [10]);
			CreateQuad (points [12], points [1], points [2], points [13]);
			CreateQuad (points [13], points [7], points [10], points [6]);
			CreateQuad (points [12], points [5], points [6], points [2]);
			CreateQuad (points [5], points [9], points [7], points [3]);
			break;
		case 46:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);
			CreateCornerMesh (points [11], points [1], points [10], points [12], false);

			CreateQuad (points [1], points [9], points [3], points [10]);
			CreateQuad (points [12], points [7], points [10], points [6]);
			CreateQuad (points [12], points [2], points [6], points [1]);
			CreateQuad (points [5], points [2], points [3], points [6]);
			CreateQuad (points [9], points [5], points [3], points [7]);
			break;
		case 47:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);
			CreateCornerMesh (points [11], points [1], points [10], points [12], false);
			CreateCornerMesh (points [13], points [14], points [12], points [6], false);

			CreateTriangle (points [5], points [3], points [2]);

			CreateQuad (points [14], points [1], points [2], points [12]);
			CreateQuad (points [1], points [9], points [3], points [10]);
			CreateQuad (points [12], points [7], points [10], points [6]);
			CreateQuad (points [5], points [14], points [2], points [6]);
			CreateQuad (points [9], points [5], points [3], points [7]);
			break;
		case 48:
			break;
		default:
			Debug.LogWarning ("Mesh Setup not completed yet!");
			break;
		}
	}

	private void CreateQuad(Node center1, Node center2, Node edge1, Node edge2){
		CreateTriangle (center1, edge1, center2);
		CreateTriangle (center1, center2, edge2);
	}

	private void CreateCornerMesh(Node center, Node n1, Node n2, Node n3, bool closeCorner){
		CreateTriangle (center, n1, n2);
		CreateTriangle (center, n2, n3);
		CreateTriangle (center, n3, n1);

		if (closeCorner) {
			CreateTriangle (n3, n2, n1);
		}
	}

	public void CreateMesh(Cube cube){
		
		#region Create corner Triangles
		if (cube.topSquare.forwardLeft.active) {
			Node[] points = new Node[] { 
				cube.topSquare.forwardLeft, 
				cube.topSquare.centreForward, 
				cube.topSquare.centreLeft, 
				cube.middleForwardLeft
			};
			AssignVertices (points);
			CreateTriangle (points [0], points [1], points [2]);
			CreateTriangle (points [0], points [2], points [3]);
			CreateTriangle (points [0], points [3], points [1]);
		}

		if (cube.topSquare.forwardRight.active) {
			Node[] points = new Node[] { 
				cube.topSquare.forwardRight, 
				cube.topSquare.centreRight, 
				cube.topSquare.centreForward, 
				cube.middleForwardRight
			};
			AssignVertices (points);
			CreateTriangle (points [0], points [1], points [2]);
			CreateTriangle (points [0], points [2], points [3]);
			CreateTriangle (points [0], points [3], points [1]);
		}

		if (cube.topSquare.backwardRight.active) {
			Node[] points = new Node[] { 
				cube.topSquare.backwardRight, 
				cube.topSquare.centreBackward, 
				cube.topSquare.centreRight, 
				cube.middleBackwardRight
			};
			AssignVertices (points);
			CreateTriangle (points [0], points [1], points [2]);
			CreateTriangle (points [0], points [2], points [3]);
			CreateTriangle (points [0], points [3], points [1]);
		}

		if (cube.topSquare.backwardLeft.active) {
			Node[] points = new Node[] { 
				cube.topSquare.backwardLeft, 
				cube.topSquare.centreLeft, 
				cube.topSquare.centreBackward, 
				cube.middleBackwardLeft
			};
			AssignVertices (points);
			CreateTriangle (points [0], points [1], points [2]);
			CreateTriangle (points [0], points [2], points [3]);
			CreateTriangle (points [0], points [3], points [1]);
		}

		if (cube.bottomSquare.forwardLeft.active) {
			Node[] points = new Node[] { 
				cube.bottomSquare.forwardLeft, 
				cube.bottomSquare.centreLeft, 
				cube.bottomSquare.centreForward, 
				cube.middleForwardLeft
			};
			AssignVertices (points);
			CreateTriangle (points [0], points [1], points [2]);
			CreateTriangle (points [0], points [2], points [3]);
			CreateTriangle (points [0], points [3], points [1]);
		}

		if (cube.bottomSquare.forwardRight.active) {
			Node[] points = new Node[] { 
				cube.bottomSquare.forwardRight, 
				cube.bottomSquare.centreForward, 
				cube.bottomSquare.centreRight, 
				cube.middleForwardRight
			};
			AssignVertices (points);
			CreateTriangle (points [0], points [1], points [2]);
			CreateTriangle (points [0], points [2], points [3]);
			CreateTriangle (points [0], points [3], points [1]);
		}

		if (cube.bottomSquare.backwardRight.active) {
			Node[] points = new Node[] { 
				cube.bottomSquare.backwardRight, 
				cube.bottomSquare.centreRight, 
				cube.bottomSquare.centreBackward, 
				cube.middleBackwardRight
			};
			AssignVertices (points);
			CreateTriangle (points [0], points [1], points [2]);
			CreateTriangle (points [0], points [2], points [3]);
			CreateTriangle (points [0], points [3], points [1]);
		}

		if (cube.bottomSquare.backwardLeft.active) {
			Node[] points = new Node[] { 
				cube.bottomSquare.backwardLeft, 
				cube.bottomSquare.centreBackward, 
				cube.bottomSquare.centreLeft, 
				cube.middleBackwardLeft
			};
			AssignVertices (points);
			CreateTriangle (points [0], points [1], points [2]);
			CreateTriangle (points [0], points [2], points [3]);
			CreateTriangle (points [0], points [3], points [1]);
		}

		#endregion

		#region Create side walls

		if (cube.topSquare.forwardLeft.active || cube.topSquare.forwardRight.active || cube.topSquare.backwardLeft.active || cube.topSquare.backwardRight.active) {
			//start building the top
			if (cube.topSquare.forwardLeft.active) {
				if (cube.topSquare.forwardRight.active) {
					Node[] points = new Node[] {
						cube.topSquare.centreForward,
						cube.topSquare.centreRight,
						cube.topSquare.centreLeft,
						cube.middleForwardLeft,
						cube.middleForwardRight
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [3], points [4]);

					if (cube.topSquare.backwardRight.active) {
						points = new Node[] {
							cube.topSquare.centreRight,
							cube.middleForwardRight,
							cube.middleBackwardRight,
							cube.topSquare.centreBackward,
							cube.topSquare.centreLeft
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [0], points [3], points [4]);

						if(cube.bottomSquare.backwardLeft.active){
							points = new Node[] {
								cube.middleBackwardLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight,
								cube.middleForwardLeft,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [3], points [4]);
						}
					} else if (cube.topSquare.backwardLeft.active) {
						points = new Node[] {
							cube.topSquare.centreLeft,
							cube.topSquare.centreRight,
							cube.topSquare.centreBackward,
							cube.middleBackwardLeft,
							cube.middleForwardLeft
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [0], points [3], points [4]);

						points = new Node[]{
							cube.middleBackwardLeft,
							cube.topSquare.centreBackward,
							cube.middleBackwardRight,
							cube.topSquare.centreRight,
							cube.middleForwardRight
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [2], points [3], points [4]);
					} else {
						points = new Node[]{
							cube.topSquare.centreRight,
							cube.middleForwardRight,
							cube.middleBackwardRight,
							cube.topSquare.centreLeft,
							cube.middleBackwardLeft,
							cube.middleForwardLeft
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [3], points [4], points [5]);
					}
				} else if (cube.topSquare.backwardRight.active) {
					Node[] points = new Node[] {
						cube.topSquare.centreLeft,
						cube.topSquare.centreForward,
						cube.topSquare.centreRight,
						cube.topSquare.centreBackward
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [2], points [3]);

					if (cube.topSquare.backwardLeft.active) {
						points = new Node[] {
							cube.middleBackwardLeft,
							cube.topSquare.centreBackward,
							cube.middleBackwardRight,
							cube.middleForwardLeft,
							cube.topSquare.centreLeft
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [0], points [3], points [4]);

						points = new Node[]{
							cube.topSquare.centreForward,
							cube.middleForwardLeft,
							cube.middleForwardRight,
							cube.middleBackwardRight,
							cube.topSquare.centreRight
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [2], points [3], points [4]);
					}else{
						//backleft and forwardright inactive
						points = new Node[] {
							cube.middleBackwardLeft,
							cube.topSquare.centreBackward,
							cube.middleBackwardRight,
							cube.topSquare.centreRight,
							cube.middleForwardRight,
							cube.topSquare.centreForward,
							cube.middleForwardLeft,
							cube.topSquare.centreLeft
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [2], points [3], points [4]);
						CreateTriangle (points [4], points [5], points [6]);
						CreateTriangle (points [6], points [7], points [0]);
					}
				} else if (cube.topSquare.backwardLeft.active) {
					//backright and forward right inactive
					Node[] points = new Node[] {
						cube.topSquare.centreLeft,
						cube.topSquare.centreForward,
						cube.topSquare.centreBackward,
						cube.middleBackwardLeft,
						cube.middleForwardLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [3], points [4]);

					points = new Node[] {
						cube.middleBackwardRight,
						cube.middleBackwardLeft,
						cube.topSquare.centreBackward,
						cube.middleForwardRight,
						cube.topSquare.centreForward,
						cube.middleForwardLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [3], points [4], points [5]);
				}else{
					//backright, forwardright and backleft inactive
					Node[] points = new Node[] {
						cube.topSquare.centreLeft,
						cube.middleBackwardLeft,
						cube.middleForwardLeft,
						cube.middleForwardRight,
						cube.topSquare.centreForward
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [2], points [3], points [4]);
				}
			} else if (cube.topSquare.forwardRight.active) {
				//forwardleft inactive
				if (cube.topSquare.backwardRight.active) {
					Node[] points = new Node[] {
						cube.topSquare.centreRight,
						cube.middleForwardRight,
						cube.middleBackwardRight,
						cube.topSquare.centreBackward,
						cube.topSquare.centreForward
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [3], points [4]);

					if (cube.topSquare.backwardLeft.active) {
						points = new Node[] {
							cube.topSquare.centreBackward,
							cube.middleBackwardRight,
							cube.middleBackwardLeft,
							cube.topSquare.centreLeft,
							cube.topSquare.centreForward,
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [0], points [3], points [4]);

						points = new Node[]{
							cube.topSquare.centreLeft,
							cube.middleBackwardLeft,
							cube.middleForwardLeft,
							cube.middleForwardRight,
							cube.topSquare.centreForward
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [2], points [3], points [4]);
					}else{
						//forwardleft and backleft inactive
						points = new Node[]{
							cube.topSquare.centreForward,
							cube.middleForwardLeft,
							cube.middleForwardRight,
							cube.middleBackwardLeft,
							cube.topSquare.centreBackward,
							cube.middleBackwardRight
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [3], points [4], points [5]);
					}
				} else if (cube.topSquare.backwardLeft.active) {
					//forwardleft and backright are inactive
					Node[] points = new Node[] {
						cube.topSquare.centreForward,
						cube.topSquare.centreRight,
						cube.topSquare.centreBackward,
						cube.topSquare.centreLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [2], points [3]);

					points = new Node[] {
						cube.topSquare.centreBackward,
						cube.topSquare.centreRight,
						cube.middleBackwardRight,
						cube.topSquare.centreForward,
						cube.topSquare.centreLeft,
						cube.middleForwardLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [3], points [4], points [5]);

					points = new Node[] {
						cube.middleBackwardLeft,
						cube.topSquare.centreBackward,
						cube.middleBackwardRight,
						cube.topSquare.centreRight,
						cube.middleForwardRight,
						cube.topSquare.centreForward,
						cube.middleForwardLeft,
						cube.topSquare.centreLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [2], points [3], points [4]);
					CreateTriangle (points [4], points [5], points [6]);
					CreateTriangle (points [6], points [7], points [0]);
				}else{
					//forwardleft, backright, backleft inactive
					Node[] points = new Node[] {
						cube.middleBackwardRight,
						cube.topSquare.centreRight,
						cube.middleForwardRight,
						cube.topSquare.centreForward,
						cube.middleForwardLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [2], points [3], points [4]);
				}
			} else if (cube.topSquare.backwardRight.active) {
				//forwardleft, forwardright inactive

				if (cube.topSquare.backwardLeft.active) {
					Node[] points = new Node[] {
						cube.middleForwardLeft,
						cube.topSquare.centreLeft,
						cube.middleBackwardLeft,
						cube.middleForwardRight,
						cube.middleBackwardRight,
						cube.topSquare.centreRight
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [3], points [4], points [5]);

					points = new Node[] {
						cube.middleBackwardRight,
						cube.middleBackwardLeft,
						cube.topSquare.centreBackward,
						cube.topSquare.centreLeft,
						cube.topSquare.centreRight
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [2], points [3], points [4]);
				}else{
					//forwardleft, forwardright and backleft inactive
					Node[] points = new Node[] {
						cube.middleBackwardLeft,
						cube.topSquare.centreBackward,
						cube.middleBackwardRight,
						cube.topSquare.centreRight,
						cube.middleForwardRight
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [2], points [3], points [4]);
				}
			}else{
				//forward left, forward right, back right inactive
				Node[] points = new Node[] {
					cube.middleForwardLeft,
					cube.topSquare.centreLeft,
					cube.middleBackwardLeft,
					cube.topSquare.centreBackward,
					cube.middleBackwardRight
				};
				AssignVertices (points);
				CreateTriangle (points [0], points [1], points [2]);
				CreateTriangle (points [2], points [3], points [4]);
			}
		}

		if (cube.bottomSquare.forwardLeft.active || cube.bottomSquare.forwardRight.active || cube.bottomSquare.backwardLeft.active || cube.bottomSquare.backwardRight.active) {
			//start building the bottom
			if (cube.bottomSquare.forwardLeft.active) {
				if (cube.bottomSquare.forwardRight.active) {
					Node[] points = new Node[] {
						cube.bottomSquare.centreForward,
						cube.bottomSquare.centreLeft,
						cube.bottomSquare.centreRight,
						cube.middleForwardRight,
						cube.middleForwardLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [3], points [4]);

					if (cube.bottomSquare.backwardRight.active) {
						points = new Node[] {
							cube.bottomSquare.centreRight,
							cube.middleBackwardRight,
							cube.middleForwardRight,
							cube.bottomSquare.centreLeft,
							cube.bottomSquare.centreBackward
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [0], points [3], points [4]);

						if(cube.topSquare.backwardLeft.active){
							points = new Node[] {
								cube.middleBackwardLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [3], points [4]);
						}
					} else if (cube.bottomSquare.backwardLeft.active) {
						//backright inactive
						points = new Node[] {
							cube.bottomSquare.centreLeft,
							cube.bottomSquare.centreBackward,
							cube.bottomSquare.centreRight,
							cube.middleForwardLeft,
							cube.middleBackwardLeft
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [0], points [3], points [4]);

						points = new Node[]{
							cube.bottomSquare.centreBackward,
							cube.middleBackwardLeft,
							cube.middleBackwardRight,
							cube.middleForwardRight,
							cube.bottomSquare.centreRight
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [2], points [3], points [4]);
					}else{
						points = new Node[]{
							cube.middleBackwardRight,
							cube.middleForwardRight,
							cube.bottomSquare.centreRight,
							cube.middleBackwardLeft,
							cube.bottomSquare.centreLeft,
							cube.middleForwardLeft
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [3], points [4], points [5]);
					}
				} else if (cube.bottomSquare.backwardRight.active) {
					//forward right inactive
					Node[] points = new Node[] {
						cube.bottomSquare.centreBackward,
						cube.bottomSquare.centreRight,
						cube.bottomSquare.centreForward,
						cube.bottomSquare.centreLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [2], points [3]);

					if (cube.bottomSquare.backwardLeft.active) {
						points = new Node[] {
							cube.middleBackwardLeft,
							cube.middleBackwardRight,
							cube.bottomSquare.centreBackward,
							cube.bottomSquare.centreLeft,
							cube.middleForwardLeft
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [0], points [3], points [4]);

						points = new Node[]{
							cube.bottomSquare.centreRight,
							cube.middleBackwardRight,
							cube.middleForwardRight,
							cube.middleForwardLeft,
							cube.bottomSquare.centreForward
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [2], points [3], points [4]);
					}else{
						points = new Node[]{
							cube.middleBackwardRight,
							cube.bottomSquare.centreBackward,
							cube.middleBackwardLeft,
							cube.bottomSquare.centreLeft,
							cube.middleForwardLeft,
							cube.bottomSquare.centreForward,
							cube.middleForwardRight,
							cube.bottomSquare.centreRight
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [2], points [3], points [4]);
						CreateTriangle (points [4], points [5], points [6]);
						CreateTriangle (points [6], points [7], points [0]);
					}
				} else if (cube.bottomSquare.backwardLeft.active) {
					//forward right and back right inactive
					Node[] points = new Node[] {
						cube.bottomSquare.centreLeft,
						cube.bottomSquare.centreBackward,
						cube.bottomSquare.centreForward,
						cube.middleForwardLeft,
						cube.middleBackwardLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [3], points [4]);

					points = new Node[]{
						cube.middleBackwardRight,
						cube.bottomSquare.centreBackward,
						cube.middleBackwardLeft,
						cube.middleForwardRight,
						cube.middleForwardLeft,
						cube.bottomSquare.centreForward
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [3], points [4], points [5]);
				}else{
					//forward right, backright, backleft inactive
					Node[] points = new Node[]{
						cube.bottomSquare.centreForward,
						cube.middleForwardRight,
						cube.middleForwardLeft,
						cube.middleBackwardLeft,
						cube.bottomSquare.centreLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [2], points [3], points [4]);
				}
			} else if (cube.bottomSquare.forwardRight.active) {
				//forward left inactive
				if (cube.bottomSquare.backwardRight.active) {
					Node[] points = new Node[] {
						cube.bottomSquare.centreRight,
						cube.middleBackwardRight,
						cube.middleForwardRight,
						cube.bottomSquare.centreForward,
						cube.bottomSquare.centreBackward
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [3], points [4]);

					if (cube.bottomSquare.backwardLeft.active) {
						points = new Node[] {
							cube.bottomSquare.centreBackward,
							cube.middleBackwardLeft,
							cube.middleBackwardRight,
							cube.bottomSquare.centreForward,
							cube.bottomSquare.centreLeft
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [0], points [3], points [4]);

						points = new Node[]{
							cube.middleBackwardLeft,
							cube.bottomSquare.centreLeft,
							cube.middleForwardLeft,
							cube.bottomSquare.centreForward,
							cube.middleForwardRight
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [2], points [3], points [4]);
					}else{
						points = new Node[]{
							cube.middleBackwardLeft,
							cube.middleBackwardRight,
							cube.bottomSquare.centreBackward,
							cube.middleForwardLeft,
							cube.bottomSquare.centreForward,
							cube.middleForwardRight
						};
						AssignVertices (points);
						CreateTriangle (points [0], points [1], points [2]);
						CreateTriangle (points [3], points [4], points [5]);
					}
				} else if (cube.bottomSquare.backwardLeft.active) {
					//forwardleft and backright inactive
					Node[] points = new Node[] {
						cube.bottomSquare.centreLeft,
						cube.bottomSquare.centreBackward,
						cube.bottomSquare.centreRight,
						cube.bottomSquare.centreForward
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [2], points [3]);

					points = new Node[]{
						cube.middleForwardLeft,
						cube.bottomSquare.centreForward,
						cube.middleForwardRight,
						cube.bottomSquare.centreRight,
						cube.middleBackwardRight,
						cube.bottomSquare.centreBackward,
						cube.middleBackwardLeft,
						cube.bottomSquare.centreLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [2], points [3], points [4]);
					CreateTriangle (points [4], points [5], points [6]);
					CreateTriangle (points [6], points [7], points [0]);
				}else{
					//forwardleft, backright and backleft inactive
					Node[] points = new Node[]{
						cube.bottomSquare.centreRight,
						cube.middleBackwardRight,
						cube.middleForwardRight,
						cube.middleForwardLeft,
						cube.bottomSquare.centreForward
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [2], points [3], points [4]);
				}
			} else if (cube.bottomSquare.backwardRight.active) {
				//forwardleft and forwardright inactive
				if (cube.bottomSquare.backwardLeft.active) {
					Node[] points = new Node[] {
						cube.bottomSquare.centreBackward,
						cube.middleBackwardLeft,
						cube.middleBackwardRight,
						cube.bottomSquare.centreRight,
						cube.bottomSquare.centreLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [3], points [4]);

					points = new Node[]{
						cube.middleForwardRight,
						cube.bottomSquare.centreRight,
						cube.middleBackwardRight,
						cube.middleForwardLeft,
						cube.middleBackwardLeft,
						cube.bottomSquare.centreLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [3], points [4], points [5]);
				}else{
					Node[] points = new Node[] {
						cube.bottomSquare.centreBackward,
						cube.middleBackwardLeft,
						cube.middleBackwardRight,
						cube.middleForwardRight,
						cube.bottomSquare.centreRight
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [2], points [3], points [4]);
				}
			}else{
				Node[] points = new Node[]{
					cube.bottomSquare.centreLeft,
					cube.middleForwardLeft,
					cube.middleBackwardLeft,
					cube.middleBackwardRight,
					cube.bottomSquare.centreBackward
				};
				AssignVertices (points);
				CreateTriangle (points [0], points [1], points [2]);
				CreateTriangle (points [2], points [3], points [4]);
			}
		}

		#endregion

		#region Create middle walls for full top

		if(cube.topSquare.forwardLeft.active && cube.topSquare.forwardRight.active && cube.topSquare.backwardRight.active && cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							//do nothing, all elements are active
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points[0], points [2], points[3]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points[0], points [2], points[3]);
						}else{
							Node[] points = new Node[] {
								cube.middleBackwardLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreForward,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points[0], points [2], points[3]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points[0], points [2], points[3]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.middleBackwardLeft,
								cube.middleBackwardRight,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points[0], points [2], points[3]);
						}
					}
				}
			}
		}

		#endregion

		#region Create middle walls for rf rb and lb active

		if(!cube.topSquare.forwardLeft.active && cube.topSquare.forwardRight.active && cube.topSquare.backwardRight.active && cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreForward,
								cube.middleBackwardLeft,
								cube.topSquare.centreLeft,
								cube.topSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreForward,
								cube.middleBackwardLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
							CreateTriangle (points [4], points [3], points [5]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [3], points [5], points [0]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [2], points [4], points [3]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [1], points [3], points [2]);
							CreateTriangle (points [1], points [4], points [3]);
						}
					}
				}
			}
		}

		#endregion

		#region Create middle walls for lf rb and lb active
		if(cube.topSquare.forwardLeft.active && !cube.topSquare.forwardRight.active && cube.topSquare.backwardRight.active && cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.middleBackwardLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardLeft,
								cube.topSquare.centreForward,
								cube.topSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [3], points [5], points [0]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [3], points [5], points [0]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.middleForwardLeft,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [1], points [3], points [2]);
							CreateTriangle (points [1], points [4], points [3]);
						}
					}
				}
			}
		}
		#endregion

		#region Create middle walls for lf rf and lb active
		if(cube.topSquare.forwardLeft.active && cube.topSquare.forwardRight.active && !cube.topSquare.backwardRight.active && cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreRight,
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [1], points [3], points [2]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreBackward,
								cube.middleForwardRight,
								cube.topSquare.centreRight,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.middleForwardRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreLeft,
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [1], points [3], points [2]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardLeft,

								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [4], points [2]);
							CreateTriangle (points [3], points [5], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.middleForwardLeft,
								cube.middleBackwardLeft,
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [0], points [3], points [4]);
						}
					}
				}
			}
		}
		#endregion

		#region Create middle walls for lf rf and rb active
		if(cube.topSquare.forwardLeft.active && cube.topSquare.forwardRight.active && cube.topSquare.backwardRight.active && !cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [1], points [3], points [2]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardRight,
								cube.topSquare.centreBackward,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [1], points [3], points [2]);
							CreateTriangle (points [1], points [4], points [3]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [1], points [3], points [2]);
							CreateTriangle (points [4], points [5], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [1], points [3], points [2]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreForward,
								cube.middleBackwardRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [1], points [0], points [3]);
							CreateTriangle (points [1], points [3], points [4]);
							CreateTriangle (points [4], points [3], points [5]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [0], points [2], points [3]);
							CreateTriangle (points [3], points [2], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}
				}
			}
		}
		#endregion

		#region Create middle walls for lf and rf active
		if(cube.topSquare.forwardLeft.active && cube.topSquare.forwardRight.active && !cube.topSquare.backwardRight.active && !cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft,

								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft,

								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreBackward,
								cube.middleForwardRight,
								cube.topSquare.centreRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[]{
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.middleForwardRight,
								cube.topSquare.centreRight,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft,

								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
							CreateTriangle (points [4], points [3], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreLeft,

								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);

							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreForward,
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft,

								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
							CreateTriangle (points [6], points [5], points [7]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
							CreateTriangle (points [4], points [3], points [5]);
							CreateTriangle (points [4], points [5], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
							CreateTriangle (points [4], points [3], points [5]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.middleForwardRight,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}
					}
				}
			}
		}
		#endregion

		#region Create middle walls for lf and rb active
		if(cube.topSquare.forwardLeft.active && !cube.topSquare.forwardRight.active && cube.topSquare.backwardRight.active && !cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}else{
							Node[] points = new Node[]{
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardRight,
								cube.topSquare.centreBackward,
								cube.topSquare.centreLeft,

								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft,

								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
							CreateTriangle (points [6], points [5], points [7]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.bottomSquare.centreBackward,

								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
							CreateTriangle (points [9], points [10], points [11]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardLeft,
								cube.topSquare.centreForward,
								cube.topSquare.centreRight,

								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreForward,
								cube.topSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.middleForwardLeft,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreBackward,

								cube.topSquare.centreLeft,
								cube.topSquare.centreRight,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);

							CreateTriangle (points [6], points [7], points [8]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.middleForwardLeft,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}
				}
			}
		}
		#endregion

		#region Create middle walls for lf and lb active
		if(cube.topSquare.forwardLeft.active && !cube.topSquare.forwardRight.active && !cube.topSquare.backwardRight.active && cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight,

								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.topSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[]{
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.middleForwardRight,
								cube.topSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [4], points [1]);
							CreateTriangle (points [3], points [5], points [4]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.topSquare.centreForward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.topSquare.centreForward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreForward,

								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight,

								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight,

								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
							CreateTriangle (points [6], points [5], points [7]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.topSquare.centreForward,

								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);

							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[]{
								cube.middleForwardLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.middleForwardRight,
								cube.topSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardLeft,
								cube.topSquare.centreForward,
								cube.middleBackwardRight,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.topSquare.centreForward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.middleForwardLeft,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.middleForwardLeft,
								cube.topSquare.centreForward,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.middleForwardLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}
					}
				}
			}
		}
		#endregion

		#region Create middle walls for rf and rb active
		if(!cube.topSquare.forwardLeft.active && cube.topSquare.forwardRight.active && cube.topSquare.backwardRight.active && !cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.topSquare.centreBackward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.middleForwardLeft,

								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);

							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardRight,
								cube.topSquare.centreBackward,
								cube.middleForwardLeft,
								cube.topSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.middleForwardLeft,

								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);

							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.topSquare.centreBackward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreBackward,

								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);

							CreateTriangle (points [5], points [6], points [7]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.middleForwardLeft,

								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);

							CreateTriangle (points [4], points [5], points [6]);
							CreateTriangle (points [6], points [5], points [7]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.topSquare.centreBackward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreForward,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreForward,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreForward,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.topSquare.centreBackward,

								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);

							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[]{
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreForward,
								cube.middleBackwardRight,
								cube.topSquare.centreBackward,
								cube.topSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.middleBackwardLeft,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
							CreateTriangle (points [4], points [3], points [5]);
						}else{
							Node[] points = new Node[]{
								cube.middleForwardRight,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.middleForwardRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.middleBackwardLeft,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.middleBackwardRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}
					}
				}
			}
		}
		#endregion

		#region Create middle walls for rf and lb active
		if(!cube.topSquare.forwardLeft.active && cube.topSquare.forwardRight.active && cube.topSquare.backwardRight.active && cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,

								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,

								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,

								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreBackward,

								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreLeft,

								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,

								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,

								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight,

								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,

								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,

								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
							CreateTriangle (points [9], points [10], points [11]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreBackward,
								cube.middleForwardRight,
								cube.topSquare.centreRight,
								cube.topSquare.centreBackward,

								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.middleForwardRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreLeft,

								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward,

								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreForward,
								cube.middleBackwardLeft,
								cube.topSquare.centreLeft,
								cube.topSquare.centreForward,

								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward,

								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
							CreateTriangle (points [6], points [5], points [7]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.topSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreRight,

								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreRight,

								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.topSquare.centreRight,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [4], points [1], points [5]);
						}
					}
				}
			}
		}
		#endregion

		#region Create middle walls for rb and lb active
		if(!cube.topSquare.forwardLeft.active && !cube.topSquare.forwardRight.active && cube.topSquare.backwardRight.active && cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreRight,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreLeft,
								cube.middleForwardRight,

								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreLeft,
								cube.middleForwardRight,

								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreLeft,
								cube.middleForwardRight,

								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreLeft,
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [4], points [5], points [6]);
							CreateTriangle (points [6], points [5], points [7]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreRight,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[]{
								cube.bottomSquare.centreRight,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.topSquare.centreLeft,

								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);

							CreateTriangle (points [5], points [6], points [7]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.middleForwardLeft,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
							CreateTriangle (points [4], points [3], points [5]);
						}else{
							Node[] points = new Node[]{
								cube.middleBackwardLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.middleForwardLeft,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.topSquare.centreLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[]{
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreForward,
								cube.middleBackwardLeft,
								cube.topSquare.centreLeft,
								cube.middleForwardRight,
								cube.topSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.topSquare.centreLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreLeft,

								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);

							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.topSquare.centreLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreRight,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[]{
								cube.middleBackwardRight,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[]{
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}
					}
				}
			}
		}
		#endregion

		#region Create walls for lf active
		if(cube.topSquare.forwardLeft.active && !cube.topSquare.forwardRight.active && !cube.topSquare.backwardRight.active && !cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.middleBackwardRight,
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.middleBackwardRight,
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreRight,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreForward,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [1], points [0]);
							CreateTriangle (points [3], points [4], points [1]);
							CreateTriangle (points [3], points [5], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [3], points [2], points [1]);
							CreateTriangle (points [3], points [4], points [2]);
							CreateTriangle (points [3], points [5], points [4]);
							CreateTriangle (points [4], points [5], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.middleBackwardRight,
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.middleBackwardRight,
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardLeft,
								cube.topSquare.centreForward,
								cube.middleBackwardRight,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.middleForwardLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [4], points [1], points [2]);
							CreateTriangle (points [4], points [2], points [5]);
							CreateTriangle (points [0], points [3], points [5]);
							CreateTriangle (points [0], points [5], points [2]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.topSquare.centreForward,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}
					}
				}
			}
		}
		#endregion

		#region Create walls for rf active
		if(!cube.topSquare.forwardLeft.active && cube.topSquare.forwardRight.active && !cube.topSquare.backwardRight.active && !cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [4], points [2]);
							CreateTriangle (points [3], points [5], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.middleBackwardRight,
								cube.topSquare.centreForward,
								cube.middleForwardLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreBackward,
								cube.middleForwardRight,
								cube.topSquare.centreRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.middleForwardRight,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.middleBackwardRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreForward,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.middleBackwardRight,
								cube.bottomSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.topSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [4], points [1], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreForward,
								cube.bottomSquare.centreLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
							CreateTriangle (points [4], points [3], points [5]);
							CreateTriangle (points [4], points [5], points [0]);
							CreateTriangle (points [0], points [5], points [1]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreForward,
								cube.topSquare.centreRight,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}
					}
				}
			}
		}
		#endregion

		#region Create walls for rb active
		if(!cube.topSquare.forwardLeft.active && !cube.topSquare.forwardRight.active && cube.topSquare.backwardRight.active && !cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreBackward,
								cube.middleForwardLeft,
								cube.topSquare.centreRight,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [1], points [0], points [3]);
							CreateTriangle (points [3], points [4], points [1]);
							CreateTriangle (points [3], points [5], points [4]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreBackward,
								cube.middleForwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardRight,
								cube.topSquare.centreBackward,
								cube.middleForwardLeft,
								cube.topSquare.centreRight,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [4], points [5], points [3]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreRight,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreForward,
								cube.middleForwardLeft,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreForward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.middleForwardLeft,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreForward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
							CreateTriangle (points [4], points [3], points [5]);
							CreateTriangle (points [4], points [5], points [0]);
							CreateTriangle (points [0], points [5], points [1]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreBackward,
								cube.middleForwardRight,
								cube.topSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.topSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}else{
							Node[] points = new Node[] {
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreRight,
								cube.middleForwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [4], points [2]);
							CreateTriangle (points [3], points [5], points [4]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardLeft,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardLeft,
								cube.bottomSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.topSquare.centreRight,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}
					}
				}
			}
		}
		#endregion

		#region Create walls for lb active
		if(!cube.topSquare.forwardLeft.active && !cube.topSquare.forwardRight.active && !cube.topSquare.backwardRight.active && cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.middleForwardRight,
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.middleForwardRight,
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight,

								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [5], points [6], points [7]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.middleForwardRight,
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.middleForwardRight,
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreRight,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreRight,

								cube.bottomSquare.centreLeft,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [6], points [7], points [8]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [4], points [2]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.middleForwardLeft,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreForward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [4], points [2]);
							CreateTriangle (points [3], points [5], points [4]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [4], points [2]);
							CreateTriangle (points [3], points [5], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreForward,
								cube.middleBackwardLeft,
								cube.topSquare.centreLeft,
								cube.middleForwardRight,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
							CreateTriangle (points [5], points [4], points [6]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreForward,
								cube.topSquare.centreLeft,
								cube.middleForwardRight,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [4], points [1]);
							CreateTriangle (points [4], points [3], points [5]);
							CreateTriangle (points [4], points [5], points [6]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreLeft,
								cube.middleForwardRight,
								cube.topSquare.centreBackward,
								cube.bottomSquare.centreRight,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [2], points [3], points [4]);
							CreateTriangle (points [4], points [3], points [5]);
							CreateTriangle (points [4], points [5], points [0]);
							CreateTriangle (points [0], points [5], points [1]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreRight,
								cube.topSquare.centreLeft,
								cube.topSquare.centreBackward,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreRight,
								cube.middleBackwardLeft,
								cube.topSquare.centreLeft,
								cube.middleBackwardRight,
								cube.topSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [3], points [4], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.topSquare.centreLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}else{
							Node[] points = new Node[] {
								cube.topSquare.centreBackward,
								cube.topSquare.centreLeft,
								cube.middleBackwardLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}
					}
				}
			}
		}
		#endregion

		#region Create walls for no top
		if(!cube.topSquare.forwardLeft.active && !cube.topSquare.forwardRight.active && !cube.topSquare.backwardRight.active && !cube.topSquare.backwardLeft.active){
			if(cube.bottomSquare.forwardLeft.active){
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.middleForwardRight,
								cube.middleBackwardLeft,
								cube.middleBackwardRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.middleBackwardRight,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.middleForwardRight,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.middleForwardRight,
								cube.bottomSquare.centreRight,
								cube.middleForwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardLeft,
								cube.middleForwardLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreBackward,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [4], points [1], points [5]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleForwardLeft,
								cube.bottomSquare.centreForward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreLeft,
								cube.middleForwardLeft,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}
					}
				}
			}else{
				if(cube.bottomSquare.forwardRight.active){
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardRight,
								cube.middleBackwardLeft,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
						}else{
							Node[] points = new Node[] {
								cube.middleBackwardRight,
								cube.bottomSquare.centreBackward,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreRight,
								cube.middleForwardRight,
								cube.bottomSquare.centreForward,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
							CreateTriangle (points [3], points [1], points [4]);
							CreateTriangle (points [4], points [1], points [5]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreForward,
								cube.middleForwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}
					}
				}else{
					if(cube.bottomSquare.backwardRight.active){
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.middleBackwardLeft,
								cube.bottomSquare.centreLeft,
								cube.middleBackwardRight,
								cube.bottomSquare.centreRight
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
							CreateTriangle (points [2], points [1], points [3]);
						}else{
							Node[] points = new Node[] {
								cube.bottomSquare.centreRight,
								cube.middleBackwardRight,
								cube.bottomSquare.centreBackward
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}
					}else{
						if(cube.bottomSquare.backwardLeft.active){
							Node[] points = new Node[] {
								cube.bottomSquare.centreBackward,
								cube.middleBackwardLeft,
								cube.bottomSquare.centreLeft
							};
							AssignVertices (points);
							CreateTriangle (points [0], points [1], points [2]);
						}else{
							//Do nothing!
						}
					}
				}
			}
		}
		#endregion
	}

	void AssignVertices(Node[] points) {
		for (int i = 0; i < points.Length; i ++) {
			if (points[i].vertexIndex == -1) {
				points[i].vertexIndex = vertices.Count;
				vertices.Add(points[i].position);
			}
		}
	}

	void CreateTriangle(Node a, Node b, Node c) {
		triangles.Add(a.vertexIndex);
		triangles.Add(b.vertexIndex);
		triangles.Add(c.vertexIndex);
	}

	public class CubeGrid {
		public Cube[,,] cubes;

		public CubeGrid(List<bool[,]> maps, float squareSize) {

			int nodeCountX = maps[0].GetLength(0);
			int nodeCountZ = maps[0].GetLength(1);
			float mapWidth = nodeCountX * squareSize;
			float mapDepth = nodeCountZ * squareSize;

			ControlNode[,,] controlNodes = new ControlNode[maps.Count, nodeCountX, nodeCountZ];

			for(int y = 0; y < maps.Count; y ++) {
				for (int x = 0; x < nodeCountX; x ++) {
					for (int z = 0; z < nodeCountZ; z ++) {
						Vector3 pos = new Vector3(-mapWidth/2 + x * squareSize + squareSize/2, -maps.Count/2 + y * squareSize + squareSize/2, -mapDepth/2 + z * squareSize + squareSize/2);
						controlNodes[y, x, z] = new ControlNode(pos, maps[y][x, z], squareSize);
					}
				}
			}

			cubes = new Cube[maps.Count - 1, nodeCountX - 1, nodeCountZ - 1];

			for(int y = 0; y < maps.Count - 1; y ++) {
				for (int x = 0; x < nodeCountX - 1; x ++) {
					for (int z = 0; z < nodeCountZ - 1; z ++) {
						cubes[y, x, z] = new Cube(controlNodes[y + 1, x, z + 1], controlNodes[y + 1, x + 1, z + 1], controlNodes[y + 1, x + 1, z], controlNodes[y + 1, x, z], 
							controlNodes[y, x, z + 1], controlNodes[y, x + 1, z + 1], controlNodes[y, x + 1, z], controlNodes[y, x, z]);
					}
				}
			}
		}
	}

	public class Cube {
		public Square topSquare, bottomSquare;
		public Node middleForwardLeft, middleForwardRight, middleBackwardRight, middleBackwardLeft;
		public int configuration;
		public bool cubeCenterIsInsideMesh = false;
		public int controlNodesActive = 0;

		public Cube (ControlNode _topHalfTopLeft, ControlNode _topHalfTopRight, ControlNode _topHalfBottomRight, ControlNode _topHalfBottomLeft,
			ControlNode _bottomHalfTopLeft, ControlNode _bottomHalfTopRight, ControlNode _bottomHalfBottomRight, ControlNode _bottomHalfBottomLeft) {

			topSquare = new Square(_topHalfTopLeft, _topHalfTopRight, _topHalfBottomRight, _topHalfBottomLeft);
			bottomSquare = new Square(_bottomHalfTopLeft, _bottomHalfTopRight, _bottomHalfBottomRight, _bottomHalfBottomLeft);

			middleForwardLeft = bottomSquare.forwardLeft.above;
			middleForwardRight = bottomSquare.forwardRight.above;
			middleBackwardRight = bottomSquare.backwardRight.above;
			middleBackwardLeft = bottomSquare.backwardLeft.above;

			int count = 0;

			if (bottomSquare.backwardLeft.active){
				configuration += 128;
				count++;
				controlNodesActive++;
			}
			if (bottomSquare.backwardRight.active){
				configuration += 64;
				count++;
				controlNodesActive++;
			}
			if (bottomSquare.forwardRight.active){
				configuration += 32;
				count++;
				controlNodesActive++;
			}
			if (bottomSquare.forwardLeft.active){
				configuration += 16;
				count++;
				controlNodesActive++;
			}

			if (topSquare.backwardLeft.active){
				configuration += 8;
				count++;
				controlNodesActive++;
			}
			if (topSquare.backwardRight.active){
				configuration += 4;
				count++;
				controlNodesActive++;
			}
			if (topSquare.forwardRight.active){
				configuration += 2;
				count++;
				controlNodesActive++;
			}
			if (topSquare.forwardLeft.active){
				configuration += 1;
				count++;
				controlNodesActive++;
			}

			if(count > 4){
				cubeCenterIsInsideMesh = true;
			}
		}
	}

	public class Square {

		public ControlNode forwardLeft, forwardRight, backwardRight, backwardLeft;
		public Node centreForward, centreRight, centreBackward, centreLeft;

		public Square (ControlNode _forwardLeft, ControlNode _forwardRight, ControlNode _backwardRight, ControlNode _backwardLeft) {
			forwardLeft = _forwardLeft;
			forwardRight = _forwardRight;
			backwardRight = _backwardRight;
			backwardLeft = _backwardLeft;

			centreForward = forwardLeft.right;
			centreRight = backwardRight.forward;
			centreBackward = backwardLeft.right;
			centreLeft = backwardLeft.forward;
		}
	}

	public class Node {
		public Vector3 position;
		public int vertexIndex = -1;

		public Node(Vector3 _pos) {
			position = _pos;
		}
	}

	public class ControlNode : Node {

		public bool active;
		public Node above, right, forward;

		public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos) {
			active = _active;
			above = new Node(position + Vector3.up * squareSize/2f);
			right = new Node(position + Vector3.right * squareSize/2f);
			forward = new Node(position + Vector3.forward * squareSize/2f);
		}
	}
}