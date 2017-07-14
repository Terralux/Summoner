using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapGenerator : MonoBehaviour {

	public int dimension;

	public string seed;
	public bool useRandomSeed;

	[Range(0,100)]
	public int randomFillPercent;

	[Range(0f,1f)]
	public float randomAdditionPercent;

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

	public int configuration = 0;

	void Start() {
		if (useTestCube) {
			GenerateTestCube ();
		} else {
			GenerateStartCube ();
			//GenerateMap ();
		}
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)){
			configuration++;
			GenerateTestCube();
		}

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
				GenerateStartCube ();
			}
		}
	}

	void GenerateTestCube(){
		List<bool[,]> maps = new List<bool[,]> ();

		//maps.Add (new bool[,]{ { configuration%256==255?true:false, configuration%32==31?true:false }, { configuration%128==127?true:false, configuration%64==63?true:false } });
		//maps.Add (new bool[,]{ { configuration%16==15?true:false, configuration%2==1?true:false }, { configuration%8==7?true:false, configuration%4==3?true:false } });
		/*
		maps.Add (new bool[,]{ { cubeBottomBackLeft, cubeBottomForwardLeft }, { cubeBottomBackRight, cubeBottomForwardRight } });
		maps.Add (new bool[,]{ { cubeTopBackLeft, cubeTopForwardLeft }, { cubeTopBackRight, cubeTopForwardRight } });
		*/
		MeshGenerator meshGen = new MeshGenerator();
		meshGen.GenerateMesh (GetComponent<MeshFilter>(), maps, 1);
	}

	void UpdateCollision(){
		if (GetComponent<MeshCollider> () == null) {
			gameObject.AddComponent<MeshCollider> ();
		}
		GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter> ().sharedMesh;
	}

	void GenerateStartCube (){
		map = new bool[dimension, dimension];

		for(int x = 0; x < dimension; x++){
			for(int z = 0; z < dimension; z++){
				map[x,z] = true;
			}
		}

		Generator gen = new Generator();
		MeshGenerator meshGen = new MeshGenerator();

		meshGen.GenerateMesh (GetComponent<MeshFilter>(), gen.GenerateStartCubeGrid(randomAdditionPercent, dimension), 1);
		UpdateCollision();
	}

	void GenerateMap() {
		/*
		List<bool[,]> maps = new List<bool[,]> ();

		for(int i = 0; i < height; i++){
			map = new bool[width, depth];
			RandomFillMap();

			for (int s = 0; s < 5; s++) {
				SmoothMap();
			}
			maps.Add (map);
		}
		*/

		map = new bool[dimension, dimension];
		RandomFillMap();

		for (int s = 0; s < 5; s++) {
			SmoothMap();
		}

		Generator gen = new Generator();
		MeshGenerator meshGen = new MeshGenerator();

		meshGen.GenerateMesh (GetComponent<MeshFilter>(), gen.Generate(Direction.XPositive, map, randomAdditionPercent), 1);
	}

	void RandomFillMap() {
		if (useRandomSeed) {
			seed = System.DateTime.Now.Millisecond.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());

		for (int x = 0; x < dimension; x ++) {
			for (int y = 0; y < dimension; y ++) {
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
		for (int x = 0; x < dimension; x ++) {
			for (int y = 0; y < dimension; y ++) {
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
				if (neighbourX >= 0 && neighbourX < dimension && neighbourY >= 0 && neighbourY < dimension) {
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