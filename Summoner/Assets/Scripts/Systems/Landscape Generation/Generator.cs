using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
	XPositive,
	XNegative,
	YPositive,
	YNegative,
	ZPositive,
	ZNegative
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
		/*
		switch(dir){
		case Direction.XPositive:
			
			break;
		case Direction.XNegative:
			break;
		case Direction.YPositive:
			break;
		case Direction.YNegative:
			break;
		case Direction.ZPositive:
			break;
		case Direction.ZNegative:
			break;
		}
		*/
	}

	public List<bool[,]> GenerateStartCubeGrid (int randomAddition, int dimension){
		List<bool[,]> maps = new List<bool[,]>();

		for(int x = 0; x < dimension; x++){
			maps.Add(SliceFill(dimension));
		}

		maps = OptimizeSlices(maps);

		maps.Add(SliceNoise(dimension, randomAddition, 5));

		while(maps.Count < dimension * 2){
			maps.Add(SliceEmpty(dimension));
		}

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
}