using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public static int dimension = 17;
	public int squareSize = 1;
	public static int staticSquareSize;

	public static WorldManager instance;

	public GameObject chunkManagerPrefab;

	public string seed = "Testing";
	public bool useRandomSeed = false;

	public static Hashtable world = new Hashtable();

	[Range(0,100)]
	public int randomAdd;
	public static int staticRandomAdd;

	public PlayerPosition playerPos = new PlayerPosition(0, 0, 0);
	[Range(0f,10f)]
	public float playerDistanceLimit = 1f;

	private static List<GameObject> go = new List<GameObject>();
	[Range(0f,1f)]
	public float smoothPercentage;

	public List<Vector3> worldSurface = new List<Vector3>();

	void Awake(){
		staticRandomAdd = randomAdd;
		staticSquareSize = squareSize;

		if(instance){
			Destroy(this);
		}else{
			instance = this;
		}

		HeightMapManager.SegmentHeightMap ();

		//GenerateSurfaceMap (0, 0, 0);
		//GenerateChunk(0, 0, 0);
	}
		
	void Update(){
		if(Input.GetKeyDown(KeyCode.Return)){
			world.Clear();
			foreach(GameObject g in go){
				Destroy(g);
			}

			//GenerateSurfaceMap (0, 0, 0);
			//GenerateChunk(0, 0, 0);
		}
	}

	public static void RegisterChunk(int x, int y, int z, ChunkManager chunkManager){
		string chunkKey = x + "," + y + "," + z;

		world [chunkKey] = chunkManager;

		go.Add (chunkManager.gameObject);

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

		(world [chunkKey] as ChunkManager).myKey = chunkKey;
		(world [chunkKey] as ChunkManager).Init(chunkManager.gameObject, staticRandomAdd, staticSquareSize);
	}

	void GenerateSurfaceMap(int x, int y, int z){

		Generator.smoothPercentage = smoothPercentage;

		string chunkKey = x + "," + y + "," + z;

		GameObject go = Instantiate(chunkManagerPrefab, new Vector3((dimension-1) * x, (dimension-1) * y, (dimension-1) * z), Quaternion.identity);
		world[chunkKey] = go.GetComponent<ChunkManager>();
		WorldManager.go.Add(go);

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

		(world [chunkKey] as ChunkManager).myKey = chunkKey;
		(world [chunkKey] as ChunkManager).Init(go, randomAdd, squareSize);

		if(Vector3.Distance(playerPos.pos, new Vector3(x, y, z)) < playerDistanceLimit){
			if(!world.ContainsKey(y1)){
				GenerateSurfaceMap (x, y + 1, z);
			}
			if(!world.ContainsKey(y_1)){
				GenerateSurfaceMap (x, y - 1, z);
			}

			if(!world.ContainsKey(x_1)){
				GenerateSurfaceMap (x - 1, y, z);
			}
			if(!world.ContainsKey(x1)){
				GenerateSurfaceMap (x + 1, y, z);
			}

			if(!world.ContainsKey(z1)){
				GenerateSurfaceMap (x, y, z + 1);
			}
			if(!world.ContainsKey(z_1)){
				GenerateSurfaceMap (x, y, z - 1);
			}
		}
	}

	void GenerateChunk(int x, int y, int z){

		Generator.smoothPercentage = smoothPercentage;

		// string operations are consuming
		string chunkKey = x + "," + y + "," + z;

		GameObject go = Instantiate(chunkManagerPrefab, new Vector3((dimension-1) * x, (dimension-1) * y, (dimension-1) * z), Quaternion.identity);
		world[chunkKey] = go.GetComponent<ChunkManager>();
		WorldManager.go.Add(go);

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

		(world [chunkKey] as ChunkManager).myKey = chunkKey;
		//(world[chunkKey] as ChunkManager).Init(go, randomAdd, squareSize);

		if(Vector3.Distance(playerPos.pos, new Vector3(x, y, z)) < playerDistanceLimit){
			if(!world.ContainsKey(y1)){
				GenerateChunk(x, y + 1, z);
			}
			if(!world.ContainsKey(y_1)){
				GenerateChunk(x, y - 1, z);
			}
			if(!world.ContainsKey(x_1)){
				GenerateChunk(x - 1, y, z);
			}
			if(!world.ContainsKey(x1)){
				GenerateChunk(x + 1, y, z);
			}
			if(!world.ContainsKey(z1)){
				GenerateChunk(x, y, z + 1);
			}
			if(!world.ContainsKey(z_1)){
				GenerateChunk(x, y, z - 1);
			}
		}
	}

	/*
	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;

		if (worldSurface.Count > 0) {
			foreach (Vector3 v in worldSurface) {
				Gizmos.DrawCube (v, new Vector3 (1f, 0.05f, 1f));
			}
		}
	}
	*/
}