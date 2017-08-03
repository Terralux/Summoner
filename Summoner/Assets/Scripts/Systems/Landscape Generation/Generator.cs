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

	/*
	public List<bool[,]> PerlinNoise(){
		
	}

	public float Lerp(float a0, float a1, float w) {
		return (1f - w) * a0 + w * a1;
	}

	public void DotGridGradient(int ix, int iy, float x, float y){
		//extern float Gradient[IYMAX][IXMAX][2];

		float dx = x - (float)ix;
		float dy = y - (float)iy;

		return (dx * Gradient[iy][ix][0] + dy * Gradient[iy][ix][1]);
	}

	public float Perlin(float x, float y){
		int x0 = Mathf.Floor(x);
		int x1 = x0 + 1;
		int y0 = Mathf.Floor(y);
		int y1 = y0 + 1;

		float sx = x - (float) x0;
		float sy = y - (float) y0;

		float n0, n1, ix0, ix1, value;

		n0 = DotGridGradient(x0, y0, x, y);
		n1 = DotGridGradient(x1, y0, x, y);
		ix0 = Lerp(n0, n1, sx);

		n0 = DotGridGradient(x0, y1, x, y);
		n1 = DotGridGradient(x1, y1, x, y);
		ix1 = Lerp(n0, n1, sx);

		value = Lerp(ix0, ix1, sy);

		return value;
	}
	*/
}