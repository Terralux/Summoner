﻿using System.Collections;
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

					if(totalProbability > Random.Range(0, 100f - randomAddition)){
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
}