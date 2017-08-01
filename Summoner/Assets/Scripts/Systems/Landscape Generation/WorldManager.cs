using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
	
	public int dimension = 17;
	public int squareSize = 1;

	public static WorldManager instance;
	public static Generator generator;

	public int x;
	public int y;
	public int z;

	public GameObject chunkManagerPrefab;

	public string seed = "Testing";
	public bool useRandomSeed = false;

	[Range(0,100)]
	public int randomFill;
	[Range(0,100)]
	public int randomAdd;

	void Awake(){
		Random.InitState (seed.GetHashCode());
		generator = new Generator();

		if(instance){
			Destroy(this);
		}else{
			instance = this;
		}

		for(int i = -(y/2); i < y/2; i++){
			for(int j = -(z/2); j < z/2; j++){
				for(int k = -(x/2); k < x/2; k++){
					GameObject go = Instantiate(chunkManagerPrefab, new Vector3((dimension-1)*k, (dimension-1)*i, (dimension-1)*j), Quaternion.identity);
					go.GetComponent<ChunkManager>().Init(go, randomFill, randomAdd, generator, dimension, squareSize);
				}
			}
		}
	}
}