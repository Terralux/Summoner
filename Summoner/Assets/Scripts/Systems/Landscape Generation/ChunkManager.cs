﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ChunkManager : MonoBehaviour{

	private const int dimension = 17;
	private const int squareSize = 1;

	bool[,] map;

	private MeshCollider mc;
	private MeshFilter mf;
	private GameObject targetChunk;

	public void Init(GameObject neoChunk, int randomFillPercentage, int randomAdditionPercentage, Generator gen){
		if(targetChunk != null){
			Destroy(targetChunk);
		}else{
			targetChunk = neoChunk;
		}

		mc = targetChunk.GetComponent<MeshCollider> ();
		mf = targetChunk.GetComponent<MeshFilter> ();

		if (mc == null) {
			mc = targetChunk.AddComponent<MeshCollider> ();
		}
		if (mf == null){
			mf = targetChunk.AddComponent<MeshFilter> ();
		}

		GenerateStartCube(gen, randomFillPercentage, randomAdditionPercentage);
	}

	void UpdateCollision(){
		mc.sharedMesh = mf.sharedMesh;
	}

	void GenerateStartCube (Generator gen, int randomFillPercentage, int randomAdditionPercentage){
		MeshGenerator meshGen = new MeshGenerator();

		meshGen.GenerateMesh (mf, gen.GenerateStartCubeGrid(randomAdditionPercentage, dimension), squareSize);
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

				if (x == 0 || x == width-1 || y == 0 || y == depth -1) {
					map[x,y] = true;
				}
				else {
					map [x, y] = (pseudoRandom.Next (0, 100) < randomFillPercent) ? true : false;
				}
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