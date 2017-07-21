using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

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
					GameObject go = Instantiate(chunkManagerPrefab, new Vector3(16*k, 16*i, 16*j), Quaternion.identity);
					go.GetComponent<ChunkManager>().Init(go,randomFill, randomAdd, generator);
				}
			}
		}

		//chunkManagerPrefab = Resources.Load("ChunkManager") as GameObject;
	}
}