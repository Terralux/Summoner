using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public static int dimension = 17;
	public int squareSize = 1;

	public static WorldManager instance;

	public GameObject chunkManagerPrefab;

	public string seed = "Testing";
	public bool useRandomSeed = false;

	public static Hashtable world = new Hashtable();

	/*
	 * bounding box for colliders
	 * LOD chunk optimization
	*/

	[Range(0,100)]
	public int randomAdd;

	public PlayerPosition playerPos = new PlayerPosition(0, 0, 0);
	[Range(0f,10f)]
	public float playerDistanceLimit = 1f;

	private List<GameObject> go = new List<GameObject>();
	[Range(0f,1f)]
	public float smoothPercentage;

	void Awake(){
		if(instance){
			Destroy(this);
		}else{
			instance = this;
		}

		Random.InitState (seed.GetHashCode());

		GenerateChunk(0, 0, 0);
	}
		
	void Update(){
		if(Input.GetKeyDown(KeyCode.Return)){
			world.Clear();
			foreach(GameObject g in go){
				Destroy(g);
			}

			GenerateChunk(0,0,0);
		}
	}

	void GenerateChunk(int x, int y, int z){
		Generator.smoothPercentage = smoothPercentage;

		// string operations are consuming
		string chunkKey = x + "," + y + "," + z;

		GameObject go = Instantiate(chunkManagerPrefab, new Vector3((dimension-1) * x, (dimension-1) * y, (dimension-1) * z), Quaternion.identity);
		world[chunkKey] = go.GetComponent<ChunkManager>();
		this.go.Add(go);

		string y1 = x + "," + (y + 1) + "," + z;
		if(world.ContainsKey(y1)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.top, world[y1] as ChunkManager);
		}
		string y_1 = x + "," + (y - 1) + "," + z;
		if(world.ContainsKey(y_1)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.bottom, world[y_1] as ChunkManager);
		}
		string x_1 = (x - 1) + "," + y + "," + z;
		if(world.ContainsKey(x_1)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.left, world[x_1] as ChunkManager);
		}
		string x1 = (x + 1) + "," + y + "," + z;
		if(world.ContainsKey(x1)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.right, world[x1] as ChunkManager);
		}
		string z1 = x + "," + y + "," + (z + 1);
		if(world.ContainsKey(z1)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.forward, world[z1] as ChunkManager);
		}
		string z_1 = x + "," + y + "," + (z - 1);
		if(world.ContainsKey(z_1)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.back, world[z_1] as ChunkManager);
		}

		(world[chunkKey] as ChunkManager).myKey = chunkKey;
		(world[chunkKey] as ChunkManager).Init(go, randomAdd, squareSize);

		if(Vector3.Distance(playerPos.pos, new Vector3(x, y, z)) < playerDistanceLimit){
			if(!world.ContainsKey(y1)){
				if((world[chunkKey] as ChunkManager).hasAccessTo.top){
					GenerateChunk(x, y + 1, z);
				}
			}
			if(!world.ContainsKey(y_1)){
				if((world[chunkKey] as ChunkManager).hasAccessTo.bottom){
					GenerateChunk(x, y - 1, z);
				}
			}
			if(!world.ContainsKey(x_1)){
				if((world[chunkKey] as ChunkManager).hasAccessTo.left){
					GenerateChunk(x - 1, y, z);
				}
			}
			if(!world.ContainsKey(x1)){
				if((world[chunkKey] as ChunkManager).hasAccessTo.right){
					GenerateChunk(x + 1, y, z);
				}
			}
			if(!world.ContainsKey(z1)){
				if((world[chunkKey] as ChunkManager).hasAccessTo.forward){
					GenerateChunk(x, y, z + 1);
				}
			}
			if(!world.ContainsKey(z_1)){
				if((world[chunkKey] as ChunkManager).hasAccessTo.back){
					GenerateChunk(x, y, z - 1);
				}
			}
		}
	}
}