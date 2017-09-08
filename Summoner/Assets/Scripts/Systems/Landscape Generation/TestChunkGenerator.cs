using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChunkGenerator : MonoBehaviour {

	private ChunkManager myChunk;

	[Range(0,100)]
	public int random;
	public string seed;

	[Range(0f,1f)]
	public float smoothPercentage;

	public static int randomStatic;

	[Range(0,16)]
	public int xMin;
	[Range(0,16)]
	public int xMid;
	[Range(0,16)]
	public int xMax;

	[Range(0,16)]
	public int zMin;
	[Range(0,16)]
	public int zMid;
	[Range(0,16)]
	public int zMax;

	public bool TurnOnHiddenNodes = false;

	void Awake () {
		myChunk = gameObject.AddComponent<ChunkManager>();
		Generator.smoothPercentage = smoothPercentage;
		Generator.seed = seed;
		randomStatic = random;
		myChunk.myKey = "0,0,0";
		myChunk.Init(gameObject, random, 1);
	}

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			Generator.smoothPercentage = smoothPercentage;
			Generator.seed = seed;
			randomStatic = random;

			myChunk.Init(gameObject, random, 1);
		}
	}
}