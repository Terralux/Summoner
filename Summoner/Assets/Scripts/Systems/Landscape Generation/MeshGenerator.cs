using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshGenerator {

	//public SquareGrid squareGrid;
	public CubeGrid cubeGrid;
	List<Vector3> vertices;
	List<int> triangles;

	public void GenerateMesh(List<int[,]> maps, float squareSize, MeshFilter mf) {
		//squareGrid = new SquareGrid(map, squareSize);
		cubeGrid = new CubeGrid(maps, squareSize);

		vertices = new List<Vector3>();
		triangles = new List<int>();

		for(int y = 0; y < cubeGrid.cubes.GetLength(0); y++){
			for (int x = 0; x < cubeGrid.cubes.GetLength(1); x ++) {
				for (int z = 0; z < cubeGrid.cubes.GetLength(2); z ++) {
					CreateMesh(cubeGrid.cubes[y, x, z]);
				}
			}
		}

		Mesh mesh = new Mesh();
		mf.mesh = mesh;

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();
	}

	void OnDrawGizmos(){
		if (cubeGrid != null) {
			for (int y = 0; y < cubeGrid.cubes.GetLength(0); y ++) {
				for (int x = 0; x < cubeGrid.cubes.GetLength(1); x ++) {
					for (int z = 0; z < cubeGrid.cubes.GetLength (2); z++) {
						DrawCube (cubeGrid.cubes [y, x, z].topSquare.forwardLeft);
						DrawCube (cubeGrid.cubes [y, x, z].topSquare.forwardRight);
						DrawCube (cubeGrid.cubes [y, x, z].topSquare.backwardRight);
						DrawCube (cubeGrid.cubes [y, x, z].topSquare.backwardLeft);
						//Vector3 pos = new Vector3 (-cubeGrid.cubes.GetLength(1) / 2 + x + .5f, -cubeGrid.cubes.GetLength(0) / 2 + y + .5f, -cubeGrid.cubes.GetLength(2) / 2 + z + .5f);
						//Gizmos.DrawCube (pos, Vector3.one * .1f);
					}
				}
			}
		}
	}

	void DrawCube(ControlNode node) {
		Gizmos.color = node.active ? Color.white : Color.black;
		Gizmos.DrawCube (node.position, Vector3.one * .1f);
	}

	/*
	void TriangulateSquare(Square square) {
		switch (square.configuration) {
		case 0:
			break;

			// 1 points:
		case 1:
			MeshFromPoints(square.centreBackward, square.backwardLeft, square.centreLeft);
			break;
		case 2:
			MeshFromPoints(square.centreRight, square.backwardRight, square.centreBackward);
			break;
		case 4:
			MeshFromPoints(square.centreForward, square.forwardRight, square.centreRight);
			break;
		case 8:
			MeshFromPoints(square.forwardLeft, square.centreForward, square.centreLeft);
			break;

			// 2 points:
		case 3:
			MeshFromPoints(square.centreRight, square.backwardRight, square.backwardLeft, square.centreLeft);
			break;
		case 6:
			MeshFromPoints(square.centreForward, square.forwardRight, square.backwardRight, square.centreBackward);
			break;
		case 9:
			MeshFromPoints(square.forwardLeft, square.centreForward, square.centreBackward, square.backwardLeft);
			break;
		case 12:
			MeshFromPoints(square.forwardLeft, square.forwardRight, square.centreRight, square.centreLeft);
			break;
		case 5:
			MeshFromPoints(square.centreForward, square.forwardRight, square.centreRight, square.centreBackward, square.backwardLeft, square.centreLeft);
			break;
		case 10:
			MeshFromPoints(square.forwardLeft, square.centreForward, square.centreRight, square.backwardRight, square.centreBackward, square.centreLeft);
			break;

			// 3 point:
		case 7:
			MeshFromPoints(square.centreForward, square.forwardRight, square.backwardRight, square.backwardLeft, square.centreLeft);
			break;
		case 11:
			MeshFromPoints(square.forwardLeft, square.centreForward, square.centreRight, square.backwardRight, square.backwardLeft);
			break;
		case 13:
			MeshFromPoints(square.forwardLeft, square.forwardRight, square.centreRight, square.centreBackward, square.backwardLeft);
			break;
		case 14:
			MeshFromPoints(square.forwardLeft, square.forwardRight, square.backwardRight, square.centreBackward, square.centreLeft);
			break;

			// 4 point:
		case 15:
			MeshFromPoints(square.forwardLeft, square.forwardRight, square.backwardRight, square.backwardLeft);
			break;
		}
	}
*/

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
					}
				} else if (cube.topSquare.backwardLeft.active) {
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
				}
			} else if (cube.topSquare.forwardRight.active) {
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
					}
				} else if (cube.topSquare.backwardLeft.active) {
					Node[] points = new Node[] {
						cube.topSquare.centreForward,
						cube.topSquare.centreRight,
						cube.topSquare.centreBackward,
						cube.topSquare.centreLeft
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [2], points [3]);
				}
			} else if (cube.topSquare.backwardRight.active) {
				if (cube.topSquare.backwardLeft.active) {
					Node[] points = new Node[] {
						cube.topSquare.centreBackward,
						cube.middleBackwardRight,
						cube.middleBackwardLeft,
						cube.topSquare.centreLeft,
						cube.topSquare.centreRight,
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [3], points [4]);
				}
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
						}
					} else if (cube.bottomSquare.backwardLeft.active) {
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
					}
				} else if (cube.bottomSquare.backwardRight.active) {
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
					}
				} else if (cube.bottomSquare.backwardLeft.active) {
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
				}
			} else if (cube.bottomSquare.forwardRight.active) {
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
					}
				} else if (cube.bottomSquare.backwardLeft.active) {
					Node[] points = new Node[] {
						cube.bottomSquare.centreLeft,
						cube.bottomSquare.centreBackward,
						cube.bottomSquare.centreRight,
						cube.bottomSquare.centreForward
					};
					AssignVertices (points);
					CreateTriangle (points [0], points [1], points [2]);
					CreateTriangle (points [0], points [2], points [3]);
				}
			} else if (cube.bottomSquare.backwardRight.active) {
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
				}
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

	void MeshFromPoints(bool isTopSquare, params Node[] points){
		AssignVertices(points);

		//single control node
		if (points.Length == 4){
			CreateTriangle (points [0], points [1], points [2]);
			CreateTriangle (points [0], points [2], points [3]);

			if (isTopSquare) {
				CreateTriangle (points [0], points [1], points [3]);
			} else {
				CreateTriangle (points [0], points [3], points [1]);
			}
		}

		if (points.Length == 5) {
			CreateTriangle (points [0], points [1], points [2]);
			CreateTriangle (points [0], points [3], points [4]);
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

	void CreateTriangle(Node a, Node b, Node c) {
		triangles.Add(a.vertexIndex);
		triangles.Add(b.vertexIndex);
		triangles.Add(c.vertexIndex);
	}

	public class CubeGrid {
		public Cube[,,] cubes;

		public CubeGrid(List<int[,]> maps, float squareSize) {

			int nodeCountX = maps[0].GetLength(0);
			int nodeCountZ = maps[0].GetLength(1);
			float mapWidth = nodeCountX * squareSize;
			float mapDepth = nodeCountZ * squareSize;

			ControlNode[,,] controlNodes = new ControlNode[maps.Count, nodeCountX, nodeCountZ];

			for(int y = 0; y < maps.Count; y ++) {
				for (int x = 0; x < nodeCountX; x ++) {
					for (int z = 0; z < nodeCountZ; z ++) {
						Vector3 pos = new Vector3(-mapWidth/2 + x * squareSize + squareSize/2, -maps.Count/2 + y * squareSize + squareSize/2, -mapDepth/2 + z * squareSize + squareSize/2);
						controlNodes[y, x, z] = new ControlNode(pos, maps[y][x, z] == 1, squareSize);
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

		public Cube (ControlNode _topHalfTopLeft, ControlNode _topHalfTopRight, ControlNode _topHalfBottomRight, ControlNode _topHalfBottomLeft,
			ControlNode _bottomHalfTopLeft, ControlNode _bottomHalfTopRight, ControlNode _bottomHalfBottomRight, ControlNode _bottomHalfBottomLeft) {

			topSquare = new Square(_topHalfTopLeft, _topHalfTopRight, _topHalfBottomRight, _topHalfBottomLeft);
			bottomSquare = new Square(_bottomHalfTopLeft, _bottomHalfTopRight, _bottomHalfBottomRight, _bottomHalfBottomLeft);

			middleForwardLeft = bottomSquare.forwardLeft.above;
			middleForwardRight = bottomSquare.forwardRight.above;
			middleBackwardRight = bottomSquare.backwardRight.above;
			middleBackwardLeft = bottomSquare.backwardLeft.above;

			int count = 0;

			if (bottomSquare.forwardLeft.active){
				configuration += 128;
				count++;
			}
			if (bottomSquare.forwardRight.active){
				configuration += 64;
				count++;
			}
			if (bottomSquare.backwardRight.active){
				configuration += 32;
				count++;
			}
			if (bottomSquare.backwardLeft.active){
				configuration += 16;
				count++;
			}

			if (topSquare.forwardLeft.active){
				configuration += 8;
				count++;
			}
			if (topSquare.forwardRight.active){
				configuration += 4;
				count++;
			}
			if (topSquare.backwardRight.active){
				configuration += 2;
				count++;
			}
			if (topSquare.backwardLeft.active){
				configuration += 1;
				count++;
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