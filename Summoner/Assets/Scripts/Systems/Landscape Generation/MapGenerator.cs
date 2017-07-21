using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapGenerator : MonoBehaviour {

	private const int dimension = 17;

	public string seed;
	public bool useRandomSeed;

	[Range(0,100)]
	public int randomFillPercent;

	[Range(0f,100f)]
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

			Debug.Log(System.DateTime.Now.Second + System.DateTime.Now.Millisecond);

			configuration++;

			if (useTestCube) {
				GenerateTestCube ();
			} else {
				GenerateStartCube ();
			}
		}
	}

	void GenerateTestCube(){
		List<bool[,]> maps = new List<bool[,]> ();

		cubeBottomBackLeft = false;
		cubeBottomForwardLeft = false;
		cubeBottomBackRight = false;
		cubeBottomForwardRight = false;
		cubeTopBackLeft = false;
		cubeTopForwardLeft = false;
		cubeTopBackRight = false;
		cubeTopForwardRight = false;

		int tempConfig = configuration;

		if(tempConfig >= 128){
			tempConfig -= 128;
			cubeBottomBackLeft = true;
		}
		if(tempConfig >= 64){
			tempConfig -= 64;
			cubeBottomBackRight = true;
		}
		if(tempConfig >= 32){
			tempConfig -= 32;
			cubeBottomForwardRight = true;
		}
		if(tempConfig >= 16){
			tempConfig -= 16;
			cubeBottomForwardLeft = true;
		}
		if(tempConfig >= 8){
			tempConfig -= 8;
			cubeTopBackLeft = true;
		}
		if(tempConfig >= 4){
			tempConfig -= 4;
			cubeTopBackRight = true;
		}
		if(tempConfig >= 2){
			tempConfig -= 2;
			cubeTopForwardRight = true;
		}
		if(tempConfig >= 1){
			tempConfig -= 1;
			cubeTopForwardLeft = true;
		}

		maps.Add(new bool[,]{ { cubeBottomBackLeft, cubeBottomForwardLeft }, { cubeBottomBackRight, cubeBottomForwardRight } });
		maps.Add(new bool[,]{ { cubeTopBackLeft, cubeTopForwardLeft }, { cubeTopBackRight, cubeTopForwardRight } });

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
		Generator gen = new Generator();
		MeshGenerator meshGen = new MeshGenerator();

		meshGen.GenerateMesh (GetComponent<MeshFilter>(), gen.GenerateStartCubeGrid(randomAdditionPercent, dimension), 1);
		UpdateCollision();
	}

	/*
	void GenerateMap() {

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
*/
}