using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshGenerator {

	public List<Vector3> vertices = new List<Vector3>();
	public List<int> triangles = new List<int>();

	private Cube top;
	private Cube bottom;
	private Cube left;
	private Cube right;
	private Cube forward;
	private Cube back;

	[Range(0f,10f)]
	public float xOffset = 1f;

	public void GenerateMesh(MeshFilter mf, Chunk chunk) {
		vertices = new List<Vector3>();
		triangles = new List<int>();

		for(int y = 0; y < chunk.slices.Length; y++){
			for (int x = 0; x < chunk.slices[y].cubes.GetLength(0); x ++) {
				for (int z = 0; z < chunk.slices[y].cubes.GetLength(0); z ++) {
					top = (y + 1 < chunk.slices.Length ? chunk.slices[y + 1].cubes[x, z] : new Cube());
					bottom = (y - 1 > -1 ? chunk.slices[y - 1].cubes[x, z] : new Cube());
					left = (x - 1 > -1 ? chunk.slices[y].cubes[x - 1, z] : new Cube());
					right = (x + 1 < chunk.slices[y].cubes.GetLength(0) ? chunk.slices[y].cubes[x + 1, z] : new Cube());
					forward = (z + 1 < chunk.slices[y].cubes.GetLength(0) ? chunk.slices[y].cubes[x, z + 1] : new Cube());
					back = (z - 1 > -1 ? chunk.slices[y].cubes[x, z - 1] : new Cube());

					CreateMeshUsingSwitchCase(chunk.slices[y].cubes[x, z]);
				}
			}
		}

		Mesh mesh = new Mesh();
		mf.mesh = mesh;

		Debug.Log (vertices.Count);

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();
	}

	private int total;

	public void CreateMeshUsingSwitchCase(Cube cube){
		Node[] points;
		switch (cube.configuration) {
		case 0:
			//Debug.LogError("Having a cube of 0 configuration should not be possible!");
			break;
		case 1:
			points = new Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], true);
			break;
		case 2:
			points = new Node[] {
				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward
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
				cube.topSquare.centreRight
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
				cube.topSquare.centreLeft
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
				cube.topSquare.centreBackward
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
				cube.topSquare.centreBackward
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
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);

			CreateQuad (points[1], points[5], points[3], points [2]);
			CreateQuad (points[3], points[6], points[2], points [5]);
			break;
		case 49:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [5], points [8], points [9], false);

			CreateTriangle (points [2], points [6], points [3]);

			CreateQuad (points [1], points [5], points [8], points[2]);
			CreateQuad (points [9], points [6], points [3], points[5]);
			CreateQuad (points [9], points [1], points [8], points[3]);
			break;
		case 50:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward

			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);

			CreateTriangle (points [2], points [6], points [3]);

			CreateQuad (points [5], points [1], points [2], points[9]);
			CreateQuad (points [8], points [3], points [1], points[6]);
			CreateQuad (points [5], points [8], points [9], points[6]);
			break;
		case 51:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft

			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);
			CreateCornerMesh (points [10], points [5], points [9], points [11], false);

			CreateTriangle (points [2], points [6], points [3]);
			CreateTriangle (points [9], points [8], points [11]);
			CreateTriangle (points [5], points [11], points [6]);
			CreateTriangle (points [1], points [3], points [8]);

			CreateQuad (points [5], points [1], points [2], points[9]);
			CreateQuad (points [11], points [3], points [8], points[6]);
			break;
		case 52:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight
	
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);

			CreateTriangle (points [2], points [6], points [3]);
			CreateTriangle (points [2], points [1], points [5]);
			CreateTriangle (points [9], points [6], points [5]);

			CreateQuad (points [1], points [8], points [3], points[10]);
			CreateQuad (points [6], points [8], points [9], points[3]);
			CreateQuad (points [5], points [10], points [1], points[9]);
			break;
		case 53:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft

			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [5], points [12], points [13], false);

			CreateTriangle (points [2], points [6], points [3]);
			CreateTriangle (points [1], points [10], points [12]);

			CreateQuad (points [5], points [1], points [2], points[12]);
			CreateQuad (points [6], points [8], points [9], points[3]);
			CreateQuad (points [9], points [12], points [13], points[10]);
			CreateQuad (points [13], points [6], points [9], points[5]);
			CreateQuad (points [10], points [3], points [1], points[8]);
			break;
		case 54:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [1], points [10], points [12], false);

			CreateTriangle (points [2], points [6], points [3]);
			CreateTriangle (points [9], points [6], points [5]);

			CreateQuad (points [5], points [1], points [2], points[12]);
			CreateQuad (points [6], points [8], points [9], points[3]);
			CreateQuad (points [8], points [1], points [10], points[3]);
			CreateQuad (points [12], points [9], points [10], points[5]);
			break;
		case 55:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [1], points [10], points [12], false);
			CreateCornerMesh (points [13], points [5], points [12], points [14], false);

			CreateTriangle (points [2], points [6], points [3]);

			CreateQuad (points [5], points [1], points [2], points[12]);
			CreateQuad (points [6], points [8], points [9], points[3]);
			CreateQuad (points [8], points [1], points [10], points[3]);
			CreateQuad (points [10], points [14], points [9], points[12]);
			CreateQuad (points [14], points [6], points [9], points[5]);
			break;
		case 56:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);

			CreateTriangle (points [2], points [6], points [3]);

			CreateQuad (points [3], points [8], points [6], points[10]);
			CreateQuad (points [8], points [5], points [6], points[9]);
			CreateQuad (points [1], points [5], points [9], points[2]);
			CreateQuad (points [10], points [1], points [9], points[3]);
			break;
		case 57:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward

			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [5], points [12], points [9], false);

			CreateTriangle (points [2], points [6], points [3]);
			CreateTriangle (points [9], points [12], points [10]);

			CreateQuad (points [5], points [1], points [2], points[12]);
			CreateQuad (points [8], points [5], points [6], points[9]);
			CreateQuad (points [3], points [8], points [6], points[10]);
			CreateQuad (points [1], points [10], points [3], points[12]);
			break;
		case 58:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

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
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [1], points [12], points [13], false);

			CreateTriangle (points [2], points [6], points [3]);
			CreateTriangle (points [5], points [13], points [9]);

			CreateQuad (points [5], points [1], points [2], points[13]);
			CreateQuad (points [3], points [8], points [6], points[10]);
			CreateQuad (points [8], points [5], points [6], points[9]);
			CreateQuad (points [12], points [9], points [10], points[13]);
			CreateQuad (points [12], points [3], points [1], points[10]);
			break;
		case 59:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [1], points [12], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [9], false);

			CreateTriangle (points [2], points [6], points [3]);

			CreateQuad (points [5], points [1], points [2], points[13]);
			CreateQuad (points [12], points [9], points [10], points[13]);
			CreateQuad (points [8], points [5], points [6], points[9]);
			CreateQuad (points [3], points [8], points [6], points[10]);
			CreateQuad (points [12], points [3], points [1], points[10]);
			break;
		case 60:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

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
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);

			CreateTriangle (points [2], points [6], points [3]);
			CreateTriangle (points [2], points [1], points [5]);
			CreateTriangle (points [10], points [9], points [13]);
			CreateTriangle (points [10], points [12], points [8]);

			CreateQuad (points [1], points [12], points [3], points[13]);
			CreateQuad (points [8], points [5], points [6], points[9]);
			CreateQuad (points [12], points [6], points [3], points[8]);
			CreateQuad (points [9], points [1], points [5], points[13]);
			break;
		case 61:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

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
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);
			CreateCornerMesh (points [14], points [5], points [15], points [9], false);

			CreateTriangle (points [2], points [6], points [3]);
			CreateTriangle (points [10], points [12], points [8]);
			CreateTriangle (points [1], points [13], points [15]);

			CreateQuad (points [5], points [1], points [2], points[15]);
			CreateQuad (points [13], points [9], points [10], points[15]);
			CreateQuad (points [8], points [5], points [6], points[9]);
			CreateQuad (points [1], points [12], points [3], points[13]);
			CreateQuad (points [12], points [6], points [3], points[8]);
			break;
		case 62:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);
			CreateCornerMesh (points [14], points [1], points [13], points [15], false);

			CreateTriangle (points [2], points [6], points [3]);
			CreateTriangle (points [10], points [12], points [8]);
			CreateTriangle (points [5], points [15], points [9]);

			CreateQuad (points [5], points [1], points [2], points[15]);
			CreateQuad (points [13], points [9], points [10], points[15]);
			CreateQuad (points [8], points [5], points [6], points[9]);
			CreateQuad (points [1], points [12], points [3], points[13]);
			CreateQuad (points [12], points [6], points [3], points[8]);
			break;
		case 63:
			points = new Node[] {
				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);
			CreateCornerMesh (points [14], points [1], points [13], points [15], false);
			CreateCornerMesh (points [16], points [5], points [15], points [9], false);

			CreateTriangle (points [2], points [6], points [3]);
			CreateTriangle (points [10], points [12], points [8]);


			CreateQuad (points [5], points [1], points [2], points[15]);
			CreateQuad (points [13], points [9], points [10], points[15]);
			CreateQuad (points [8], points [5], points [6], points[9]);
			CreateQuad (points [1], points [12], points [3], points[13]);
			CreateQuad (points [12], points [6], points [3], points[8]);
			break;
		case 64:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], true);
			break;
		case 65:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateTriangle (points[5],points[3],points[2]);
			CreateTriangle (points[1],points[7],points[6]);

			CreateQuad (points[2],points[6],points[1],points[5]);
			CreateQuad (points[7],points[3],points[1],points[5]);
			break;
		case 66:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points[1], points[5], points[6], points[2]);
			CreateQuad (points[7], points[2], points[3], points[5]);
			CreateQuad (points[7], points[1], points[6], points[3]);
			break;
		case 67:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);

			CreateTriangle (points[7], points[6], points[10]);
			CreateTriangle (points[7], points[9], points[5]);
			CreateTriangle (points[3], points[9], points[10]);

			CreateQuad (points[1], points[5], points[6], points[2]);
			CreateQuad (points[10], points[1], points[6], points[3]);
			CreateQuad (points[2], points[9], points[5], points[3]);
			break;
		case 68:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);

			CreateTriangle (points[1], points[6], points[2]);
			CreateTriangle (points[1], points[3], points[5]);

			CreateQuad (points[2], points[5], points[6], points[3]);
			break;
		case 69:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);

			CreateTriangle (points[1], points[6], points[2]);
			CreateTriangle (points[1], points[3], points[5]);
			CreateTriangle (points[8], points[3], points[2]);

			CreateQuad (points[10], points[6], points[9], points[5]);
			CreateQuad (points[10], points[3], points[5], points[8]);
			CreateQuad (points[2], points[9], points[6], points[8]);
			break;
		case 70:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [6], points [9], false);

			CreateTriangle (points[6], points[5], points[9]);
			CreateTriangle (points[1], points[3], points[5]);
			CreateTriangle (points[8], points[3], points[2]);

			CreateQuad (points[1], points[8], points[6], points[2]);
			CreateQuad (points[9], points[3], points[5], points[8]);
			break;
		case 71:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [6], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [12], false);

			CreateTriangle (points[1], points[3], points[5]);
			CreateTriangle (points[9], points[11], points[8]);

			CreateQuad (points[1], points[8], points[6], points[2]);
			CreateQuad (points[12], points[6], points[9], points[5]);
			CreateQuad (points[11], points[2], points[3], points[8]);
			CreateQuad (points[12], points[3], points[5], points[11]);
			break;
		case 72:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points[5], points[1], points[7], points[3]);
			CreateQuad (points[1], points[6], points[7], points[2]);
			CreateQuad (points[6], points[3], points[5], points[2]);
			break;
		case 73:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [6], false);

			CreateTriangle (points[6], points[10], points[7]);
			CreateTriangle (points[6], points[5], points[9]);
			CreateTriangle (points[2], points[10], points[9]);

			CreateQuad (points[5], points[1], points[7], points[3]);
			CreateQuad (points[1], points[10], points[7], points[2]);
			CreateQuad (points[3], points[9], points[2], points[5]);
			break;
		case 74:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);

			CreateTriangle (points[1], points[7], points[10]);

			CreateQuad (points[5], points[1], points[7], points[3]);
			CreateQuad (points[6], points[10], points[11], points[7]);
			CreateQuad (points[1], points[9], points[10], points[2]);
			CreateQuad (points[9], points[6], points[11], points[5]);
			CreateQuad (points[9], points[3], points[5], points[2]);
			break;
		case 75:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [13], points [11], points [6], false);

			CreateTriangle (points[1], points[7], points[10]);
			CreateTriangle (points[13], points[3], points[2]);

			CreateQuad (points[5], points[1], points[7], points[3]);
			CreateQuad (points[6], points[10], points[11], points[7]);
			CreateQuad (points[1], points[9], points[10], points[2]);
			CreateQuad (points[9], points[13], points[11], points[2]);
			CreateQuad (points[13], points[5], points[6], points[3]);
			break;
		case 76:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [7], points [9], false);

			CreateTriangle (points[1], points[9], points[2]);

			CreateQuad (points[5], points[1], points[7], points[3]);
			CreateQuad (points[9], points[6], points[7], points[2]);
			CreateQuad (points[2], points[5], points[6], points[3]);
			break;
		case 77:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [7], points [9], false);
			CreateCornerMesh (points [10], points [11], points [12], points [6], false);

			CreateQuad (points[5], points[1], points[7], points[3]);
			CreateQuad (points[6], points[9], points[12], points[7]);
			CreateQuad (points[2], points[9], points[1], points[12]);
			CreateQuad (points[2], points[11], points[12], points[3]);
			CreateQuad (points[11], points[5], points[6], points[3]);
			break;
		case 78:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [7], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [12], false);

			CreateQuad (points[5], points[1], points[7], points[3]);
			CreateQuad (points[6], points[9], points[12], points[7]);
			CreateQuad (points[1], points[11], points[9], points[2]);
			CreateQuad (points[12], points[5], points[6], points[11]);
			CreateQuad (points[2], points[5], points[11], points[3]);
			break;
		case 79:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [7], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [12], false);
			CreateCornerMesh (points [13], points [14], points [12], points [6], false);

			CreateTriangle (points[14], points[3], points[2]);

			CreateQuad (points[5], points[1], points[7], points[3]);
			CreateQuad (points[6], points[9], points[12], points[7]);
			CreateQuad (points[1], points[11], points[9], points[2]);
			CreateQuad (points[11], points[14], points[12], points[2]);
			CreateQuad (points[14], points[5], points[6], points[3]);
			break;
		case 80:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[6], points[1], points[5], points[3]);
			CreateQuad (points[1], points[7], points[5], points[2]);
			break;
		case 81:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [5], points [9], points [10], false);

			CreateTriangle (points[5], points[10], points[6]);
			CreateTriangle (points[5], points[7], points[9]);
			CreateTriangle (points[1], points[10], points[9]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[2], points[9], points[1], points[7]);
			CreateQuad (points[10], points[3], points[1], points[6]);
			break;
		case 82:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);

			CreateTriangle (points[9], points[7], points[2]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[1], points[9], points[10], points[2]);
			CreateQuad (points[9], points[5], points[11], points[7]);
			CreateQuad (points[5], points[3], points[1], points[6]);
			CreateQuad (points[11], points[1], points[10], points[5]);
			break;
		case 83:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [5], points [11], points [13], false);

			CreateTriangle (points[9], points[7], points[2]);
			CreateTriangle (points[11], points[10], points[13]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[1], points[9], points[10], points[2]);
			CreateQuad (points[9], points[5], points[11], points[7]);
			CreateQuad (points[6], points[13], points[5], points[3]);
			CreateQuad (points[13], points[1], points[10], points[3]);
			break;
		case 84:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);

			CreateTriangle (points[1], points[3], points[9]);
			CreateTriangle (points[1], points[10], points[2]);
			CreateTriangle (points[5], points[10], points[9]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[6], points[9], points[5], points[3]);
			CreateQuad (points[10], points[7], points[5], points[2]);
			break;
		case 85:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);
			CreateCornerMesh (points [11], points [5], points [12], points [13], false);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[13], points[10], points[12], points[9]);
			CreateQuad (points[6], points[13], points[5], points[3]);
			CreateQuad (points[9], points[3], points[1], points[13]);
			CreateQuad (points[2], points[10], points[1], points[7]);
			CreateQuad (points[12], points[7], points[5], points[10]);
			break;
		case 86:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);

			CreateTriangle (points[10], points[9], points[13]);
			CreateTriangle (points[12], points[7], points[2]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[1], points[12], points[10], points[2]);
			CreateQuad (points[12], points[5], points[13], points[7]);
			CreateQuad (points[9], points[3], points[1], points[6]);
			CreateQuad (points[5], points[9], points[13], points[6]);
			break;
		case 87:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [15], false);

			CreateTriangle (points[12], points[7], points[2]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[1], points[12], points[10], points[2]);
			CreateQuad (points[12], points[5], points[13], points[7]);
			CreateQuad (points[15], points[10], points[13], points[9]);
			CreateQuad (points[6], points[15], points[5], points[3]);
			CreateQuad (points[9], points[3], points[1], points[15]);
			break;
		case 88:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);

			CreateTriangle (points[9], points[3], points[6]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[5], points[9], points[10], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[1], points[7], points[5], points[2]);
			CreateQuad (points[11], points[5], points[10], points[1]);
			break;
		case 89:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [5], points [13], points [10], false);

			CreateTriangle (points[9], points[3], points[6]);
			CreateTriangle (points[10], points[13], points[11]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[5], points[9], points[10], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[13], points[7], points[5], points[2]);
			CreateQuad (points[1], points[13], points[11], points[2]);
			break;
		case 90:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

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
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [13], points [14], points [15], false);

			CreateTriangle (points[9], points[3], points[6]);
			CreateTriangle (points[1], points[11], points[14]);
			CreateTriangle (points[13], points[7], points[2]);
			CreateTriangle (points[5], points[15], points[10]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[5], points[9], points[10], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[1], points[13], points[14], points[2]);
			CreateQuad (points[13], points[5], points[15], points[7]);
			CreateQuad (points[10], points[14], points[15], points[11]);
			break;
		case 91:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

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

				cube.topSquare.forwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [13], points [14], points [15], false);
			CreateCornerMesh (points [16], points [5], points [15], points [10], false);

			CreateTriangle (points[9], points[3], points[6]);
			CreateTriangle (points[1], points[11], points[14]);
			CreateTriangle (points[13], points[7], points[2]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[5], points[9], points[10], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[1], points[13], points[14], points[2]);
			CreateQuad (points[13], points[5], points[15], points[7]);
			CreateQuad (points[10], points[14], points[15], points[11]);
			break;
		case 92:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [1], points [11], points [13], false);

			CreateTriangle (points[9], points[3], points[6]);
			CreateTriangle (points[11], points[10], points[13]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[5], points[9], points[10], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[2], points[13], points[1], points[7]);
			CreateQuad (points[13], points[5], points[10], points[7]);
			break;
		case 93:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [1], points [11], points [13], false);
			CreateCornerMesh (points [14], points [5], points [15], points [10], false);

			CreateTriangle (points[9], points[3], points[6]);
			CreateTriangle (points[11], points[10], points[13]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[5], points[9], points[10], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[10], points[13], points[15], points[11]);
			CreateQuad (points[2], points[13], points[1], points[7]);
			CreateQuad (points[15], points[7], points[5], points[13]);
			break;
		case 94:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [1], points [11], points [13], false);
			CreateCornerMesh (points [14], points [15], points [13], points [16], false);

			CreateTriangle (points[9], points[3], points[6]);
			CreateTriangle (points[15], points[7], points[2]);
			CreateTriangle (points[5], points[16], points[10]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[5], points[9], points[10], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[10], points[13], points[16], points[11]);
			CreateQuad (points[1], points[15], points[13], points[2]);
			CreateQuad (points[15], points[5], points[16], points[7]);
			break;
		case 95:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [1], points [11], points [13], false);
			CreateCornerMesh (points [14], points [15], points [13], points [16], false);
			CreateCornerMesh (points [17], points [5], points [16], points [10], false);

			CreateTriangle (points[9], points[3], points[6]);
			CreateTriangle (points[15], points[7], points[2]);

			CreateQuad (points[6], points[2], points[3], points[7]);
			CreateQuad (points[5], points[9], points[10], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[10], points[13], points[16], points[11]);
			CreateQuad (points[1], points[15], points[13], points[2]);
			CreateQuad (points[15], points[5], points[16], points[7]);
			break;
		case 96:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);

			CreateTriangle (points[2], points[6], points[3]);
			CreateTriangle (points[2], points[1], points[5]);

			CreateQuad (points[5], points[3], points[1], points[6]);
			break;
		case 97:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);

			CreateTriangle (points[2], points[6], points[3]);
			CreateTriangle (points[2], points[1], points[5]);
			CreateTriangle (points[1], points[3], points[10]);

			CreateQuad (points[5], points[8], points[9], points[6]);
			CreateQuad (points[1], points[9], points[10], points[5]);
			CreateQuad (points[8], points[3], points[10], points[6]);
			break;
		case 98:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [5], points [8], points [9], false);

			CreateTriangle (points[2], points[6], points[3]);

			CreateQuad (points[1], points[5], points[8], points[2]);
			CreateQuad (points[6], points[9], points[5], points[3]);
			CreateQuad (points[3], points[8], points[9], points[1]);
			break;
		case 99:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [5], points [8], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [12], false);

			CreateQuad (points[1], points[5], points[8], points[2]);
			CreateQuad (points[5], points[11], points[9], points[6]);
			CreateQuad (points[12], points[8], points[9], points[1]);
			CreateQuad (points[3], points[6], points[2], points[11]);
			CreateQuad (points[12], points[3], points[1], points[11]);
			break;
		case 100:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);

			CreateTriangle (points[2], points[6], points[3]);

			CreateQuad (points[1], points[5], points[9], points[2]);
			CreateQuad (points[8], points[3], points[1], points[6]);
			CreateQuad (points[9], points[6], points[8], points[5]);
			break;
		case 101:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);

			CreateTriangle (points[5], points[9], points[12]);
			CreateTriangle (points[1], points[3], points[8]);

			CreateQuad (points[1], points[5], points[9], points[2]);
			CreateQuad (points[5], points[11], points[12], points[6]);
			CreateQuad (points[13], points[9], points[12], points[8]);
			CreateQuad (points[6], points[3], points[11], points[2]);
			CreateQuad (points[13], points[3], points[8], points[11]);
			break;
		case 102:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);
			CreateCornerMesh (points [10], points [5], points [9], points [11], false);

			CreateTriangle (points[2], points[6], points[3]);
			CreateTriangle (points[1], points[3], points[8]);
			CreateTriangle (points[9], points[8], points[11]);
			CreateTriangle (points[5], points[11], points[6]);

			CreateQuad (points[1], points[5], points[9], points[2]);
			CreateQuad (points[11], points[3], points[8], points[6]);
			break;
		case 103:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);
			CreateCornerMesh (points [10], points [5], points [9], points [11], false);
			CreateCornerMesh (points [12], points [13], points [11], points [14], false);

			CreateTriangle (points[1], points[3], points[8]);

			CreateQuad (points[1], points[5], points[9], points[2]);
			CreateQuad (points[5], points[13], points[11], points[6]);
			CreateQuad (points[14], points[9], points[11], points[8]);
			CreateQuad (points[6], points[3], points[13], points[2]);
			CreateQuad (points[14], points[3], points[8], points[13]);
			break;
		case 104:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);

			CreateTriangle (points[5], points[10], points[9]);

			CreateQuad (points[8], points[1], points[10], points[3]);
			CreateQuad (points[1], points[5], points[10], points[2]);
			CreateQuad (points[6], points[3], points[8], points[2]);
			CreateQuad (points[6], points[9], points[5], points[8]);
			break;
		case 105:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [13], points [9], false);

			CreateQuad (points[8], points[1], points[10], points[3]);
			CreateQuad (points[5], points[12], points[13], points[6]);
			CreateQuad (points[6], points[3], points[12], points[2]);
			CreateQuad (points[12], points[8], points[9], points[3]);
			CreateQuad (points[10], points[13], points[9], points[5]);
			CreateQuad (points[1], points[5], points[10], points[2]);
			break;
		case 106:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [5], points [12], points [13], false);

			CreateTriangle (points[5], points[13], points[6]);
			CreateTriangle (points[1], points[10], points[12]);

			CreateQuad (points[8], points[1], points[10], points[3]);
			CreateQuad (points[1], points[5], points[12], points[2]);
			CreateQuad (points[9], points[12], points[13], points[10]);
			CreateQuad (points[6], points[3], points[8], points[2]);
			CreateQuad (points[6], points[9], points[13], points[8]);
			break;
		case 107:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [5], points [12], points [13], false);
			CreateCornerMesh (points [14], points [15], points [13], points [9], false);

			CreateTriangle (points[1], points[10], points[12]);

			CreateQuad (points[8], points[1], points[10], points[3]);
			CreateQuad (points[1], points[5], points[12], points[2]);
			CreateQuad (points[9], points[12], points[13], points[10]);
			CreateQuad (points[5], points[15], points[13], points[6]);
			CreateQuad (points[15], points[8], points[9], points[3]);
			CreateQuad (points[6], points[3], points[15], points[2]);
			break;
		case 108:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [1], points [10], points [12], false);

			CreateQuad (points[8], points[1], points[10], points[3]);
			CreateQuad (points[1], points[5], points[12], points[2]);
			CreateQuad (points[12], points[9], points[10], points[5]);
			CreateQuad (points[6], points[3], points[8], points[2]);
			CreateQuad (points[6], points[9], points[5], points[8]);
			break;
		case 109:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [1], points [10], points [12], false);
			CreateCornerMesh (points [13], points [14], points [15], points [9], false);

			CreateTriangle (points[5], points[12], points[15]);

			CreateQuad (points[8], points[1], points[10], points[3]);
			CreateQuad (points[1], points[5], points[12], points[2]);
			CreateQuad (points[9], points[12], points[15], points[10]);
			CreateQuad (points[5], points[14], points[15], points[6]);
			CreateQuad (points[6], points[3], points[14], points[2]);
			CreateQuad (points[14], points[8], points[9], points[3]);
			break;
		case 110:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [1], points [10], points [12], false);
			CreateCornerMesh (points [13], points [5], points [12], points [14], false);

			CreateTriangle (points[6], points[9], points[8]);

			CreateQuad (points[8], points[1], points[10], points[3]);
			CreateQuad (points[1], points[5], points[12], points[2]);
			CreateQuad (points[9], points[12], points[14], points[10]);
			CreateQuad (points[6], points[3], points[8], points[2]);
			CreateQuad (points[6], points[14], points[5], points[9]);
			break;
		case 111:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [1], points [10], points [12], false);
			CreateCornerMesh (points [13], points [5], points [12], points [14], false);
			CreateCornerMesh (points [15], points [16], points [14], points [9], false);

			CreateQuad (points[8], points[1], points[10], points[3]);
			CreateQuad (points[1], points[5], points[12], points[2]);
			CreateQuad (points[9], points[12], points[14], points[10]);
			CreateQuad (points[5], points[16], points[14], points[6]);
			CreateQuad (points[6], points[3], points[16], points[2]);
			CreateQuad (points[16], points[8], points[9], points[3]);
			break;
		case 112:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);

			CreateTriangle (points[2], points[1], points[5]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[8], points[5], points[6], points[1]);
			CreateQuad (points[8], points[3], points[1], points[9]);
			break;
		case 113:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [8], points [11], points [12], false);

			CreateTriangle (points[1], points[12], points[11]);
			CreateTriangle (points[12], points[1], points[3]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[5], points[8], points[11], points[6]);
			CreateQuad (points[1], points[5], points[11], points[2]);
			CreateQuad (points[9], points[12], points[8], points[3]);
			break;
		case 114:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [5], points [11], points [12], false);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[5], points[8], points[12], points[6]);
			CreateQuad (points[1], points[5], points[11], points[2]);
			CreateQuad (points[9], points[1], points[8], points[3]);
			CreateQuad (points[11], points[8], points[1], points[12]);
			break;
		case 115:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [5], points [11], points [12], false);
			CreateCornerMesh (points [13], points [8], points [12], points [14], false);

			CreateTriangle (points[8], points[14], points[9]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[5], points[8], points[12], points[6]);
			CreateQuad (points[1], points[5], points[11], points[2]);
			CreateQuad (points[14], points[11], points[12], points[1]);
			CreateQuad (points[14], points[3], points[1], points[9]);
			break;
		case 116:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);

			CreateTriangle (points[8], points[12], points[11]);
			CreateTriangle (points[11], points[9], points[8]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[1], points[5], points[12], points[2]);
			CreateQuad (points[5], points[8], points[12], points[6]);
			CreateQuad (points[11], points[3], points[1], points[9]);
			break;
		case 117:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);
			CreateCornerMesh (points [13], points [8], points [14], points [15], false);

			CreateTriangle (points[5], points[12], points[14]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[1], points[5], points[12], points[2]);
			CreateQuad (points[5], points[8], points[14], points[6]);
			CreateQuad (points[15], points[12], points[14], points[11]);
			CreateQuad (points[9], points[15], points[8], points[3]);
			CreateQuad (points[11], points[3], points[1], points[15]);
			break;
		case 118:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);
			CreateCornerMesh (points [13], points [5], points [12], points [14], false);

			CreateTriangle (points[11], points[9], points[8]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[1], points[5], points[12], points[2]);
			CreateQuad (points[5], points[8], points[14], points[6]);
			CreateQuad (points[14], points[11], points[12], points[8]);
			CreateQuad (points[11], points[3], points[1], points[9]);
			break;
		case 119:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);
			CreateCornerMesh (points [13], points [5], points [12], points [14], false);
			CreateCornerMesh (points [15], points [8], points [14], points [16], false);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[1], points[5], points[12], points[2]);
			CreateQuad (points[5], points[8], points[14], points[6]);
			CreateQuad (points[16], points[12], points[14], points[11]);
			CreateQuad (points[9], points[16], points[8], points[3]);
			CreateQuad (points[11], points[3], points[1], points[16]);
			break;
		case 120:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);

			CreateTriangle (points [5], points [13], points [12]);
			CreateTriangle (points [11], points [3], points [9]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[11], points[1], points[13], points[3]);
			CreateQuad (points[8], points[11], points[12], points[9]);
			CreateQuad (points[1], points[5], points[13], points[2]);
			CreateQuad (points[5], points[8], points[12], points[6]);
			break;
		case 121:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);
			CreateCornerMesh (points [14], points [8], points [15], points [12], false);

			CreateTriangle (points [11], points [3], points [9]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[11], points[1], points[13], points[3]);
			CreateQuad (points[8], points[11], points[12], points[9]);
			CreateQuad (points[5], points[8], points[15], points[6]);
			CreateQuad (points[13], points[15], points[12], points[1]);
			CreateQuad (points[1], points[5], points[15], points[2]);
			break;
		case 122:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);
			CreateCornerMesh (points [14], points [5], points [15], points [16], false);

			CreateTriangle (points [11], points [3], points [9]);
			CreateTriangle (points [1], points [13], points [15]);
			CreateTriangle (points [8], points [16], points [12]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[11], points[1], points[13], points[3]);
			CreateQuad (points[8], points[11], points[12], points[9]);
			CreateQuad (points[5], points[8], points[16], points[6]);
			CreateQuad (points[1], points[5], points[15], points[2]);
			CreateQuad (points[12], points[15], points[16], points[13]);
			break;
		case 123:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);
			CreateCornerMesh (points [14], points [5], points [15], points [16], false);
			CreateCornerMesh (points [17], points [8], points [16], points [12], false);

			CreateTriangle (points [11], points [3], points [9]);
			CreateTriangle (points [1], points [13], points [15]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[11], points[1], points[13], points[3]);
			CreateQuad (points[8], points[11], points[12], points[9]);
			CreateQuad (points[5], points[8], points[16], points[6]);
			CreateQuad (points[1], points[5], points[15], points[2]);
			CreateQuad (points[12], points[15], points[16], points[13]);
			break;
		case 124:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);
			CreateCornerMesh (points [14], points [1], points [13], points [15], false);

			CreateTriangle (points [11], points [3], points [9]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[11], points[1], points[13], points[3]);
			CreateQuad (points[1], points[5], points[15], points[2]);
			CreateQuad (points[8], points[11], points[12], points[9]);
			CreateQuad (points[15], points[12], points[13], points[5]);
			CreateQuad (points[5], points[8], points[12], points[6]);
			break;
		case 125:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);
			CreateCornerMesh (points [14], points [1], points [13], points [15], false);
			CreateCornerMesh (points [16], points [8], points [17], points [12], false);

			CreateTriangle (points [11], points [3], points [9]);
			CreateTriangle (points [5], points [15], points [17]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[11], points[1], points[13], points[3]);
			CreateQuad (points[1], points[5], points[15], points[2]);
			CreateQuad (points[8], points[11], points[12], points[9]);
			CreateQuad (points[12], points[15], points[17], points[13]);
			CreateQuad (points[5], points[8], points[17], points[6]);
			break;
		case 126:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);
			CreateCornerMesh (points [14], points [1], points [13], points [15], false);
			CreateCornerMesh (points [16], points [5], points [15], points [17], false);

			CreateTriangle (points [11], points [3], points [9]);
			CreateTriangle (points [8], points [17], points [12]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[11], points[1], points[13], points[3]);
			CreateQuad (points[1], points[5], points[15], points[2]);
			CreateQuad (points[8], points[11], points[12], points[9]);
			CreateQuad (points[12], points[15], points[17], points[13]);
			CreateQuad (points[5], points[8], points[17], points[6]);
			break;
		case 127:
			points = new Node[] {
				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);
			CreateCornerMesh (points [14], points [1], points [13], points [15], false);
			CreateCornerMesh (points [16], points [5], points [15], points [17], false);
			CreateCornerMesh (points [18], points [8], points [17], points [12], false);

			CreateTriangle (points [11], points [3], points [9]);

			CreateQuad (points[9], points[2], points[3], points[6]);
			CreateQuad (points[11], points[1], points[13], points[3]);
			CreateQuad (points[1], points[5], points[15], points[2]);
			CreateQuad (points[8], points[11], points[12], points[9]);
			CreateQuad (points[12], points[15], points[17], points[13]);
			CreateQuad (points[5], points[8], points[17], points[6]);
			break;
		case 128:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], true);
			break;
		case 129:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points[5], points[1], points[7], points[3]);
			CreateQuad (points[7], points[2], points[6], points[1]);
			CreateQuad (points[2], points[5], points[6], points[3]);
			break;
		case 130:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateTriangle (points [1], points [7], points [6]);
			CreateTriangle (points [5], points [3], points [2]);

			CreateQuad (points[2], points[6], points[1], points[5]);
			CreateQuad (points[7], points[3], points[1], points[5]);
			break;
		case 131:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);

			CreateTriangle (points [5], points [3], points [2]);

			CreateQuad (points[9], points[1], points[10], points[3]);
			CreateQuad (points[5], points[9], points[7], points[3]);
			CreateQuad (points[10], points[6], points[7], points[1]);
			CreateQuad (points[2], points[6], points[1], points[5]);
			break;
		case 132:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points[1], points[5], points[6], points[2]);
			CreateQuad (points[6], points[3], points[1], points[7]);
			CreateQuad (points[5], points[3], points[7], points[2]);
			break;
		case 133:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

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
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);

			CreateTriangle (points [1], points [11], points [6]);

			CreateQuad (points[1], points[5], points[6], points[2]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[11], points[7], points[10], points[6]);
			CreateQuad (points[2], points[9], points[5], points[3]);
			CreateQuad (points[5], points[10], points[7], points[9]);
			break;
		case 134:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);

			CreateTriangle (points [9], points [3], points [2]);

			CreateQuad (points[1], points[5], points[6], points[2]);
			CreateQuad (points[5], points[9], points[7], points[2]);
			CreateQuad (points[10], points[6], points[7], points[1]);
			CreateQuad (points[10], points[3], points[1], points[9]);
			break;
		case 135:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);

			CreateTriangle (points [9], points [3], points [2]);
			CreateTriangle (points [1], points [13], points [6]);

			CreateQuad (points[1], points[5], points[6], points[2]);
			CreateQuad (points[12], points[1], points[13], points[3]);
			CreateQuad (points[13], points[7], points[10], points[6]);
			CreateQuad (points[5], points[9], points[7], points[2]);
			CreateQuad (points[9], points[12], points[10], points[3]);
			break;
		case 136:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);

			CreateQuad (points[2], points[6], points[1], points[3]);
			CreateQuad (points[5], points[3], points[1], points[6]);
			break;
		case 137:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [5], false);

			CreateTriangle (points [5], points [9], points [6]);

			CreateQuad (points[8], points[1], points[5], points[3]);
			CreateQuad (points[2], points[6], points[1], points[9]);
			CreateQuad (points[2], points[8], points[9], points[3]);
			break;
		case 138:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);

			CreateTriangle (points [8], points [3], points [2]);
			CreateTriangle (points [1], points [6], points [2]);
			CreateTriangle (points [1], points [3], points [5]);

			CreateQuad (points[5], points[9], points[10], points[6]);
			CreateQuad (points[2], points[9], points[6], points[8]);
			CreateQuad (points[10], points[3], points[5], points[8]);
			break;
		case 139:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [5], false);

			CreateQuad (points[5], points[9], points[10], points[6]);
			CreateQuad (points[12], points[1], points[5], points[3]);
			CreateQuad (points[8], points[12], points[10], points[3]);
			CreateQuad (points[2], points[6], points[1], points[9]);
			CreateQuad (points[2], points[8], points[9], points[3]);
			break;
		case 140:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [6], points [9], false);

			CreateTriangle (points [6], points [5], points [9]);

			CreateQuad (points[1], points[8], points[6], points[2]);
			CreateQuad (points[5], points[3], points[1], points[9]);
			CreateQuad (points[8], points[3], points[9], points[2]);
			break;
		case 141:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [6], points [9], false);
			CreateCornerMesh (points [10], points [11], points [12], points [5], false);

			CreateQuad (points[1], points[8], points[6], points[2]);
			CreateQuad (points[5], points[9], points[12], points[6]);
			CreateQuad (points[11], points[1], points[5], points[3]);
			CreateQuad (points[2], points[11], points[8], points[3]);
			CreateQuad (points[8], points[12], points[9], points[11]);
			break;
		case 142:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [6], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [12], false);

			CreateQuad (points[1], points[8], points[6], points[2]);
			CreateQuad (points[5], points[9], points[12], points[6]);
			CreateQuad (points[8], points[11], points[9], points[2]);
			CreateQuad (points[5], points[3], points[1], points[12]);
			CreateQuad (points[11], points[3], points[12], points[2]);
			break;
		case 143:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [1], points [5], points [6], false);
			CreateCornerMesh (points [7], points [8], points [6], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [12], false);
			CreateCornerMesh (points [13], points [14], points [12], points [5], false);

			CreateTriangle (points [11], points [3], points [2]);

			CreateQuad (points[1], points[8], points[6], points[2]);
			CreateQuad (points[5], points[9], points[12], points[6]);
			CreateQuad (points[14], points[1], points[5], points[3]);
			CreateQuad (points[8], points[11], points[9], points[2]);
			CreateQuad (points[11], points[14], points[12], points[3]);
			break;
		case 144:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);

			CreateQuad (points[2], points[6], points[1], points[3]);
			CreateQuad (points[5], points[1], points[6], points[3]);
			break;
		case 145:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [5], points [8], points [9], false);

			CreateTriangle (points [3], points [2], points [6]);

			CreateQuad (points[5], points[1], points[9], points[3]);
			CreateQuad (points[6], points[8], points[2], points[5]);
			CreateQuad (points[9], points[2], points[8], points[1]);
			break;
		case 146:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);

			CreateTriangle (points [1], points [10], points [9]);

			CreateQuad (points[8], points[5], points[10], points[6]);
			CreateQuad (points[5], points[1], points[10], points[3]);
			CreateQuad (points[2], points[6], points[8], points[3]);
			CreateQuad (points[2], points[9], points[1], points[8]);
			break;
		case 147:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [5], points [10], points [12], false);

			CreateQuad (points[8], points[5], points[10], points[6]);
			CreateQuad (points[5], points[1], points[12], points[3]);
			CreateQuad (points[2], points[6], points[8], points[3]);
			CreateQuad (points[12], points[9], points[10], points[1]);
			CreateQuad (points[2], points[9], points[1], points[8]);
			break;
		case 148:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);

			CreateTriangle (points [5], points [10], points [9]);

			CreateQuad (points[1], points[8], points[9], points[2]);
			CreateQuad (points[5], points[1], points[9], points[3]);
			CreateQuad (points[2], points[6], points[8], points[3]);
			CreateQuad (points[10], points[6], points[5], points[8]);
			break;
		case 149:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [5], points [12], points [13], false);

			CreateTriangle (points [1], points [13], points [9]);
			CreateTriangle (points [6], points [8], points [10]);

			CreateQuad (points[1], points[8], points[9], points[2]);
			CreateQuad (points[5], points[1], points[13], points[3]);
			CreateQuad (points[13], points[10], points[12], points[9]);
			CreateQuad (points[2], points[6], points[8], points[3]);
			CreateQuad (points[12], points[6], points[5], points[10]);
			break;
		case 150:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);

			CreateQuad (points[1], points[8], points[9], points[2]);
			CreateQuad (points[12], points[5], points[13], points[6]);
			CreateQuad (points[2], points[6], points[8], points[3]);
			CreateQuad (points[8], points[12], points[10], points[6]);
			CreateQuad (points[5], points[1], points[13], points[3]);
			CreateQuad (points[13], points[9], points[10], points[1]);
			break;
		case 151:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [15], false);

			CreateTriangle (points [1], points [15], points [9]);

			CreateQuad (points[1], points[8], points[9], points[2]);
			CreateQuad (points[12], points[5], points[13], points[6]);
			CreateQuad (points[5], points[1], points[15], points[3]);
			CreateQuad (points[15], points[10], points[13], points[9]);
			CreateQuad (points[2], points[6], points[8], points[3]);
			CreateQuad (points[8], points[12], points[10], points[6]);
			break;
		case 152:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);

			CreateTriangle (points [3], points [2], points [6]);

			CreateQuad (points[5], points[1], points[8], points[3]);
			CreateQuad (points[2], points[9], points[1], points[6]);
			CreateQuad (points[8], points[6], points[5], points[9]);
			break;
		case 153:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);
			CreateCornerMesh (points [10], points [5], points [11], points [8], false);

			CreateTriangle (points [3], points [2], points [6]);
			CreateTriangle (points [8], points [11], points [9]);

			CreateQuad (points[5], points[1], points[8], points[3]);
			CreateQuad (points[2], points[9], points[1], points[11]);
			CreateQuad (points[11], points[6], points[5], points[2]);
			break;
		case 154:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);

			CreateTriangle (points [5], points [13], points [8]);
			CreateTriangle (points [2], points [12], points [11]);

			CreateQuad (points[5], points[1], points[8], points[3]);
			CreateQuad (points[8], points[12], points[13], points[9]);
			CreateQuad (points[11], points[5], points[13], points[6]);
			CreateQuad (points[2], points[6], points[11], points[3]);
			CreateQuad (points[2], points[9], points[1], points[12]);
			break;
		case 155:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [8], false);

			CreateTriangle (points [2], points [12], points [11]);

			CreateQuad (points[5], points[1], points[8], points[3]);
			CreateQuad (points[8], points[12], points[13], points[9]);
			CreateQuad (points[11], points[5], points[13], points[6]);
			CreateQuad (points[2], points[6], points[11], points[3]);
			CreateQuad (points[2], points[9], points[1], points[12]);
			break;
		case 156:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [12], false);

			CreateQuad (points[5], points[1], points[8], points[3]);
			CreateQuad (points[1], points[11], points[9], points[2]);
			CreateQuad (points[2], points[6], points[11], points[3]);
			CreateQuad (points[12], points[8], points[9], points[5]);
			CreateQuad (points[12], points[6], points[5], points[11]);
			break;
		case 157:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [12], false);
			CreateCornerMesh (points [13], points [5], points [14], points [8], false);

			CreateTriangle (points [6], points [11], points [12]);

			CreateQuad (points[5], points[1], points[8], points[3]);
			CreateQuad (points[1], points[11], points[9], points[2]);
			CreateQuad (points[8], points[12], points[14], points[9]);
			CreateQuad (points[2], points[6], points[11], points[3]);
			CreateQuad (points[14], points[6], points[5], points[12]);
			break;
		case 158:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [12], false);
			CreateCornerMesh (points [13], points [14], points [12], points [15], false);

			CreateTriangle (points [5], points [15], points [8]);

			CreateQuad (points[5], points[1], points[8], points[3]);
			CreateQuad (points[1], points[11], points[9], points[2]);
			CreateQuad (points[8], points[12], points[15], points[9]);
			CreateQuad (points[14], points[5], points[15], points[6]);
			CreateQuad (points[2], points[6], points[11], points[3]);
			CreateQuad (points[11], points[14], points[12], points[6]);
			break;
		case 159:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [3], points [6], false);
			CreateCornerMesh (points [7], points [1], points [8], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [12], false);
			CreateCornerMesh (points [13], points [14], points [12], points [15], false);
			CreateCornerMesh (points [16], points [5], points [15], points [8], false);

			CreateQuad (points[5], points[1], points[8], points[3]);
			CreateQuad (points[1], points[11], points[9], points[2]);
			CreateQuad (points[8], points[12], points[15], points[9]);
			CreateQuad (points[14], points[5], points[15], points[6]);
			CreateQuad (points[2], points[6], points[11], points[3]);
			CreateQuad (points[11], points[14], points[12], points[6]);
			break;
		case 160:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[1], points[7], points[5], points[2]);
			CreateQuad (points[5], points[3], points[1], points[6]);
			break;
		case 161:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);

			CreateTriangle (points [9], points [3], points [6]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[5], points[9], points[10], points[6]);
			CreateQuad (points[2], points[5], points[1], points[7]);
			CreateQuad (points[1], points[10], points[11], points[5]);
			break;
		case 162:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [5], points [9], points [10], false);

			CreateTriangle (points [1], points [10], points [9]);
			CreateTriangle (points [5], points [7], points [9]);
			CreateTriangle (points [5], points [10], points [6]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[2], points[9], points[1], points[7]);
			CreateQuad (points[10], points[3], points[1], points[6]);
			break;
		case 163:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [5], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);

			CreateTriangle (points [12], points [3], points [6]);
			CreateTriangle (points [9], points [2], points [1]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[5], points[12], points[10], points[6]);
			CreateQuad (points[12], points[1], points[13], points[3]);
			CreateQuad (points[13], points[9], points[10], points[1]);
			CreateQuad (points[9], points[7], points[5], points[2]);
			break;
		case 164:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);

			CreateTriangle (points [9], points [7], points [2]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[5], points[11], points[7]);
			CreateQuad (points[1], points[9], points[10], points[2]);
			CreateQuad (points[6], points[1], points[5], points[3]);
			CreateQuad (points[5], points[10], points[11], points[1]);
			break;
		case 165:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

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
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [13], points [14], points [15], false);

			CreateTriangle (points [9], points [7], points [2]);
			CreateTriangle (points [5], points [11], points [14]);
			CreateTriangle (points [13], points [3], points [6]);
			CreateTriangle (points [1], points [15], points [10]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[5], points[11], points[7]);
			CreateQuad (points[1], points[9], points[10], points[2]);
			CreateQuad (points[5], points[13], points[14], points[6]);
			CreateQuad (points[13], points[1], points[15], points[3]);
			CreateQuad (points[15], points[11], points[14], points[10]);
			break;
		case 166:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

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
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [5], points [11], points [13], false);

			CreateTriangle (points [9], points [7], points [2]);
			CreateTriangle (points [13], points [1], points [3]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[5], points[11], points[7]);
			CreateQuad (points[1], points[9], points[10], points[2]);
			CreateQuad (points[13], points[10], points[11], points[1]);
			CreateQuad (points[6], points[13], points[5], points[3]);
			break;
		case 167:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

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
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [10], points [11], false);
			CreateCornerMesh (points [12], points [5], points [11], points [13], false);
			CreateCornerMesh (points [14], points [15], points [13], points [16], false);

			CreateTriangle (points [9], points [7], points [2]);
			CreateTriangle (points [15], points [3], points [6]);
			CreateTriangle (points [1], points [16], points [10]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[5], points[11], points[7]);
			CreateQuad (points[1], points[9], points[10], points[2]);
			CreateQuad (points[5], points[15], points[13], points[6]);
			CreateQuad (points[15], points[1], points[16], points[3]);
			CreateQuad (points[16], points[11], points[13], points[10]);
			break;
		case 168:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);

			CreateTriangle (points [5], points [10], points [9]);
			CreateTriangle (points [1], points [10], points [2]);
			CreateTriangle (points [1], points [3], points [9]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[10], points[7], points[5], points[2]);
			CreateQuad (points[6], points[9], points[5], points[3]);
			break;
		case 169:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [13], points [9], false);

			CreateTriangle (points [12], points [3], points [6]);
			CreateTriangle (points [10], points [5], points [7]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[12], points[1], points[9], points[3]);
			CreateQuad (points[5], points[12], points[13], points[6]);
			CreateQuad (points[10], points[13], points[9], points[5]);
			CreateQuad (points[2], points[10], points[1], points[7]);
			break;
		case 170:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);
			CreateCornerMesh (points [11], points [5], points [12], points [13], false);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[12], points[13], points[10]);
			CreateQuad (points[2], points[10], points[1], points[7]);
			CreateQuad (points[12], points[7], points[5], points[10]);
			CreateQuad (points[6], points[13], points[5], points[3]);
			CreateQuad (points[9], points[3], points[1], points[13]);
			break;
		case 171:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);
			CreateCornerMesh (points [11], points [5], points [12], points [13], false);
			CreateCornerMesh (points [14], points [15], points [13], points [9], false);

			CreateTriangle (points [15], points [3], points [6]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[12], points[13], points[10]);
			CreateQuad (points[15], points[1], points[9], points[3]);
			CreateQuad (points[5], points[15], points[13], points[6]);
			CreateQuad (points[2], points[10], points[1], points[7]);
			CreateQuad (points[12], points[7], points[5], points[10]);
			break;
		case 172:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);

			CreateTriangle (points [12], points [7], points [2]);
			CreateTriangle (points [9], points [6], points [5]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[1], points[12], points[10], points[2]);
			CreateQuad (points[12], points[5], points[13], points[7]);
			CreateQuad (points[13], points[9], points[10], points[5]);
			CreateQuad (points[9], points[3], points[1], points[6]);
			break;
		case 173:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);
			CreateCornerMesh (points [14], points [15], points [16], points [9], false);

			CreateTriangle (points [12], points [7], points [2]);
			CreateTriangle (points [5], points [13], points [16]);
			CreateTriangle (points [15], points [3], points [6]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[1], points[12], points[10], points[2]);
			CreateQuad (points[12], points[5], points[13], points[7]);
			CreateQuad (points[5], points[15], points[16], points[6]);
			CreateQuad (points[15], points[1], points[9], points[3]);
			CreateQuad (points[9], points[13], points[16], points[10]);
			break;
		case 174:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [15], false);

			CreateTriangle (points [12], points [7], points [2]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[1], points[12], points[10], points[2]);
			CreateQuad (points[12], points[5], points[13], points[7]);
			CreateQuad (points[9], points[13], points[15], points[10]);
			CreateQuad (points[6], points[15], points[5], points[3]);
			CreateQuad (points[9], points[3], points[1], points[15]);
			break;
		case 175:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [1], points [9], points [10], false);
			CreateCornerMesh (points [11], points [12], points [10], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [15], false);
			CreateCornerMesh (points [16], points [17], points [15], points [9], false);

			CreateTriangle (points [12], points [7], points [2]);
			CreateTriangle (points [17], points [3], points [6]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[1], points[12], points[10], points[2]);
			CreateQuad (points[12], points[5], points[13], points[7]);
			CreateQuad (points[9], points[13], points[15], points[10]);
			CreateQuad (points[5], points[17], points[15], points[6]);
			CreateQuad (points[17], points[1], points[9], points[3]);
			break;
		case 176:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);

			CreateTriangle (points [6], points [5], points [9]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[2], points[5], points[1], points[7]);
			CreateQuad (points[1], points[9], points[3], points[5]);
			break;
		case 177:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [9], points [11], points [12], false);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[5], points[9], points[11], points[6]);
			CreateQuad (points[9], points[1], points[12], points[3]);
			CreateQuad (points[2], points[5], points[1], points[7]);
			CreateQuad (points[1], points[11], points[12], points[5]);
			break;
		case 178:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [5], points [11], points [12], false);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[5], points[9], points[12], points[6]);
			CreateQuad (points[1], points[9], points[3], points[12]);
			CreateQuad (points[11], points[7], points[5], points[2]);
			CreateQuad (points[1], points[11], points[12], points[2]);
			break;
		case 179:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [5], points [11], points [12], false);
			CreateCornerMesh (points [13], points [9], points [12], points [14], false);

			CreateTriangle (points [11], points [2], points [1]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[5], points[9], points[12], points[6]);
			CreateQuad (points[9], points[1], points[14], points[3]);
			CreateQuad (points[14], points[11], points[12], points[1]);
			CreateQuad (points[11], points[7], points[5], points[2]);
			break;
		case 180:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);

			CreateTriangle (points [11], points [7], points [2]);
			CreateTriangle (points [9], points [13], points [12]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[1], points[11], points[12], points[2]);
			CreateQuad (points[11], points[5], points[13], points[7]);
			CreateQuad (points[9], points[5], points[6], points[13]);
			CreateQuad (points[1], points[9], points[3], points[12]);
			break;
		case 181:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);
			CreateCornerMesh (points [14], points [9], points [15], points [16], false);

			CreateTriangle (points [11], points [7], points [2]);
			CreateTriangle (points [5], points [13], points [15]);
			CreateTriangle (points [1], points [16], points [12]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[1], points[11], points[12], points[2]);
			CreateQuad (points[11], points[5], points[13], points[7]);
			CreateQuad (points[5], points[9], points[15], points[6]);
			CreateQuad (points[9], points[1], points[16], points[3]);
			CreateQuad (points[16], points[13], points[15], points[12]);
			break;
		case 182:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [15], false);

			CreateTriangle (points [11], points [7], points [2]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[1], points[11], points[12], points[2]);
			CreateQuad (points[11], points[5], points[13], points[7]);
			CreateQuad (points[5], points[9], points[15], points[6]);
			CreateQuad (points[15], points[12], points[13], points[9]);
			CreateQuad (points[9], points[1], points[12], points[3]);
			break;
		case 183:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [11], points [12], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [15], false);
			CreateCornerMesh (points [16], points [9], points [15], points [17], false);

			CreateTriangle (points [11], points [7], points [2]);
			CreateTriangle (points [1], points [17], points [12]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[1], points[11], points[12], points[2]);
			CreateQuad (points[11], points[5], points[13], points[7]);
			CreateQuad (points[5], points[9], points[15], points[6]);
			CreateQuad (points[9], points[1], points[17], points[3]);
			CreateQuad (points[17], points[13], points[15], points[12]);
			break;
		case 184:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[5], points[9], points[11], points[6]);
			CreateQuad (points[2], points[12], points[1], points[7]);
			CreateQuad (points[12], points[5], points[11], points[7]);
			break;
		case 185:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);
			CreateCornerMesh (points [13], points [9], points [14], points [11], false);

			CreateTriangle (points [12], points [5], points [7]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[5], points[9], points[14], points[6]);
			CreateQuad (points[12], points[14], points[11], points[5]);
			CreateQuad (points[2], points[12], points[1], points[7]);
			break;
		case 186:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);
			CreateCornerMesh (points [13], points [5], points [14], points [15], false);

			CreateTriangle (points [9], points [15], points [11]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[5], points[9], points[15], points[6]);
			CreateQuad (points[11], points[14], points[15], points[12]);
			CreateQuad (points[2], points[12], points[1], points[7]);
			CreateQuad (points[14], points[7], points[5], points[12]);
			break;
		case 187:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.forwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);
			CreateCornerMesh (points [13], points [5], points [14], points [15], false);
			CreateCornerMesh (points [16], points [9], points [15], points [11], false);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[5], points[9], points[15], points[6]);
			CreateQuad (points[11], points[14], points[15], points[12]);
			CreateQuad (points[2], points[12], points[1], points[7]);
			CreateQuad (points[14], points[7], points[5], points[12]);
			break;
		case 188:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);
			CreateCornerMesh (points [13], points [14], points [12], points [15], false);

			CreateTriangle (points [14], points [7], points [2]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[1], points[14], points[12], points[2]);
			CreateQuad (points[14], points[5], points[15], points[7]);
			CreateQuad (points[15], points[11], points[12], points[5]);
			CreateQuad (points[5], points[9], points[11], points[6]);
			break;
		case 189:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);
			CreateCornerMesh (points [13], points [14], points [12], points [15], false);
			CreateCornerMesh (points [16], points [9], points [17], points [11], false);

			CreateTriangle (points [14], points [7], points [2]);
			CreateTriangle (points [5], points [15], points [17]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[14], points[5], points[15], points[7]);
			CreateQuad (points[1], points[14], points[12], points[2]);
			CreateQuad (points[5], points[9], points[17], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[11], points[15], points[17], points[12]);
			break;
		case 190:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);
			CreateCornerMesh (points [13], points [14], points [12], points [15], false);
			CreateCornerMesh (points [16], points [5], points [15], points [17], false);

			CreateTriangle (points [14], points [7], points [2]);
			CreateTriangle (points [9], points [17], points [11]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[14], points[5], points[15], points[7]);
			CreateQuad (points[1], points[14], points[12], points[2]);
			CreateQuad (points[5], points[9], points[17], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[11], points[15], points[17], points[12]);
			break;
		case 191:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,

				cube.topSquare.backwardLeft,
				cube.topSquare.centreLeft,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.forwardRight,
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [7], false);
			CreateCornerMesh (points [8], points [9], points [3], points [6], false);
			CreateCornerMesh (points [10], points [1], points [11], points [12], false);
			CreateCornerMesh (points [13], points [14], points [12], points [15], false);
			CreateCornerMesh (points [16], points [5], points [15], points [17], false);
			CreateCornerMesh (points [18], points [9], points [17], points [11], false);

			CreateTriangle (points [14], points [7], points [2]);

			CreateQuad (points[3], points[7], points[2], points[6]);
			CreateQuad (points[14], points[5], points[15], points[7]);
			CreateQuad (points[1], points[14], points[12], points[2]);
			CreateQuad (points[5], points[9], points[17], points[6]);
			CreateQuad (points[9], points[1], points[11], points[3]);
			CreateQuad (points[11], points[15], points[17], points[12]);
			break;
		case 192:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);

			CreateQuad (points[1], points[5], points[3], points[2]);
			CreateQuad (points[6], points[3], points[5], points[2]);
			break;
		case 193:
			points = new Node[] {
				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.bottomSquare.centreBackward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreRight,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [10], false);

			CreateTriangle (points [5], points [10], points [9]);

			CreateQuad (points[8], points[1], points[10], points[3]);
			CreateQuad (points[1], points[5], points[10], points[2]);
			CreateQuad (points[6], points[3], points[8], points[2]);
			CreateQuad (points[6], points[9], points[5], points[8]);
			break;
		default:
			ExtendedMeshGenerator emg = new ExtendedMeshGenerator();
			emg.ExtendedMeshGeneration(this, cube, top, bottom, left, right, forward, back);
			break;
		}
	}

	private void CreateQuad (Node center1, Node center2, Node edge1, Node edge2){
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

	void AssignVertices(Node[] points) {
		for (int i = 0; i < points.Length; i ++) {
			if (points[i].vertexIndex == -1) {
				points[i].vertexIndex = vertices.Count;
				vertices.Add(points[i].position);
			}
		}
	}

	void CreateTriangle (Node a, Node b, Node c) {
		Vector3 U = a.position - b.position;
		Vector3 V = c.position - b.position;

		Vector3 normalVU = Vector3.Cross(V, U).normalized;

		bool shouldRender = false;

		if((Mathf.Abs(normalVU.x) > 0.95f && (Mathf.Abs(normalVU.y) + Mathf.Abs(normalVU.z) < 0.2f)) || 
			(Mathf.Abs(normalVU.y) > 0.95f && (Mathf.Abs(normalVU.x) + Mathf.Abs(normalVU.z) < 0.2f)) || 
			(Mathf.Abs(normalVU.z) > 0.95f && (Mathf.Abs(normalVU.x) + Mathf.Abs(normalVU.y) < 0.2f))){

			if(Mathf.Abs(normalVU.x) > Mathf.Abs(normalVU.y) + Mathf.Abs(normalVU.z)){
				if(normalVU.x > 0){
					shouldRender = right.IsEmpty();
				}else{
					shouldRender = left.IsEmpty();
				}
			}else if(Mathf.Abs(normalVU.y) > Mathf.Abs(normalVU.x) + Mathf.Abs(normalVU.z)){
				if(normalVU.y > 0){
					shouldRender = top.IsEmpty();
				}else{
					shouldRender = bottom.IsEmpty();
				}
			}else{
				if(normalVU.z > 0){
					shouldRender = forward.IsEmpty();
				}else{
					shouldRender = back.IsEmpty();
				}
			}

			if(shouldRender){
				triangles.Add(a.vertexIndex);
				triangles.Add(b.vertexIndex);
				triangles.Add(c.vertexIndex);
			}
		}else{
			triangles.Add(a.vertexIndex);
			triangles.Add(b.vertexIndex);
			triangles.Add(c.vertexIndex);
		}
	}

	public void CreateMesh(Cube cube, Cube top, Cube bottom, Cube left, Cube right, Cube forward, Cube back){
		this.top = top;
		this.bottom = bottom;
		this.left = left;
		this.right = right;
		this.forward = forward;
		this.back = back;

		CreateMeshUsingSwitchCase(cube);
	}
}