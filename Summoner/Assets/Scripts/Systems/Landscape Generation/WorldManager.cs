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

	public static Hashtable world = new Hashtable();

	[Range(0,100)]
	public int randomFill;
	[Range(0,100)]
	public int randomAdd;

	public PlayerPosition playerPos = new PlayerPosition(0, 0, 0);
	[Range(1f,10f)]
	public float playerDistanceLimit = 1f;

	void Awake(){
		if(instance){
			Destroy(this);
		}else{
			instance = this;
		}

		Random.InitState (seed.GetHashCode());
		generator = new Generator();

		//GenerateWorld();
		GenerateChunk(0, 0, 0);
	}

	void GenerateChunk(int x, int y, int z){
		string chunkKey = x + "," + y + "," + z;

		GameObject go = Instantiate(chunkManagerPrefab, new Vector3((dimension-1) * x, (dimension-1) * y, (dimension-1) * z), Quaternion.identity);
		world[chunkKey] = go.GetComponent<ChunkManager>();

		if(x == 0 && y == 0 && z == 0){
			(world[chunkKey] as ChunkManager).Init(go, randomFill, randomAdd, generator.GenerateStartCubeGrid, dimension, squareSize);
		}else{
			(world[chunkKey] as ChunkManager).Init(go, (randomFill + 100)/2, (randomAdd + 100)/2, generator.GenerateStartSurroundings, dimension, squareSize);
		}

		if(world.ContainsKey(x + "," + (y + 1) + "," + z)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.top, world[x + "," + (y + 1) + "," + z] as ChunkManager);
		}
		if(world.ContainsKey(x + "," + (y - 1) + "," + z)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.bottom, world[x + "," + (y - 1) + "," + z] as ChunkManager);
		}
		if(world.ContainsKey((x - 1) + "," + y + "," + z)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.bottom, world[(x - 1) + "," + y + "," + z] as ChunkManager);
		}
		if(world.ContainsKey((x + 1) + "," + y + "," + z)){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.bottom, world[(x + 1) + "," + y + "," + z] as ChunkManager);
		}
		if(world.ContainsKey(x + "," + y + "," + (z + 1))){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.bottom, world[x + "," + y + "," + (z + 1)] as ChunkManager);
		}
		if(world.ContainsKey(x + "," + y + "," + (z - 1))){
			(world[chunkKey] as ChunkManager).AddNeighbour(Direction.bottom, world[x + "," + y + "," + (z - 1)] as ChunkManager);
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

	/*
	void GenerateWorld(){
		
		world = new ChunkManager[x, y, z];

		for(int i = -(y/2); i < y/2; i++){
			for(int j = -(z/2); j < z/2; j++){
				for(int k = -(x/2); k < x/2; k++){
					GameObject go = Instantiate(chunkManagerPrefab, new Vector3((dimension-1)*k, (dimension-1)*i, (dimension-1)*j), Quaternion.identity);
					world[k + (x/2), i + (y/2), j + (z/2)] = go.GetComponent<ChunkManager>();

					if(i == 0 && j == 0 && k == 0){
						world[k + (x/2), i + (y/2), j + (z/2)].Init(go, randomFill, randomAdd, generator.GenerateStartCubeGrid, dimension, squareSize);
					}else{
						world[k + (x/2), i + (y/2), j + (z/2)].Init(go, (randomFill + 100)/2, (randomAdd + 100)/2, generator.GenerateStartSurroundings, dimension, squareSize);
					}

					if(k + (x/2) > 0){
						world[k + (x/2), i + (y/2), j + (z/2)].chunkNeighbours.left = world[k + (x/2) - 1, i + (y/2), j + (z/2)];
						world[k + (x/2) - 1, i + (y/2), j + (z/2)].chunkNeighbours.right = world[k + (x/2), i + (y/2), j + (z/2)];
					}

					if(i + (y/2) > 0){
						world[k + (x/2), i + (y/2), j + (z/2)].chunkNeighbours.bottom = world[k + (x/2), i + (y/2) - 1, j + (z/2)];
						world[k + (x/2), i + (y/2) - 1, j + (z/2)].chunkNeighbours.top = world[k + (x/2), i + (y/2), j + (z/2)];
					}

					if(j + (z/2) > 0){
						world[k + (x/2), i + (y/2), j + (z/2)].chunkNeighbours.back = world[k + (x/2), i + (y/2), j + (z/2) - 1];
						world[k + (x/2), i + (y/2), j + (z/2) - 1].chunkNeighbours.forward= world[k + (x/2), i + (y/2), j + (z/2)];
					}
				}
			}
		}

		world[x/2, y/2, z/2].chunkNeighbours.Print();
	}
	*/
}