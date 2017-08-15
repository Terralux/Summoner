using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChunkGenerator : MonoBehaviour {

	private ChunkManager myChunk;

	[Range(0,100)]
	public int random;
	public string seed;

	private Generator gen;

	[Range(0f,1f)]
	public float smoothPercentage;

	void Awake () {
		Random.InitState (seed.GetHashCode());

		myChunk = gameObject.AddComponent<ChunkManager>();
		gen = new Generator();
		gen.smoothPercentage = smoothPercentage;
		myChunk.myKey = "0,0,0";
		myChunk.Init(gameObject, random, gen.GenerateStartCubeGrid, 17, 1);
	}

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			gen.smoothPercentage = smoothPercentage;
			myChunk.Init(gameObject, random, gen.GenerateStartCubeGrid, 17, 1);
		}
	}
}