using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	[Tooltip("The dimensions of the chunk!")]
	public int dimension = 17;
	public int squareSize = 1;

	public static WorldManager instance;
	public static Generator generator;

	public GameObject chunkManagerPrefab;

	public string seed = "Testing";
	public bool useRandomSeed = false;

	public static Hashtable world = new Hashtable();

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
		generator = new Generator();

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
		generator.smoothPercentage = smoothPercentage;

		string chunkKey = x + "," + y + "," + z;

		GameObject go = Instantiate(chunkManagerPrefab, new Vector3((dimension-1) * x, (dimension-1) * y, (dimension-1) * z), Quaternion.identity);
		world[chunkKey] = go.GetComponent<ChunkManager>();
		this.go.Add(go);

		if(world.ContainsKey(x + "," + (y + 1) + "," + z)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.top, world[x + "," + (y + 1) + "," + z] as ChunkManager);
		}
		if(world.ContainsKey(x + "," + (y - 1) + "," + z)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.bottom, world[x + "," + (y - 1) + "," + z] as ChunkManager);
		}
		if(world.ContainsKey((x - 1) + "," + y + "," + z)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.left, world[(x - 1) + "," + y + "," + z] as ChunkManager);
		}
		if(world.ContainsKey((x + 1) + "," + y + "," + z)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.right, world[(x + 1) + "," + y + "," + z] as ChunkManager);
		}
		if(world.ContainsKey(x + "," + y + "," + (z + 1))){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.forward, world[x + "," + y + "," + (z + 1)] as ChunkManager);
		}
		if(world.ContainsKey(x + "," + y + "," + (z - 1))){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.back, world[x + "," + y + "," + (z - 1)] as ChunkManager);
		}

		if(x == 0 && y == 0 && z == 0){
			(world[chunkKey] as ChunkManager).myKey = chunkKey;
			(world[chunkKey] as ChunkManager).Init(go, randomAdd, generator.GenerateStartCubeGrid, dimension, squareSize);
		}else{
			(world[chunkKey] as ChunkManager).myKey = chunkKey;
			(world[chunkKey] as ChunkManager).Init(go, randomAdd, generator.GenerateRecursiveCellularAutomata, dimension, squareSize);
		}

		if(Vector3.Distance(playerPos.pos, new Vector3(x,y,z)) < playerDistanceLimit){
			if(!world.ContainsKey(x + "," + (y + 1) + "," + z)){
				if((world[chunkKey] as ChunkManager).hasAccessTo.top){
					GenerateChunk(x, y + 1, z);
				}
			}
			if(!world.ContainsKey(x + "," + (y - 1) + "," + z)){
				if((world[chunkKey] as ChunkManager).hasAccessTo.bottom){
					GenerateChunk(x, y - 1, z);
				}
			}
			if(!world.ContainsKey((x - 1) + "," + y + "," + z)){
				if((world[chunkKey] as ChunkManager).hasAccessTo.left){
					GenerateChunk(x - 1, y, z);
				}
			}
			if(!world.ContainsKey((x + 1) + "," + y + "," + z)){
				if((world[chunkKey] as ChunkManager).hasAccessTo.right){
					GenerateChunk(x + 1, y, z);
				}
			}
			if(!world.ContainsKey(x + "," + y + "," + (z + 1))){
				if((world[chunkKey] as ChunkManager).hasAccessTo.forward){
					GenerateChunk(x, y, z + 1);
				}
			}
			if(!world.ContainsKey(x + "," + y + "," + (z - 1))){
				if((world[chunkKey] as ChunkManager).hasAccessTo.back){
					GenerateChunk(x, y, z - 1);
				}
			}
		}
	}
}