using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
	top,
	bottom,
	right,
	left,
	forward,
	back
}

public struct Generator {

	public float smoothPercentage;

	public static List<bool[,]> GenerateChunk(string key, CubeTemplate template, Neighbours chunkNeighbours, out Accessible accessibility){
		if(template.IsEmpty()){
			return GenerateFlatChunk(WorldManager.dimension / 2, out accessibility);
		}

		accessibility = new Accessible(true,true,true,true,true,true);

		GetChunkType(key, chunkNeighbours);
		return new List<bool[,]>();
	}

	public static ChunkType GetChunkType (string key, Neighbours chunkNeighbours){
		return new Flat();
	}

	public List<bool[,]> Generate (Direction dir, bool[,] slice, float randomAddition){
		smoothPercentage = 0f;

		List<bool[,]> maps = new List<bool[,]> ();
		maps.Add(slice);

		for(int x = 1; x < slice.GetLength(0) + 1; x++){
			maps.Add(new bool[slice.GetLength(0), slice.GetLength(0)]);

			for(int y = 0; y < slice.GetLength(0); y++){
				for(int z = 0; z < slice.GetLength(0); z++){
					//Use a kernel to determine probability of segment active
					int probability = 0;
					int total = 0;
					for(int k1 = -1; k1 < 2; k1++){
						for(int k2 = -1; k2 < 2; k2++){
							if(y + k1 >= 0 && y + k1 < slice.GetLength(0) && z + k2 >= 0 && z + k2 < slice.GetLength(0)) {
								if(maps[x - 1][y + k1, z + k2]) {
									probability++;
								}
								total++;
							}
						}
					}
					float totalProbability = (float)probability / (float)total;

					if(totalProbability > Random.Range(0, 1f - randomAddition)){
						maps[x][y,z] = true;
					}
				}
			}
		}

		maps.RemoveAt(0);
		return maps;
	}

	public List<bool[,]> GenerateStartSurroundings(int randomAddition, out Accessible access){
		List<bool[,]> maps = new List<bool[,]>();

		for(int x = 0; x < WorldManager.dimension; x++){
			maps.Add(SliceFill());
		}

		maps.Add(SliceNoise(randomAddition));

		while(maps.Count < WorldManager.dimension * 2){
			maps.Add(SliceBlur(randomAddition, maps[maps.Count - 1]));
		}

		access = new Accessible(true, false, true, true, true, true);

		return maps;
	}

	public static List<bool[,]> GenerateFlatChunk (int heightSteps, out Accessible access){
		List<bool[,]> maps = new List<bool[,]>();

		for(int x = 0; x < heightSteps; x++){
			maps.Add(SliceFill());
		}

		while(maps.Count < WorldManager.dimension * 2){
			maps.Add(SliceEmpty());
		}
		
		access = new Accessible(true, false, true, true, true, true);
		return maps;
	}

	public static bool[,] SliceFill(){
		bool[,] maps = new bool[WorldManager.dimension, WorldManager.dimension];

		for(int y = 0; y < WorldManager.dimension; y++){
			for(int z = 0; z < WorldManager.dimension; z++){
				maps [y, z] = true;
			}
		}

		return maps;
	}

	public static bool[,] SliceEmpty(){
		return new bool[WorldManager.dimension, WorldManager.dimension];
	}

	public bool[,] SliceNoise(float fillRatio){
		bool[,] maps = new bool[WorldManager.dimension, WorldManager.dimension];

		for(int y = 0; y < WorldManager.dimension; y++){
			for(int z = 0; z < WorldManager.dimension; z++){
				if(Random.Range (0f, 100f) < fillRatio){
					maps [y, z] = true;
				}else{
					maps [y, z] = false;
				}
			}
		}

		return maps;
	}

	public bool[,] SliceNoise(int fillRatio, int safeZoneRadius){
		bool[,] maps = new bool[WorldManager.dimension, WorldManager.dimension];

		int center = (int)((float)WorldManager.dimension/2f);

		for(int y = 0; y < WorldManager.dimension; y++){
			for(int z = 0; z < WorldManager.dimension; z++){
				if(z < center + safeZoneRadius && z > center - safeZoneRadius && y < center + safeZoneRadius && y > center - safeZoneRadius){
					maps [y, z] = false;
				}else{
					if(Random.Range (0f, 100f) < fillRatio){
						maps [y, z] = true;
					}else{
						maps [y, z] = false;
					}
				}
			}
		}

		return maps;
	}

	public bool[,] SliceBlur(int fillRatio, bool[,] previous){
		bool[,] maps = new bool[WorldManager.dimension, WorldManager.dimension];

		for(int y = 0; y < WorldManager.dimension; y++){
			for(int z = 0; z < WorldManager.dimension; z++){
				if(previous[y, z]){
					if(Random.Range (0f, 100f) < fillRatio){
						maps [y, z] = true;
					}else{
						maps [y, z] = false;
					}
				}
			}
		}
		return maps;
	}

	public List<bool[,]> OptimizeSlices(List<bool[,]> maps){
		List<bool[,]> optimizedMaps = new List<bool[,]>();
		optimizedMaps.AddRange(maps);

		for(int y = 1; y < maps.Count - 1; y++){
			for(int x = 1; x < maps[y].GetLength(0) - 1; x++){
				for(int z = 1; z < maps[y].GetLength(0) - 1; z++){
					if(maps[y][x, z]){
						if(maps[y + (y % 2 == 1?1:0)][x, z] || 
							maps[y - (y % 2 == 1?0:1)][x + (x % 2 == 1?1:-1), z] || 
							maps[y + (y % 2 == 1?1:0)][x + (x % 2 == 1?1:-1), z] || 
							maps[y - (y % 2 == 1?0:1)][x, z + (z % 2 == 1?1:-1)] || 
							maps[y + (y % 2 == 1?1:0)][x, z + (z % 2 == 1?1:-1)] || 
							maps[y - (y % 2 == 1?0:1)][x + (x % 2 == 1?1:-1), z + (z % 2 == 1?1:-1)] || 
							maps[y + (y % 2 == 1?1:0)][x + (x % 2 == 1?1:-1), z + (z % 2 == 1?1:-1)]){
							optimizedMaps[y][x, z] = false;
						}
					}
				}
			}
		}
		return optimizedMaps;
	}

	public List<bool[,]> GenerateRecursiveCellularAutomata(string chunkKey, CubeTemplate template, int smoothIterations, int randomAddition, out Accessible access){
		List<bool[,]> maps = new List<bool[,]>();
		List<bool[,]> flags = new List<bool[,]>();

		for (int y = 0; y < WorldManager.dimension * 2; y ++) {
			maps.Add(new bool[WorldManager.dimension, WorldManager.dimension]);
			flags.Add(new bool[WorldManager.dimension, WorldManager.dimension]);
		}

		if(!template.IsEmpty()){
			for(int x = 0; x < WorldManager.dimension; x++){
				for(int y = 0; y < WorldManager.dimension; y++){
					if(template.left.GetLength(0) > 1){
						maps[y][0, x] = template.left[x, y];
						flags[y][0, x] = true;
					}
					if(template.right.GetLength(0) > 1){
						maps[y][WorldManager.dimension - 1, x] = template.right[x, y];
						flags[y][WorldManager.dimension - 1, x] = true;
					}

					if(template.forward.GetLength(0) > 1){
						maps[y][x, WorldManager.dimension - 1] = template.forward[x, y];
						flags[y][x, WorldManager.dimension - 1] = true;
					}
					if(template.back.GetLength(0) > 1){
						maps[y][x, 0] = template.back[x, y];
						flags[y][x, 0] = true;
					}

					if(template.bottom.GetLength(0) > 1){
						maps[0][x, y] = template.bottom[x, y];
						flags[0][x, y] = true;
					}
					if(template.top.GetLength(0) > 1){
						maps[WorldManager.dimension - 1][x, y] = template.top[x, y];
						flags[WorldManager.dimension - 1][x, y] = true;
					}
				}
			}
		}

		maps = RandomFillMap(maps, flags, chunkKey, randomAddition);

		for(int i = 0; i < smoothIterations; i++){
			maps = RecursiveSmoothMap(maps, flags, 0, 0, 0, smoothPercentage);
		}

		access = CheckAccessibility(maps);

		return maps;
	}

	public List<bool[,]> GenerateCellularAutomata(string chunkKey, CubeTemplate template, int smoothIterations, int randomAddition, out Accessible access){

		List<bool[,]> maps = new List<bool[,]>();

		for (int y = 0; y < WorldManager.dimension * 2; y ++) {
			maps.Add(new bool[WorldManager.dimension, WorldManager.dimension]);
		}

		if(!template.IsEmpty()){
			for(int x = 0; x < maps.Count; x++){
				for(int y = 0; y < maps.Count; y++){
					maps[y][0, x] = template.left[x, y];
					maps[y][maps.Count - 1, x] = template.right[x, y];

					maps[y][x, maps.Count - 1] = template.forward[x, y];
					maps[y][x, 0] = template.back[x, y];

					maps[0][x, y] = template.bottom[x, y];
					maps[maps.Count - 1][x, y] = template.top[x, y];
				}
			}

			randomAddition = (int)(randomAddition * 0.5f);
		}

		//maps = RandomFillMap(maps, chunkKey, WorldManager.dimension, randomAddition);

		for(int i = 0; i < smoothIterations; i++){
			maps = SmoothMap(maps, smoothPercentage);
		}

		access = CheckAccessibility(maps);

		return maps;
	}

	List<bool[,]> RandomFillMap(List<bool[,]> map, List<bool[,]> flags, string chunkKey, int randomFillPercent) {
		System.Random pseudoRandom = new System.Random(chunkKey.GetHashCode());

		for (int x = 0; x < WorldManager.dimension; x ++) {
			for (int y = 0; y < WorldManager.dimension; y ++) {
				for (int z = 0; z < WorldManager.dimension; z ++) {
					if(!flags[y] [x, z]){
						if(!map[y] [x, z]){
							map[y] [x, z] = pseudoRandom.Next (0, 100) < randomFillPercent;
						}
					}
				}
			}
		}

		return map;
	}

	List<bool[,]> RecursiveSmoothMap(List<bool[,]> map, List<bool[,]> flags, int x, int y, int z, float percentageLimitToFill) {
		float neighbourWallOnPercentage = GetSurroundingWallCount(map, x, y, z);

		flags[y] [x, z] = true;

		if (neighbourWallOnPercentage > (percentageLimitToFill)){
			map[y] [x, z] = true;

			if(x > 0){
				if(!flags[y] [x - 1, z]){
					map = RecursiveSmoothMap(map, flags, x - 1, y, z, percentageLimitToFill);
				}
			}
			if(x < WorldManager.dimension - 1){
				if(!flags[y] [x + 1, z]){
					map = RecursiveSmoothMap(map, flags, x + 1, y, z, percentageLimitToFill);
				}
			}
			if(y > 0){
				if(!flags[y - 1] [x, z]){
					map = RecursiveSmoothMap(map, flags, x, y - 1, z, percentageLimitToFill);
				}
			}
			if(y < WorldManager.dimension - 1){
				if(!flags[y + 1] [x, z]){
					map = RecursiveSmoothMap(map, flags, x, y + 1, z, percentageLimitToFill);
				}
			}
			if(z > 0){
				if(!flags[y] [x, z - 1]){
					map = RecursiveSmoothMap(map, flags, x, y, z - 1, percentageLimitToFill);
				}
			}
			if(z < WorldManager.dimension - 1){
				if(!flags[y] [x, z + 1]){
					map = RecursiveSmoothMap(map, flags, x, y, z + 1, percentageLimitToFill);
				}
			}

			for (int x1 = 0; x1 < WorldManager.dimension; x1 ++) {
				for (int y1 = 0; y1 < WorldManager.dimension; y1 ++) {
					for (int z1 = 0; z1 < WorldManager.dimension; z1 ++) {
						if(!flags[y1] [x1, z1] && map[y1] [x1, z1]){
							map = RecursiveSmoothMap(map, flags, x1, y1, z1, percentageLimitToFill);
						}
					}
				}
			}
		}else{
			map[y] [x, z] = false;
		}

		return map;
	}

	List<bool[,]> SmoothMap(List<bool[,]> map, float percentageLimitToFill) {
		for (int x = 0; x < WorldManager.dimension; x ++) {
			for (int y = 0; y < WorldManager.dimension; y ++) {
				for (int z = 0; z < WorldManager.dimension; z ++) {

					float neighbourWallOnPercentage = GetSurroundingWallCount(map, x, y, z);

					if (neighbourWallOnPercentage > (percentageLimitToFill)){
						map[y] [x, z] = true;
					}else if (neighbourWallOnPercentage < percentageLimitToFill){
						map[y] [x, z] = false;
					}
				}
			}
		}
		return map;
	}

	float GetSurroundingWallCount(List<bool[,]> map, int gridX, int gridY, int gridZ) {
		int wallCount = 0;
		int totalCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				for (int neighbourZ = gridZ - 1; neighbourZ <= gridZ + 1; neighbourZ ++) {
					totalCount++;

					if (neighbourX >= 0 && neighbourX < WorldManager.dimension && 
						neighbourY >= 0 && neighbourY < WorldManager.dimension && 
						neighbourZ >= 0 && neighbourZ < WorldManager.dimension) {

						if (neighbourX != gridX || neighbourY != gridY || neighbourZ != gridZ) {
							wallCount += map [neighbourY] [neighbourX, neighbourZ] ? 1 : 0;
						}
					}
					else {
						wallCount ++;
					}
				}
			}
		}

		return (float)wallCount/(float)totalCount;
	}

	private Accessible CheckAccessibility(List<bool[,]> maps){
		Accessible a = new Accessible(true, true, true, true, true, true);
		return a;
	}
}