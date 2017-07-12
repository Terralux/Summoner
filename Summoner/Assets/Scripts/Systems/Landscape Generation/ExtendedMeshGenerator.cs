using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedMeshGenerator {

	private MeshGenerator mg;

	public void ExtendedMeshGeneration(MeshGenerator newMG, MeshGenerator.Cube cube){
		mg = newMG;
		MeshGenerator.Node[] points;
		switch (cube.configuration) {
		case 198:
			points = new MeshGenerator.Node[] {
				cube.topSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [1], points [2], points [3], points [0], false);
			CreateCornerMesh (points [4], points [5], points [6], points [3], false);

			CreateCornerMesh (points [10], points [5], points [9], points [11], false);
			CreateCornerMesh (points [12], points [7], points [11], points [8], false);

			CreateTriangle (points [11], points [9], points [8]);
			CreateTriangle (points [0], points [3], points [6]);

			CreateTriangle (points [0], points [6], points [7]);
			CreateTriangle (points [8], points [9], points [2]);

			CreateQuad (points [0], points [8], points [7], points [2]);

			CreateQuad (points [2], points [5], points [9], points [3]);
			CreateQuad (points [5], points [7], points [11], points [6]);
			break;
		case 199:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);

			CreateCornerMesh (points [13], points [8], points [12], points [14], false);
			CreateCornerMesh (points [15], points [10], points [14], points [11], false);

			CreateTriangle (points [3], points [9], points [10]);

			CreateTriangle (points [5], points [2], points [1]);
			CreateTriangle (points [14], points [12], points [11]);

			CreateQuad (points [12], points [1], points [5], points [11]);

			CreateQuad (points [2], points [9], points [6], points [3]);

			CreateQuad (points [5], points [8], points [12], points [6]);
			CreateQuad (points [8], points [10], points [14], points [9]);
			CreateQuad (points [10], points [1], points [11], points [3]);
			break;
		case 200:
			points = new MeshGenerator.Node[] {
				cube.topSquare.centreLeft,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [3], points [4], points [0], points [2], false);

			CreateCornerMesh (points [7], points [1], points [6], points [8], false);
			CreateCornerMesh (points [9], points [4], points [8], points [5], false);

			CreateTriangle (points [0], points [4], points [5]);
			CreateTriangle (points [5], points [8], points [6]);

			CreateTriangle (points [0], points [5], points [6]);

			CreateQuad (points [0], points [1], points [6], points [2]);

			CreateQuad (points [1], points [4], points [8], points [2]);
			break;
		case 201:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [6], points [7], points [3], points [5], false);

			CreateCornerMesh (points [10], points [4], points [9], points [11], false);
			CreateCornerMesh (points [12], points [7], points [11], points [8], false);

			CreateTriangle (points [3], points [2], points [5]);
			CreateTriangle (points [9], points [8], points [11]);

			CreateTriangle (points [4], points [5], points [2]);
			CreateTriangle (points [1], points [8], points [9]);

			CreateQuad (points [2], points [9], points [1], points [4]);

			CreateQuad (points [4], points [7], points [11], points [5]);
			CreateQuad (points [7], points [1], points [8], points [3]);
			break;
		case 202:
			points = new MeshGenerator.Node[] {
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [2], points [3], points [4], points [0], false);
			CreateCornerMesh (points [7], points [8], points [1], points [6], false);

			CreateCornerMesh (points [11], points [5], points [10], points [12], false);
			CreateCornerMesh (points [13], points [8], points [12], points [9], false);

			CreateTriangle (points [4], points [5], points [6]);

			CreateTriangle (points [1], points [8], points [9]);
			CreateTriangle (points [9], points [12], points [10]);

			CreateTriangle (points [9], points [10], points [3]);
			CreateTriangle (points [9], points [3], points [0]);
			CreateTriangle (points [9], points [0], points [1]);

			CreateQuad (points [0], points [6], points [4], points [1]);

			CreateQuad (points [3], points [5], points [10], points [4]);
			CreateQuad (points [5], points [8], points [12], points [6]);
			break;
		case 203:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [9], points [10], points [3], points [8], false);

			CreateCornerMesh (points [13], points [7], points [12], points [14], false);
			CreateCornerMesh (points [15], points [10], points [14], points [11], false);

			CreateTriangle (points [6], points [7], points [8]);

			CreateTriangle (points [5], points [2], points [1]);
			CreateTriangle (points [14], points [12], points [11]);

			CreateQuad (points [12], points [1], points [5], points [11]);

			CreateQuad (points [2], points [8], points [6], points [3]);

			CreateQuad (points [5], points [7], points [12], points [6]);
			CreateQuad (points [7], points [10], points [14], points [8]);
			CreateQuad (points [10], points [1], points [11], points [3]);
			break;
		case 204:
			points = new MeshGenerator.Node[] {
				cube.topSquare.centreLeft,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [4], points [5], points [6], points [3], false);
			CreateCornerMesh (points [7], points [8], points [0], points [6], false);

			CreateCornerMesh (points [11], points [5], points [10], points [12], false);
			CreateCornerMesh (points [13], points [8], points [12], points [9], false);

			CreateTriangle (points [8], points [9], points [0]);
			CreateTriangle (points [10], points [5], points [3]);
			CreateTriangle (points [10], points [9], points [12]);
			CreateTriangle (points [0], points [3], points [6]);

			CreateQuad (points [3], points [9], points [0], points [10]);

			CreateQuad (points [5], points [8], points [12], points [6]);
			break;
		case 205:
			points = new MeshGenerator.Node[] {
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [2], points [3], points [0], points [1], false);
			CreateCornerMesh (points [5], points [6], points [7], points [4], false);
			CreateCornerMesh (points [8], points [9], points [1], points [7], false);

			CreateCornerMesh (points [12], points [6], points [11], points [13], false);
			CreateCornerMesh (points [14], points [9], points [13], points [10], false);

			CreateTriangle (points [11], points [6], points [4]);
			CreateTriangle (points [11], points [10], points [13]);

			CreateTriangle (points [11], points [4], points [0]);
			CreateTriangle (points [11], points [0], points [3]);
			CreateTriangle (points [11], points [3], points [10]);

			CreateQuad (points [0], points [7], points [4], points [1]);

			CreateQuad (points [6], points [9], points [13], points [7]);
			CreateQuad (points [9], points [3], points [10], points [1]);
			break;
		case 206:
			points = new MeshGenerator.Node[] {
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [2], points [3], points [4], points [0], false);
			CreateCornerMesh (points [5], points [6], points [7], points [4], false);
			CreateCornerMesh (points [8], points [9], points [1], points [7], false);

			CreateCornerMesh (points [12], points [6], points [11], points [13], false);
			CreateCornerMesh (points [14], points [9], points [13], points [10], false);

			CreateTriangle (points [1], points [9], points [10]);
			CreateTriangle (points [10], points [13], points [11]);

			CreateTriangle (points [10], points [11], points [3]);
			CreateTriangle (points [10], points [3], points [0]);
			CreateTriangle (points [10], points [0], points [1]);

			CreateQuad (points [0], points [7], points [4], points [1]);

			CreateQuad (points [3], points [6], points [11], points [4]);
			CreateQuad (points [6], points [9], points [13], points [7]);
			break;
		case 207:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [3], points [9], false);

			CreateCornerMesh (points [14], points [8], points [13], points [15], false);
			CreateCornerMesh (points [16], points [11], points [15], points [12], false);

			CreateTriangle (points [5], points [2], points [1]);
			CreateTriangle (points [15], points [13], points [12]);

			CreateQuad (points [13], points [1], points [5], points [12]);

			CreateQuad (points [2], points [9], points [6], points [3]);

			CreateQuad (points [5], points [8], points [13], points [6]);
			CreateQuad (points [8], points [11], points [15], points [9]);
			CreateQuad (points [11], points [1], points [12], points [3]);
			break;
		case 208:
			points = new MeshGenerator.Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [3], points [2], false);
			CreateCornerMesh (points [5], points [6], points [4], points [7], false);
			CreateCornerMesh (points [8], points [9], points [7], points [3], false);

			CreateTriangle (points [1], points [6], points [9]);

			CreateTriangle (points [1], points [9], points [3]);
			CreateTriangle (points [9], points [6], points [7]);

			CreateQuad (points [2], points [7], points [3], points [4]);

			CreateQuad (points [2], points [6], points [4], points [1]);
			break;
		case 209:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleBackwardRight,
				cube.middleBackwardLeft,
				cube.middleForwardRight,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);

			CreateCornerMesh (points [7], points [1], points [9], points [8], false);
			CreateCornerMesh (points [12], points [4], points [11], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [9], false);

			CreateTriangle (points [2], points [1], points [8]);
			CreateTriangle (points [5], points [4], points [13]);

			CreateTriangle (points [2], points [8], points [11]);
			CreateTriangle (points [4], points [5], points [3]);

			CreateQuad (points [4], points [2], points [3], points [11]);

			CreateQuad (points [13], points [8], points [11], points [9]);

			CreateQuad (points [5], points [1], points [9], points [3]);
			break;
		case 210:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.middleBackwardRight,
				cube.middleBackwardLeft,
				cube.middleForwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);

			CreateCornerMesh (points [7], points [6], points [9], points [8], false);
			CreateCornerMesh (points [11], points [4], points [10], points [12], false);
			CreateCornerMesh (points [13], points [5], points [12], points [9], false);

			CreateTriangle (points [1], points [8], points [10]);

			CreateTriangle (points [5], points [4], points [12]);
			CreateTriangle (points [6], points [5], points [9]);

			CreateTriangle (points [5], points [6], points [3]);
			CreateTriangle (points [5], points [3], points [2]);
			CreateTriangle (points [5], points [2], points [4]);

			CreateQuad (points [12], points [8], points [10], points [9]);

			CreateQuad (points [1], points [6], points [3], points [8]);
			CreateQuad (points [4], points [1], points [2], points [10]);
			break;
		case 211:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);

			CreateCornerMesh (points [9], points [1], points [11], points [10], false);
			CreateCornerMesh (points [13], points [7], points [12], points [14], false);
			CreateCornerMesh (points [15], points [8], points [14], points [11], false);

			CreateTriangle (points [5], points [10], points [12]);

			CreateTriangle (points [3], points [2], points [6]);
			CreateTriangle (points [8], points [7], points [14]);

			CreateQuad (points [3], points [7], points [6], points [8]);

			CreateQuad (points [10], points [14], points [11], points [12]);

			CreateQuad (points [1], points [5], points [10], points [2]);
			CreateQuad (points [5], points [7], points [12], points [6]);
			CreateQuad (points [8], points [1], points [11], points [3]);
			break;
		case 212:
			points = new MeshGenerator.Node[] {
				cube.middleForwardLeft,

				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [2], points [3], points [4], points [1], false);

			CreateCornerMesh (points [6], points [0], points [8], points [7], false);
			CreateCornerMesh (points [10], points [3], points [9], points [11], false);
			CreateCornerMesh (points [12], points [5], points [11], points [8], false);

			CreateTriangle (points [0], points [5], points [8]);
			CreateTriangle (points [3], points [1], points [9]);

			CreateTriangle (points [5], points [0], points [4]);
			CreateTriangle (points [1], points [7], points [9]);

			CreateQuad (points [1], points [0], points [4], points [7]);

			CreateQuad (points [7], points [11], points [8], points [9]);

			CreateQuad (points [3], points [5], points [11], points [4]);
			break;
		case 213:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [5], points [6], points [7], points [4], false);

			CreateCornerMesh (points [9], points [1], points [11], points [10], false);
			CreateCornerMesh (points [13], points [6], points [12], points [14], false);
			CreateCornerMesh (points [15], points [8], points [14], points [11], false);

			CreateTriangle (points [1], points [10], points [2]);
			CreateTriangle (points [6], points [4], points [12]);

			CreateTriangle (points [8], points [3], points [7]);

			CreateQuad (points [10], points [4], points [12], points [2]);

			CreateQuad (points [2], points [7], points [4], points [3]);
			CreateQuad (points [10], points [14], points [11], points [12]);

			CreateQuad (points [6], points [8], points [14], points [7]);
			CreateQuad (points [8], points [1], points [11], points [3]);
			break;
		case 214:
			points = new MeshGenerator.Node[] {
				cube.middleForwardLeft,
				cube.topSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [2], points [3], points [4], points [1], false);
			CreateCornerMesh (points [5], points [6], points [7], points [4], false);

			CreateCornerMesh (points [9], points [0], points [11], points [10], false);
			CreateCornerMesh (points [13], points [6], points [12], points [14], false);
			CreateCornerMesh (points [15], points [8], points [14], points [11], false);

			CreateTriangle (points [1], points [4], points [7]);
			CreateTriangle (points [11], points [0], points [8]);

			CreateTriangle (points [3], points [10], points [12]);

			CreateQuad (points [10], points [14], points [11], points [12]);

			CreateQuad (points [0], points [3], points [10], points [1]);
			CreateQuad (points [3], points [6], points [12], points [4]);
			CreateQuad (points [6], points [8], points [14], points [7]);

			CreateQuad (points [1], points [8], points [7], points [0]);
			break;
		case 215:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);

			CreateCornerMesh (points [11], points [1], points [13], points [12], false);
			CreateCornerMesh (points [15], points [8], points [14], points [16], false);
			CreateCornerMesh (points [17], points [10], points [16], points [13], false);

			CreateTriangle (points [5], points [12], points [14]);
			CreateTriangle (points [10], points [3], points [9]);

			CreateQuad (points [2], points [9], points [6], points [3]);
			CreateQuad (points [12], points [16], points [13], points [14]);

			CreateQuad (points [1], points [5], points [12], points [2]);
			CreateQuad (points [5], points [8], points [14], points [6]);
			CreateQuad (points [8], points [10], points [16], points [9]);
			CreateQuad (points [10], points [1], points [13], points [3]);
			break;
		case 216:
			points = new MeshGenerator.Node[] {
				cube.middleForwardLeft,
				cube.topSquare.centreLeft,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [4], points [5], points [1], points [3], false);

			CreateCornerMesh (points [6], points [0], points [8], points [7], false);
			CreateCornerMesh (points [10], points [2], points [9], points [11], false);
			CreateCornerMesh (points [12], points [5], points [11], points [8], false);

			CreateQuad (points [7], points [11], points [8], points [9]);

			CreateQuad (points [2], points [5], points [11], points [3]);
			CreateQuad (points [5], points [0], points [8], points [1]);

			CreateQuad (points [0], points [9], points [7], points [2]);
			CreateQuad (points [0], points [3], points [2], points [1]);
			break;
		case 217:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [6], points [7], points [3], points [5], false);

			CreateCornerMesh (points [8], points [1], points [10], points [9], false);
			CreateCornerMesh (points [12], points [4], points [11], points [13], false);
			CreateCornerMesh (points [14], points [7], points [13], points [10], false);

			CreateTriangle (points [3], points [2], points [5]);
			CreateTriangle (points [2], points [1], points [9]);

			CreateTriangle (points [2], points [9], points [11]);
			CreateTriangle (points [2], points [11], points [4]);
			CreateTriangle (points [2], points [4], points [5]);

			CreateQuad (points [9], points [13], points [10], points [11]);

			CreateQuad (points [4], points [7], points [13], points [5]);
			CreateQuad (points [7], points [1], points [10], points [3]);
			break;
		case 218:
			points = new MeshGenerator.Node[] {
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [3], points [4], points [5], points [1], false);
			CreateCornerMesh (points [8], points [9], points [2], points [7], false);

			CreateCornerMesh (points [10], points [0], points [12], points [11], false);
			CreateCornerMesh (points [14], points [6], points [13], points [15], false);
			CreateCornerMesh (points [16], points [9], points [15], points [12], false);

			CreateTriangle (points [4], points [11], points [13]);
			CreateTriangle (points [6], points [7], points [5]);
			CreateTriangle (points [0], points [1], points [2]);

			CreateQuad (points [1], points [7], points [5], points [2]);
			CreateQuad (points [11], points [15], points [12], points [13]);

			CreateQuad (points [0], points [4], points [11], points [1]);
			CreateQuad (points [4], points [6], points [13], points [5]);
			CreateQuad (points [6], points [9], points [15], points [7]);
			CreateQuad (points [9], points [0], points [12], points [2]);
			break;
		case 219:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [9], points [10], points [3], points [8], false);

			CreateCornerMesh (points [11], points [1], points [13], points [12], false);
			CreateCornerMesh (points [15], points [7], points [14], points [16], false);
			CreateCornerMesh (points [17], points [10], points [16], points [13], false);

			CreateTriangle (points [5], points [12], points [14]);
			CreateTriangle (points [7], points [8], points [6]);

			CreateQuad (points [2], points [8], points [6], points [3]);
			CreateQuad (points [12], points [16], points [13], points [14]);

			CreateQuad (points [1], points [5], points [12], points [2]);
			CreateQuad (points [5], points [7], points [14], points [6]);
			CreateQuad (points [7], points [10], points [16], points [8]);
			CreateQuad (points [10], points [1], points [13], points [3]);
			break;
		case 220:
			points = new MeshGenerator.Node[] {
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [4], points [5], points [6], points [3], false);
			CreateCornerMesh (points [7], points [8], points [2], points [6], false);

			CreateCornerMesh (points [9], points [0], points [11], points [10], false);
			CreateCornerMesh (points [13], points [5], points [12], points [14], false);
			CreateCornerMesh (points [15], points [8], points [14], points [11], false);

			CreateTriangle (points [5], points [3], points [12]);
			CreateTriangle (points [2], points [3], points [6]);

			CreateTriangle (points [3], points [2], points [0]);
			CreateTriangle (points [3], points [0], points [10]);
			CreateTriangle (points [3], points [10], points [12]);

			CreateQuad (points [10], points [14], points [11], points [12]);

			CreateQuad (points [5], points [8], points [14], points [6]);
			CreateQuad (points [8], points [0], points [11], points [2]);
			break;
		case 221:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [5], points [6], points [7], points [4], false);
			CreateCornerMesh (points [8], points [9], points [3], points [7], false);

			CreateCornerMesh (points [10], points [1], points [12], points [11], false);
			CreateCornerMesh (points [14], points [6], points [13], points [15], false);
			CreateCornerMesh (points [16], points [9], points [15], points [12], false);

			CreateTriangle (points [1], points [11], points [2]);
			CreateTriangle (points [6], points [4], points [13]);

			CreateQuad (points [11], points [4], points [13], points [2]);

			CreateQuad (points [2], points [7], points [4], points [3]);
			CreateQuad (points [11], points [15], points [12], points [13]);

			CreateQuad (points [6], points [9], points [15], points [7]);
			CreateQuad (points [9], points [1], points [12], points [3]);
			break;
		case 222:
			points = new MeshGenerator.Node[] {
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [3], points [4], points [5], points [1], false);
			CreateCornerMesh (points [6], points [7], points [8], points [5], false);
			CreateCornerMesh (points [9], points [10], points [2], points [8], false);

			CreateCornerMesh (points [11], points [0], points [13], points [12], false);
			CreateCornerMesh (points [15], points [7], points [14], points [16], false);
			CreateCornerMesh (points [17], points [10], points [16], points [13], false);

			CreateTriangle (points [4], points [12], points [14]);
			CreateTriangle (points [0], points [1], points [2]);

			CreateQuad (points [1], points [8], points [5], points [2]);
			CreateQuad (points [12], points [16], points [13], points [14]);

			CreateQuad (points [0], points [4], points [12], points [1]);
			CreateQuad (points [4], points [7], points [14], points [5]);
			CreateQuad (points [7], points [10], points [16], points [8]);
			CreateQuad (points [10], points [0], points [13], points [2]);
			break;
		case 223:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [3], points [9], false);

			CreateCornerMesh (points [12], points [1], points [14], points [13], false);
			CreateCornerMesh (points [16], points [8], points [15], points [17], false);
			CreateCornerMesh (points [18], points [11], points [17], points [14], false);

			CreateTriangle (points [5], points [13], points [15]);

			CreateQuad (points [2], points [9], points [6], points [3]);
			CreateQuad (points [13], points [17], points [14], points [15]);

			CreateQuad (points [1], points [5], points [13], points [2]);
			CreateQuad (points [5], points [8], points [15], points [6]);
			CreateQuad (points [8], points [11], points [17], points [9]);
			CreateQuad (points [11], points [1], points [14], points [3]);
			break;
		case 224:
			points = new MeshGenerator.Node[] {
				cube.middleForwardRight,
				cube.middleBackwardRight,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);

			CreateCornerMesh (points [5], points [0], points [3], points [6], false);
			CreateCornerMesh (points [7], points [1], points [6], points [8], false);
			CreateCornerMesh (points [9], points [2], points [8], points [4], false);

			CreateTriangle (points [0], points [6], points [1]);
			CreateTriangle (points [1], points [8], points [2]);

			CreateTriangle (points [0], points [1], points [2]);

			CreateQuad (points [0], points [4], points [2], points [3]);

			CreateQuad (points [3], points [8], points [4], points [6]);
			break;
		case 225:
			points = new MeshGenerator.Node[] {
				cube.topSquare.centreForward,

				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreLeft,

				cube.middleForwardRight,
				cube.middleBackwardRight,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [1], points [2], points [0], points [3], false);

			CreateCornerMesh (points [9], points [4], points [7], points [10], false);
			CreateCornerMesh (points [11], points [5], points [10], points [12], false);
			CreateCornerMesh (points [13], points [6], points [12], points [8], false);

			CreateTriangle (points [2], points [8], points [7]);

			CreateTriangle (points [4], points [10], points [5]);
			CreateTriangle (points [5], points [12], points [6]);

			CreateTriangle (points [5], points [6], points [3]);
			CreateTriangle (points [5], points [3], points [0]);
			CreateTriangle (points [5], points [0], points [4]);

			CreateQuad (points [2], points [6], points [3], points [8]);
			CreateQuad (points [4], points [2], points [0], points [7]);

			CreateQuad (points [7], points [12], points [8], points [10]);

			break;
		case 226:
			points = new MeshGenerator.Node[] {
				cube.topSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [1], points [2], points [3], points [0], false);

			CreateCornerMesh (points [8], points [2], points [6], points [9], false);
			CreateCornerMesh (points [10], points [4], points [9], points [11], false);
			CreateCornerMesh (points [12], points [5], points [11], points [7], false);

			CreateTriangle (points [5], points [4], points [11]);
			CreateTriangle (points [0], points [6], points [2]);

			CreateTriangle (points [6], points [0], points [7]);
			CreateTriangle (points [7], points [0], points [5]);
			CreateTriangle (points [5], points [0], points [3]);
			CreateTriangle (points [5], points [3], points [4]);

			CreateQuad (points [6], points [11], points [7], points [9]);

			CreateQuad (points [2], points [4], points [9], points [3]);
			break;
		case 227:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);

			CreateCornerMesh (points [11], points [5], points [9], points [12], false);
			CreateCornerMesh (points [13], points [7], points [12], points [14], false);
			CreateCornerMesh (points [15], points [8], points [14], points [10], false);

			CreateTriangle (points [1], points [10], points [9]);

			CreateTriangle (points [8], points [7], points [14]);
			CreateTriangle (points [3], points [2], points [6]);

			CreateQuad (points [3], points [7], points [6], points [8]);

			CreateQuad (points [9], points [14], points [10], points [12]);

			CreateQuad (points [1], points [5], points [9], points [2]);
			CreateQuad (points [5], points [7], points [12], points [6]);
			CreateQuad (points [8], points [1], points [10], points [3]);
			break;
		case 228:
			points = new MeshGenerator.Node[] {
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [2], points [3], points [4], points [1], false);

			CreateCornerMesh (points [8], points [0], points [6], points [9], false);
			CreateCornerMesh (points [10], points [3], points [9], points [11], false);
			CreateCornerMesh (points [12], points [5], points [11], points [7], false);

			CreateQuad (points [6], points [5], points [0], points [7]);
			CreateQuad (points [5], points [1], points [0], points [4]);

			CreateQuad (points [6], points [11], points [7], points [9]);

			CreateQuad (points [0], points [3], points [9], points [1]);
			CreateQuad (points [3], points [5], points [11], points [4]);
			break;
		case 229:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [6], points [7], points [8], points [5], false);

			CreateCornerMesh (points [12], points [4], points [10], points [13], false);
			CreateCornerMesh (points [14], points [7], points [13], points [15], false);
			CreateCornerMesh (points [16], points [9], points [15], points [11], false);

			CreateTriangle (points [4], points [5], points [2]);
			CreateTriangle (points [9], points [3], points [8]);
			CreateTriangle (points [1], points [11], points [10]);

			CreateQuad (points [2], points [8], points [5], points [3]);
			CreateQuad (points [10], points [15], points [11], points [13]);

			CreateQuad (points [1], points [4], points [10], points [2]);
			CreateQuad (points [4], points [7], points [13], points [5]);
			CreateQuad (points [7], points [9], points [15], points [8]);
			CreateQuad (points [9], points [1], points [11], points [3]);
			break;
		case 230:
			points = new MeshGenerator.Node[] {
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [2], points [3], points [4], points [0], false);
			CreateCornerMesh (points [5], points [6], points [7], points [4], false);

			CreateCornerMesh (points [11], points [3], points [9], points [12], false);
			CreateCornerMesh (points [13], points [6], points [12], points [14], false);
			CreateCornerMesh (points [15], points [8], points [14], points [10], false);

			CreateTriangle (points [3], points [0], points [9]);
			CreateTriangle (points [4], points [7], points [0]);

			CreateTriangle (points [0], points [7], points [8]);
			CreateTriangle (points [0], points [8], points [10]);
			CreateTriangle (points [0], points [10], points [9]);

			CreateQuad (points [9], points [14], points [10], points [12]);

			CreateQuad (points [3], points [6], points [12], points [4]);
			CreateQuad (points [6], points [8], points [14], points [7]);
			break;
		case 231:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);

			CreateCornerMesh (points [13], points [5], points [11], points [14], false);
			CreateCornerMesh (points [15], points [8], points [14], points [16], false);
			CreateCornerMesh (points [17], points [10], points [16], points [12], false);

			CreateTriangle (points [10], points [3], points [9]);
			CreateTriangle (points [1], points [12], points [11]);

			CreateQuad (points [2], points [9], points [6], points [3]);
			CreateQuad (points [11], points [16], points [12], points [14]);

			CreateQuad (points [1], points [5], points [11], points [2]);
			CreateQuad (points [5], points [8], points [14], points [6]);
			CreateQuad (points [8], points [10], points [16], points [9]);
			CreateQuad (points [10], points [1], points [12], points [3]);
			break;
		case 232:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleForwardRight,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [7], points [8], points [3], points [6], false);

			CreateCornerMesh (points [11], points [4], points [9], points [12], false);
			CreateCornerMesh (points [13], points [5], points [12], points [14], false);
			CreateCornerMesh (points [15], points [8], points [14], points [10], false);

			CreateTriangle (points [8], points [10], points [3]);
			CreateTriangle (points [12], points [5], points [4]);

			CreateTriangle (points [10], points [9], points [3]);
			CreateTriangle (points [3], points [9], points [4]);

			CreateQuad (points [4], points [6], points [5], points [3]);

			CreateQuad (points [5], points [8], points [14], points [6]);

			CreateQuad (points [9], points [14], points [10], points [12]);
			break;
		case 233:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleForwardRight,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [7], points [8], points [3], points [6], false);

			CreateCornerMesh (points [11], points [4], points [9], points [12], false);
			CreateCornerMesh (points [13], points [5], points [12], points [14], false);
			CreateCornerMesh (points [15], points [8], points [14], points [10], false);

			CreateTriangle (points [1], points [10], points [9]);

			CreateTriangle (points [12], points [5], points [4]);
			CreateTriangle (points [3], points [2], points [6]);

			CreateQuad (points [4], points [6], points [5], points [2]);

			CreateQuad (points [5], points [8], points [14], points [6]);
			CreateQuad (points [8], points [1], points [10], points [3]);
			CreateQuad (points [1], points [4], points [9], points [2]);

			CreateQuad (points [9], points [14], points [10], points [12]);
			break;
		case 234:
			points = new MeshGenerator.Node[] {
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [2], points [3], points [4], points [0], false);
			CreateCornerMesh (points [7], points [8], points [1], points [6], false);

			CreateCornerMesh (points [11], points [3], points [9], points [12], false);
			CreateCornerMesh (points [13], points [5], points [12], points [14], false);
			CreateCornerMesh (points [15], points [8], points [14], points [10], false);

			CreateTriangle (points [5], points [6], points [4]);

			CreateTriangle (points [3], points [0], points [9]);
			CreateTriangle (points [8], points [10], points [1]);

			CreateQuad (points [0], points [10], points [1], points [9]);

			CreateQuad (points [0], points [6], points [4], points [1]);
			CreateQuad (points [9], points [14], points [10], points [12]);

			CreateQuad (points [3], points [5], points [12], points [4]);
			CreateQuad (points [5], points [8], points [14], points [6]);
			break;
			
		case 235:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [9], points [10], points [3], points [8], false);

			CreateCornerMesh (points [13], points [5], points [11], points [14], false);
			CreateCornerMesh (points [15], points [7], points [14], points [16], false);
			CreateCornerMesh (points [17], points [10], points [16], points [12], false);

			CreateTriangle (points [7], points [8], points [6]);
			CreateTriangle (points [1], points [12], points [11]);

			CreateQuad (points [2], points [8], points [6], points [3]);
			CreateQuad (points [11], points [16], points [12], points [14]);

			CreateQuad (points [1], points [5], points [11], points [2]);
			CreateQuad (points [5], points [7], points [14], points [6]);
			CreateQuad (points [7], points [10], points [16], points [8]);
			CreateQuad (points [10], points [1], points [12], points [3]);
			break;
		case 236:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [6], points [7], points [8], points [5], false);
			CreateCornerMesh (points [9], points [10], points [3], points [8], false);

			CreateCornerMesh (points [13], points [4], points [11], points [14], false);
			CreateCornerMesh (points [15], points [7], points [14], points [16], false);
			CreateCornerMesh (points [17], points [10], points [16], points [12], false);

			CreateTriangle (points [8], points [3], points [5]);
			CreateTriangle (points [3], points [10], points [12]);

			CreateTriangle (points [3], points [12], points [11]);
			CreateTriangle (points [3], points [11], points [4]);
			CreateTriangle (points [3], points [4], points [5]);

			CreateQuad (points [11], points [16], points [12], points [14]);

			CreateQuad (points [4], points [7], points [14], points [5]);
			CreateQuad (points [7], points [10], points [16], points [8]);
			break;
		case 237:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [6], points [7], points [8], points [5], false);
			CreateCornerMesh (points [9], points [10], points [3], points [8], false);

			CreateCornerMesh (points [13], points [4], points [11], points [14], false);
			CreateCornerMesh (points [15], points [7], points [14], points [16], false);
			CreateCornerMesh (points [17], points [10], points [16], points [12], false);

			CreateTriangle (points [1], points [12], points [11]);
			CreateTriangle (points [4], points [5], points [2]);

			CreateQuad (points [2], points [8], points [5], points [3]);
			CreateQuad (points [11], points [16], points [12], points [14]);

			CreateQuad (points [1], points [4], points [11], points [2]);
			CreateQuad (points [4], points [7], points [14], points [5]);
			CreateQuad (points [7], points [10], points [16], points [8]);
			CreateQuad (points [10], points [1], points [12], points [3]);
			break;
		case 238:
			points = new MeshGenerator.Node[] {
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [2], points [3], points [4], points [0], false);
			CreateCornerMesh (points [5], points [6], points [7], points [4], false);
			CreateCornerMesh (points [8], points [9], points [1], points [7], false);

			CreateCornerMesh (points [12], points [3], points [10], points [13], false);
			CreateCornerMesh (points [14], points [6], points [13], points [15], false);
			CreateCornerMesh (points [16], points [9], points [15], points [11], false);

			CreateTriangle (points [3], points [0], points [10]);
			CreateTriangle (points [9], points [11], points [1]);

			CreateQuad (points [0], points [11], points [1], points [10]);

			CreateQuad (points [0], points [7], points [4], points [1]);
			CreateQuad (points [10], points [15], points [11], points [13]);

			CreateQuad (points [3], points [6], points [13], points [4]);
			CreateQuad (points [6], points [9], points [15], points [7]);
			break;
		case 239:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [3], points [9], false);

			CreateCornerMesh (points [14], points [5], points [12], points [15], false);
			CreateCornerMesh (points [16], points [8], points [15], points [17], false);
			CreateCornerMesh (points [18], points [11], points [17], points [13], false);

			CreateTriangle (points [1], points [13], points [12]);

			CreateQuad (points [2], points [9], points [6], points [3]);
			CreateQuad (points [12], points [17], points [13], points [15]);

			CreateQuad (points [1], points [5], points [12], points [2]);
			CreateQuad (points [5], points [8], points [15], points [6]);
			CreateQuad (points [8], points [11], points [17], points [9]);
			CreateQuad (points [11], points [1], points [13], points [3]);
			break;
		case 240:
			points = new MeshGenerator.Node[] {
				cube.bottomSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.middleForwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.middleBackwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft,
				cube.middleBackwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [3], points [2], false);
			CreateCornerMesh (points [4], points [5], points [2], points [6], false);
			CreateCornerMesh (points [7], points [8], points [6], points [9], false);
			CreateCornerMesh (points [10], points [11], points [9], points [3], false);

			CreateTriangle (points [1], points [11], points [3]);
			CreateTriangle (points [5], points [1], points [2]);
			CreateTriangle (points [8], points [5], points [6]);
			CreateTriangle (points [11], points [8], points [9]);

			CreateQuad (points [1], points [8], points [5], points [11]);
			CreateQuad (points [2], points [9], points [3], points [6]);
			break;
		case 241:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleBackwardRight,
				cube.middleBackwardLeft,
				cube.middleForwardRight,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);

			CreateCornerMesh (points [7], points [1], points [9], points [8], false);
			CreateCornerMesh (points [10], points [6], points [8], points [11], false);
			CreateCornerMesh (points [12], points [4], points [11], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [9], false);

			CreateTriangle (points [5], points [4], points [13]);
			CreateTriangle (points [4], points [6], points [11]);

			CreateTriangle (points [4], points [5], points [3]);
			CreateTriangle (points [4], points [3], points [2]);
			CreateTriangle (points [4], points [2], points [6]);

			CreateQuad (points [13], points [8], points [11], points [9]);

			CreateQuad (points [1], points [6], points [8], points [2]);
			CreateQuad (points [5], points [1], points [9], points [3]);
			break;
		case 242:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,
				cube.topSquare.centreForward,

				cube.middleBackwardRight,
				cube.middleBackwardLeft,
				cube.middleForwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);

			CreateCornerMesh (points [7], points [6], points [9], points [8], false);
			CreateCornerMesh (points [10], points [1], points [8], points [11], false);
			CreateCornerMesh (points [12], points [4], points [11], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [9], false);

			CreateTriangle (points [5], points [4], points [13]);
			CreateTriangle (points [6], points [5], points [9]);

			CreateTriangle (points [5], points [6], points [3]);
			CreateTriangle (points [5], points [3], points [2]);
			CreateTriangle (points [5], points [2], points [4]);

			CreateQuad (points [13], points [8], points [11], points [9]);

			CreateQuad (points [1], points [6], points [3], points [8]);
			CreateQuad (points [4], points [1], points [2], points [11]);
			break;
		case 243:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);

			CreateCornerMesh (points [9], points [1], points [11], points [10], false);
			CreateCornerMesh (points [12], points [5], points [10], points [13], false);
			CreateCornerMesh (points [14], points [7], points [13], points [15], false);
			CreateCornerMesh (points [16], points [8], points [15], points [11], false);

			CreateTriangle (points [2], points [6], points [3]);
			CreateTriangle (points [15], points [8], points [7]);

			CreateQuad (points [3], points [7], points [6], points [8]);
			CreateQuad (points [10], points [15], points [11], points [13]);

			CreateQuad (points [1], points [5], points [10], points [2]);
			CreateQuad (points [5], points [7], points [13], points [6]);
			CreateQuad (points [8], points [1], points [11], points [3]);
			break;
		case 244:
			points = new MeshGenerator.Node[] {
				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.middleForwardLeft,
				cube.middleForwardRight,

				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);

			CreateCornerMesh (points [7], points [4], points [9], points [8], false);
			CreateCornerMesh (points [10], points [5], points [8], points [11], false);
			CreateCornerMesh (points [12], points [1], points [11], points [13], false);
			CreateCornerMesh (points [14], points [6], points [13], points [9], false);

			CreateTriangle (points [5], points [4], points [8]);
			CreateTriangle (points [4], points [6], points [9]);

			CreateTriangle (points [4], points [5], points [3]);
			CreateTriangle (points [4], points [3], points [2]);
			CreateTriangle (points [4], points [2], points [6]);

			CreateQuad (points [1], points [6], points [13], points [2]);
			CreateQuad (points [5], points [1], points [11], points [3]);

			CreateQuad (points [13], points [8], points [11], points [9]);
			break;
		case 245:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [6], points [7], points [8], points [5], false);

			CreateCornerMesh (points [10], points [1], points [12], points [11], false);
			CreateCornerMesh (points [13], points [4], points [11], points [14], false);
			CreateCornerMesh (points [15], points [7], points [14], points [16], false);
			CreateCornerMesh (points [17], points [9], points [16], points [12], false);

			CreateTriangle (points [4], points [5], points [2]);
			CreateTriangle (points [9], points [3], points [8]);

			CreateQuad (points [2], points [8], points [5], points [3]);
			CreateQuad (points [11], points [16], points [12], points [14]);

			CreateQuad (points [1], points [4], points [11], points [2]);
			CreateQuad (points [4], points [7], points [14], points [5]);
			CreateQuad (points [7], points [9], points [16], points [8]);
			CreateQuad (points [9], points [1], points [12], points [3]);
			break;
		case 246:
			points = new MeshGenerator.Node[] {
				cube.middleForwardLeft,
				cube.topSquare.centreForward,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [2], points [3], points [4], points [1], false);
			CreateCornerMesh (points [5], points [6], points [7], points [4], false);

			CreateCornerMesh (points [9], points [0], points [11], points [10], false);
			CreateCornerMesh (points [12], points [3], points [10], points [13], false);
			CreateCornerMesh (points [14], points [6], points [13], points [15], false);
			CreateCornerMesh (points [16], points [8], points [15], points [11], false);

			CreateQuad (points [1], points [7], points [4], points [8]);
			CreateQuad (points [6], points [8], points [15], points [7]);

			CreateQuad (points [3], points [6], points [13], points [4]);
			CreateQuad (points [8], points [0], points [11], points [1]);
			CreateQuad (points [0], points [3], points [10], points [1]);

			CreateQuad (points [15], points [10], points [13], points [11]);
			break;
		case 247:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);

			CreateCornerMesh (points [11], points [1], points [13], points [12], false);
			CreateCornerMesh (points [14], points [5], points [12], points [15], false);
			CreateCornerMesh (points [16], points [8], points [15], points [17], false);
			CreateCornerMesh (points [18], points [10], points [17], points [13], false);

			CreateTriangle (points [10], points [3], points [9]);

			CreateQuad (points [2], points [9], points [6], points [3]);
			CreateQuad (points [12], points [17], points [13], points [15]);

			CreateQuad (points [1], points [5], points [12], points [2]);
			CreateQuad (points [5], points [8], points [15], points [6]);
			CreateQuad (points [8], points [10], points [17], points [9]);
			CreateQuad (points [10], points [1], points [13], points [3]);
			break;
		case 248:
			points = new MeshGenerator.Node[] {
				cube.middleForwardLeft,
				cube.topSquare.centreLeft,

				cube.middleForwardRight,
				cube.middleBackwardRight,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [4], points [5], points [1], points [6], false);

			CreateCornerMesh (points [7], points [0], points [9], points [8], false);
			CreateCornerMesh (points [10], points [2], points [8], points [11], false);
			CreateCornerMesh (points [12], points [3], points [11], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [9], false);

			CreateTriangle (points [11], points [3], points [2]);
			CreateTriangle (points [6], points [2], points [3]);
			CreateTriangle (points [6], points [1], points [2]);

			CreateQuad (points [8], points [13], points [9], points [11]);

			CreateQuad (points [0], points [2], points [8], points [1]);
			CreateQuad (points [5], points [0], points [9], points [1]);
			CreateQuad (points [3], points [5], points [13], points [6]);
			break;
		case 249:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleForwardRight,
				cube.middleBackwardRight,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreBackward,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [6], points [7], points [3], points [8], false);

			CreateCornerMesh (points [9], points [1], points [11], points [10], false);
			CreateCornerMesh (points [12], points [4], points [10], points [13], false);
			CreateCornerMesh (points [14], points [5], points [13], points [15], false);
			CreateCornerMesh (points [16], points [7], points [15], points [11], false);

			CreateTriangle (points [3], points [2], points [8]);
			CreateTriangle (points [13], points [5], points [4]);

			CreateQuad (points [10], points [15], points [11], points [13]);

			CreateQuad (points [1], points [4], points [10], points [2]);
			CreateQuad (points [7], points [1], points [11], points [3]);
			CreateQuad (points [5], points [7], points [15], points [8]);

			CreateQuad (points [5], points [2], points [8], points [4]);
			break;
		case 250:
			points = new MeshGenerator.Node[] {
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [3], points [4], points [5], points [1], false);
			CreateCornerMesh (points [8], points [9], points [2], points [7], false);

			CreateCornerMesh (points [10], points [0], points [12], points [11], false);
			CreateCornerMesh (points [13], points [4], points [11], points [14], false);
			CreateCornerMesh (points [15], points [6], points [14], points [16], false);
			CreateCornerMesh (points [17], points [9], points [16], points [12], false);

			CreateTriangle (points [6], points [7], points [5]);
			CreateTriangle (points [0], points [1], points [2]);

			CreateQuad (points [1], points [7], points [5], points [2]);
			CreateQuad (points [11], points [16], points [12], points [14]);

			CreateQuad (points [0], points [4], points [11], points [1]);
			CreateQuad (points [4], points [6], points [14], points [5]);
			CreateQuad (points [6], points [9], points [16], points [7]);
			CreateQuad (points [9], points [0], points [12], points [2]);
			break;
		case 251:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [9], points [10], points [3], points [8], false);

			CreateCornerMesh (points [11], points [1], points [13], points [12], false);
			CreateCornerMesh (points [14], points [5], points [12], points [15], false);
			CreateCornerMesh (points [16], points [7], points [15], points [17], false);
			CreateCornerMesh (points [18], points [10], points [17], points [13], false);

			CreateTriangle (points [7], points [8], points [6]);

			CreateQuad (points [2], points [8], points [6], points [3]);
			CreateQuad (points [12], points [17], points [13], points [15]);

			CreateQuad (points [1], points [5], points [12], points [2]);
			CreateQuad (points [5], points [7], points [15], points [6]);
			CreateQuad (points [7], points [10], points [17], points [8]);
			CreateQuad (points [10], points [1], points [13], points [3]);
			break;
		case 252:
			points = new MeshGenerator.Node[] {
				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,
				cube.topSquare.centreRight,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,
				cube.topSquare.centreLeft,

				cube.middleForwardLeft,
				cube.middleForwardRight,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);

			CreateCornerMesh (points [9], points [7], points [11], points [10], false);
			CreateCornerMesh (points [12], points [8], points [10], points [13], false);
			CreateCornerMesh (points [14], points [1], points [13], points [15], false);
			CreateCornerMesh (points [16], points [5], points [15], points [11], false);

			CreateTriangle (points [10], points [8], points [7]);
			CreateTriangle (points [2], points [6], points [3]);

			CreateQuad (points [8], points [1], points [13], points [3]);
			CreateQuad (points [1], points [5], points [15], points [2]);
			CreateQuad (points [5], points [7], points [11], points [6]);

			CreateQuad (points [10], points [15], points [11], points [13]);

			CreateQuad (points [8], points [6], points[3], points [7]);
			break;
		case 253:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [6], points [7], points [8], points [5], false);
			CreateCornerMesh (points [9], points [10], points [3], points [8], false);

			CreateCornerMesh (points [11], points [1], points [13], points [12], false);
			CreateCornerMesh (points [14], points [4], points [12], points [15], false);
			CreateCornerMesh (points [16], points [7], points [15], points [17], false);
			CreateCornerMesh (points [18], points [10], points [17], points [13], false);

			CreateTriangle (points [4], points [5], points [2]);

			CreateQuad (points [2], points [8], points [5], points [3]);
			CreateQuad (points [12], points [17], points [13], points [15]);

			CreateQuad (points [1], points [4], points [12], points [2]);
			CreateQuad (points [4], points [7], points [15], points [5]);
			CreateQuad (points [7], points [10], points [17], points [8]);
			CreateQuad (points [10], points [1], points [13], points [3]);
			break;
		case 254:
			points = new MeshGenerator.Node[] {
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [3], points [4], points [5], points [1], false);
			CreateCornerMesh (points [6], points [7], points [8], points [5], false);
			CreateCornerMesh (points [9], points [10], points [2], points [8], false);

			CreateCornerMesh (points [11], points [0], points [13], points [12], false);
			CreateCornerMesh (points [14], points [4], points [12], points [15], false);
			CreateCornerMesh (points [16], points [7], points [15], points [17], false);
			CreateCornerMesh (points [18], points [10], points [17], points [13], false);

			CreateTriangle (points [0], points [1], points [2]);

			CreateQuad (points [1], points [8], points [5], points [2]);
			CreateQuad (points [12], points [17], points [13], points [15]);

			CreateQuad (points [0], points [4], points [12], points [1]);
			CreateQuad (points [4], points [7], points [15], points [5]);
			CreateQuad (points [7], points [10], points [17], points [8]);
			CreateQuad (points [10], points [0], points [13], points [2]);
			break;
		case 255:
			points = new MeshGenerator.Node[] {
				cube.topSquare.forwardLeft,
				cube.middleForwardLeft,
				cube.topSquare.centreForward,
				cube.topSquare.centreLeft,

				cube.topSquare.forwardRight,
				cube.middleForwardRight,
				cube.topSquare.centreRight,

				cube.topSquare.backwardRight,
				cube.middleBackwardRight,
				cube.topSquare.centreBackward,

				cube.topSquare.backwardLeft,
				cube.middleBackwardLeft,

				cube.bottomSquare.forwardLeft,
				cube.bottomSquare.centreForward,
				cube.bottomSquare.centreLeft,

				cube.bottomSquare.forwardRight,
				cube.bottomSquare.centreRight,

				cube.bottomSquare.backwardRight,
				cube.bottomSquare.centreBackward,

				cube.bottomSquare.backwardLeft
			};
			AssignVertices (points);
			CreateCornerMesh (points [0], points [1], points [2], points [3], false);
			CreateCornerMesh (points [4], points [5], points [6], points [2], false);
			CreateCornerMesh (points [7], points [8], points [9], points [6], false);
			CreateCornerMesh (points [10], points [11], points [3], points [9], false);

			CreateCornerMesh (points [12], points [1], points [14], points [13], false);
			CreateCornerMesh (points [15], points [5], points [13], points [16], false);
			CreateCornerMesh (points [17], points [8], points [16], points [18], false);
			CreateCornerMesh (points [19], points [11], points [18], points [14], false);

			CreateQuad (points [2], points [9], points [6], points [3]);
			CreateQuad (points [13], points [18], points [14], points [16]);

			CreateQuad (points [1], points [5], points [13], points [2]);
			CreateQuad (points [5], points [8], points [16], points [6]);
			CreateQuad (points [8], points [11], points [18], points [9]);
			CreateQuad (points [11], points [1], points [14], points [3]);
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
