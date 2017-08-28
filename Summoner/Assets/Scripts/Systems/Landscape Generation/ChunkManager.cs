using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ChunkManager : MonoBehaviour {
	private int squareSize = 1;

	private Chunk myChunk;

	private MeshCollider mc;
	private MeshFilter mf;
	private GameObject targetObjectChunk;

	public Neighbours chunkNeighbours;

	public Accessible hasAccessTo;

	public string myKey;

	public void Init(GameObject neoChunk, int randomAdditionPercentage, int squareSize){
		this.squareSize = squareSize;

		if(targetObjectChunk != null && targetObjectChunk != neoChunk){
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

		GenerateStartCube(randomAdditionPercentage);
	}

	void GenerateStartCube (int randomAdditionPercentage){
		MeshGenerator meshGen = new MeshGenerator();

		myChunk = new Chunk(
			Generator.GenerateChunk(myKey, 
				new CubeTemplate(
					(chunkNeighbours.top != null ? chunkNeighbours.top.myChunk.bottom : null), 
					(chunkNeighbours.bottom != null ? chunkNeighbours.bottom.myChunk.top : null), 
					(chunkNeighbours.left != null ? chunkNeighbours.left.myChunk.right : null), 
					(chunkNeighbours.right != null ? chunkNeighbours.right.myChunk.left : null), 
					(chunkNeighbours.forward != null ? chunkNeighbours.forward.myChunk.back : null), 
					(chunkNeighbours.back != null ? chunkNeighbours.back.myChunk.forward : null)
				), chunkNeighbours, out hasAccessTo
			), 
			squareSize);

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

	void OnDrawGizmos(){
		for(int x = 0; x < myChunk.slices.Length - 1; x++){
			for(int y = 0; y < myChunk.slices.Length - 1; y++){
				for(int z = 0; z < myChunk.slices.Length - 1; z++){
					if(myChunk.slices[y].cubes[x, z].topSquare.forwardLeft.isLiquidSource){
						Gizmos.color = Color.blue;
						Gizmos.DrawSphere(myChunk.slices[y].cubes[x, z].topSquare.forwardLeft.position + targetObjectChunk.transform.position, 0.2f);
					}
				}
			}
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