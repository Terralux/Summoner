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
	public List<bool[,]> Generate (Direction dir, bool[,] slice, float randomAddition){
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

	public List<bool[,]> GenerateStartSurroundings(int randomAddition, int dimension, out Accessible access){
		List<bool[,]> maps = new List<bool[,]>();

		for(int x = 0; x < dimension; x++){
			maps.Add(SliceFill(dimension));
		}

		maps.Add(SliceNoise(dimension, randomAddition));

		while(maps.Count < dimension * 2){
			maps.Add(SliceBlur(dimension, randomAddition, maps[maps.Count - 1]));
		}

		access = new Accessible(true, false, true, true, true, true);

		return maps;
	}

	public List<bool[,]> GenerateStartCubeGrid (int randomAddition, int dimension, out Accessible access){
		List<bool[,]> maps = new List<bool[,]>();

		for(int x = 0; x < dimension; x++){
			maps.Add(SliceFill(dimension));
		}

		//maps = OptimizeSlices(maps);

		maps.Add(SliceNoise(dimension, randomAddition));

		while(maps.Count < dimension * 2){
			maps.Add(SliceEmpty(dimension));
		}

		access = new Accessible(true, false, true, true, true, true);
		return maps;
	}

	public bool[,] SliceFill(int dimension){
		bool[,] maps = new bool[dimension, dimension];

		for(int y = 0; y < dimension; y++){
			for(int z = 0; z < dimension; z++){
				maps [y, z] = true;
			}
		}

		return maps;
	}

	public bool[,] SliceEmpty(int dimension){
		return new bool[dimension, dimension];
	}

	public bool[,] SliceNoise(int dimension, float fillRatio){
		bool[,] maps = new bool[dimension, dimension];

		for(int y = 0; y < dimension; y++){
			for(int z = 0; z < dimension; z++){
				if(Random.Range (0f, 100f) < fillRatio){
					maps [y, z] = true;
				}else{
					maps [y, z] = false;
				}
			}
		}

		return maps;
	}

	public bool[,] SliceNoise(int dimension, int fillRatio, int safeZoneRadius){
		bool[,] maps = new bool[dimension, dimension];

		int center = (int)((float)dimension/2f);

		for(int y = 0; y < dimension; y++){
			for(int z = 0; z < dimension; z++){
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

	public bool[,] SliceBlur(int dimension, int fillRatio, bool[,] previous){
		bool[,] maps = new bool[dimension, dimension];

		for(int y = 0; y < dimension; y++){
			for(int z = 0; z < dimension; z++){
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

	public List<bool[,]> GenerateCellularAutomata(string chunkKey, CubeTemplate template, int smoothIterations, int randomAddition, int dimension, out Accessible access){

		List<bool[,]> maps = new List<bool[,]>();

		for (int y = 0; y < dimension * 2; y ++) {
			maps.Add(new bool[dimension, dimension]);
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

		maps = RandomFillMap(maps, chunkKey, dimension, randomAddition);

		for(int i = 0; i < smoothIterations; i++){
			maps = SmoothMap(maps, dimension, 0.58f);
		}

		access = CheckAccessibility(maps);

		return maps;
	}

	List<bool[,]> RandomFillMap(List<bool[,]> map, string chunkKey, int dimension, int randomFillPercent) {
		Debug.Log(chunkKey);

		System.Random pseudoRandom = new System.Random(chunkKey.GetHashCode());

		for (int x = 0; x < dimension; x ++) {
			for (int y = 0; y < dimension; y ++) {
				for (int z = 0; z < dimension; z ++) {
					if(!map[y] [x, z]){
						map[y] [x, z] = pseudoRandom.Next (0, 100) < randomFillPercent;
					}
				}
			}
		}

		return map;
	}

	List<bool[,]> SmoothMap(List<bool[,]> map, int dimension, float percentageLimitToFill) {
		for (int x = 0; x < dimension; x ++) {
			for (int y = 0; y < dimension; y ++) {
				for (int z = 0; z < dimension; z ++) {

				float neighbourWallOnPercentage = GetSurroundingWallCount(map, x, y, z, dimension);

				if (neighbourWallOnPercentage > percentageLimitToFill)
					map[y] [x, z] = true;
				else if (neighbourWallOnPercentage < percentageLimitToFill)
					map[y] [x, z] = false;

				}
			}
		}
		return map;
	}

	float GetSurroundingWallCount(List<bool[,]> map, int gridX, int gridY, int gridZ, int dimension) {
		int wallCount = 0;
		int totalCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				for (int neighbourZ = gridZ - 1; neighbourZ <= gridZ + 1; neighbourZ ++) {
					totalCount++;

					if (neighbourX >= 0 && neighbourX < dimension && 
						neighbourY >= 0 && neighbourY < dimension && 
						neighbourZ >= 0 && neighbourZ < dimension) {

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