using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapGenerator : MonoBehaviour {

	public int width;
	public int depth;
	public int height;

	public string seed;
	public bool useRandomSeed;

	[Range(0,100)]
	public int randomFillPercent;

	bool[,] map;

	public bool useTestCube;

	[Header("Cube Generation")]
	[Space(5)]
	[Header("Top")]
	public bool cubeTopForwardLeft;
	public bool cubeTopForwardRight;
	public bool cubeTopBackRight;
	public bool cubeTopBackLeft;

	[Space(5)]
	[Header("Bottom")]
	public bool cubeBottomForwardLeft;
	public bool cubeBottomForwardRight;
	public bool cubeBottomBackRight;
	public bool cubeBottomBackLeft;

	void Start() {
		if (useTestCube) {
			GenerateTestCube ();
		} else {
			GenerateMap ();
		}
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			cubeTopForwardLeft = !cubeTopForwardLeft;
			GenerateTestCube ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			cubeTopForwardRight = !cubeTopForwardRight;
			GenerateTestCube ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			cubeTopBackRight = !cubeTopBackRight;
			GenerateTestCube ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			cubeTopBackLeft = !cubeTopBackLeft;
			GenerateTestCube ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			cubeBottomForwardLeft = !cubeBottomForwardLeft;
			GenerateTestCube ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			cubeBottomForwardRight = !cubeBottomForwardRight;
			GenerateTestCube ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha7)) {
			cubeBottomBackRight = !cubeBottomBackRight;
			GenerateTestCube ();
		}

		if (Input.GetKeyDown (KeyCode.Alpha8)) {
			cubeBottomBackLeft = !cubeBottomBackLeft;
			GenerateTestCube ();
		}

		if (Input.GetMouseButtonDown(0)) {
			if (useTestCube) {
				GenerateTestCube ();
			} else {
				GenerateMap ();
				UpdateCollision ();
			}
		}
	}

	void GenerateTestCube(){
		List<bool[,]> maps = new List<bool[,]> ();
		maps.Add (new bool[,]{ { cubeBottomBackLeft, cubeBottomForwardLeft }, { cubeBottomBackRight, cubeBottomForwardRight } });
		maps.Add (new bool[,]{ { cubeTopBackLeft, cubeTopForwardLeft }, { cubeTopBackRight, cubeTopForwardRight } });

		MeshGenerator meshGen = GetComponent<MeshGenerator>();
		meshGen.GenerateMesh(maps, 1);
	}

	void UpdateCollision(){
		if (GetComponent<MeshCollider> () == null) {
			gameObject.AddComponent<MeshCollider> ();
		}
		GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter> ().sharedMesh;
	}

	void GenerateMap() {
		List<bool[,]> maps = new List<bool[,]> ();

		for(int i = 0; i < height; i++){
			map = new bool[width, depth];
			RandomFillMap();

			for (int s = 0; s < 5; s++) {
				SmoothMap();
			}
			maps.Add (map);
		}

		MeshGenerator meshGen = GetComponent<MeshGenerator>();
		meshGen.GenerateMesh(maps, 1);
	}

	void RandomFillMap() {
		if (useRandomSeed) {
			seed = System.DateTime.Now.Millisecond.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < depth; y ++) {
				map [x, y] = (pseudoRandom.Next (0, 100) < randomFillPercent) ? true : false;
				/*
				if (x == 0 || x == width-1 || y == 0 || y == depth -1) {
					map[x,y] = true;
				}
				else {
					map [x, y] = (pseudoRandom.Next (0, 100) < randomFillPercent) ? true : false;
				}
				*/
			}
		}
	}

	void SmoothMap() {
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < depth; y ++) {
				int neighbourWallTiles = GetSurroundingWallCount(x,y);

				if (neighbourWallTiles > 4)
					map[x,y] = true;
				else if (neighbourWallTiles < 4)
					map[x,y] = false;

			}
		}
	}

	int GetSurroundingWallCount(int gridX, int gridY) {
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < depth) {
					if (neighbourX != gridX || neighbourY != gridY) {
						wallCount += map [neighbourX, neighbourY] ? 1 : 0;
					}
				}
				else {
					wallCount ++;
				}
			}
		}

		return wallCount;
	}
}