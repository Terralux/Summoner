using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ChunkManager : MonoBehaviour{
	private int dimension = 17;
	private int squareSize = 1;

	private Chunk myChunk;

	private MeshCollider mc;
	private MeshFilter mf;
	private GameObject targetObjectChunk;

	public Neighbours chunkNeighbours;

	public Accessible hasAccessTo;

	public delegate List<bool[,]> ChunkGenerator(int random, int dimension, out Accessible access);

	public void Init(GameObject neoChunk, int randomFillPercentage, int randomAdditionPercentage, ChunkGenerator gen, int dimension, int squareSize){
		this.dimension = dimension;
		this.squareSize = squareSize;

		if(targetObjectChunk != null){
			Destroy(targetObjectChunk);
		}else{
			targetObjectChunk = neoChunk;
		}

		mc = targetObjectChunk.GetComponent<MeshCollider> ();
		mf = targetObjectChunk.GetComponent<MeshFilter> ();

		if (mc == null) {
			mc = targetObjectChunk.AddComponent<MeshCollider> ();
		}
		if (mf == null){
			mf = targetObjectChunk.AddComponent<MeshFilter> ();
		}

		GenerateStartCube(gen, randomFillPercentage, randomAdditionPercentage);
	}

	void GenerateStartCube (ChunkGenerator gen, int randomFillPercentage, int randomAdditionPercentage){
		MeshGenerator meshGen = new MeshGenerator();

		myChunk = new Chunk(gen(randomAdditionPercentage, dimension, out hasAccessTo), squareSize);

		meshGen.GenerateMesh (mf, myChunk);
		UpdateCollision();
	}

	void UpdateCollision(){
		mc.sharedMesh = mf.sharedMesh;
	}

	public void AddNeighbour(Direction dir, ChunkManager cm){
		switch(dir){
		case Direction.top:
			chunkNeighbours.top = cm;
			cm.chunkNeighbours.bottom = this;
			break;
		case Direction.bottom:
			chunkNeighbours.bottom = cm;
			cm.chunkNeighbours.top = this;
			break;
		case Direction.left:
			chunkNeighbours.left = cm;
			cm.chunkNeighbours.right = this;
			break;
		case Direction.right:
			chunkNeighbours.right = cm;
			cm.chunkNeighbours.left = this;
			break;
		case Direction.forward:
			chunkNeighbours.forward = cm;
			cm.chunkNeighbours.back = this;
			break;
		case Direction.back:
			chunkNeighbours.back = cm;
			cm.chunkNeighbours.forward = this;
			break;
		}
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

public struct Accessible{
	public bool top;
	public bool bottom;
	public bool left;
	public bool right;
	public bool forward;
	public bool back;

	public Accessible(bool top, bool bottom, bool left, bool right, bool forward, bool back){
		this.top = top;
		this.bottom = bottom;
		this.left = left;
		this.right = right;
		this.forward = forward;
		this.back = back;
	}
}

public struct Neighbours{
	public ChunkManager top;
	public ChunkManager bottom;
	public ChunkManager left;
	public ChunkManager right;
	public ChunkManager forward;
	public ChunkManager back;

	public Neighbours(ChunkManager top, ChunkManager bottom, ChunkManager left, ChunkManager right, ChunkManager forward, ChunkManager back){
		this.top = top;
		this.bottom = bottom;
		this.left = left;
		this.right = right;
		this.forward = forward;
		this.back = back;
	}

	public void Print(){
		Debug.Log(top + " " + bottom + " " + left + " " + right + " " + forward + " " + back);
	}
}

public struct Connections{
	public bool top;
	public bool bottom;
	public bool left;
	public bool right;
	public bool forward;
	public bool back;

	public Connections(bool top, bool bottom, bool left, bool right, bool forward, bool back){
		this.top = top;
		this.bottom = bottom;
		this.left = left;
		this.right = right;
		this.forward = forward;
		this.back = back;
	}
}