using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidGenerator {

	private static int minRange = 3;
	private static int maxRange = 20;

	public static Vector3 GenerateLiquidSource(Chunk chunk){
		bool[,] flags = new bool[chunk.slices.GetLength(0), chunk.slices.GetLength(0)];

		for(int y = 1; y < chunk.slices.GetLength(0) - 2; y++){
			for(int x = 1; x < chunk.slices.GetLength(0) - 2; x++){
				for(int z = 1; z < chunk.slices.GetLength(0) - 2; z++){
					if(!chunk.slices[y].cubes[x, z].topSquare.forwardLeft.active){
						if(chunk.slices[y - 1].cubes[x, z].topSquare.forwardLeft.active){
							int floodSteps = CountPotentialLiquidTravelDistance(x, z, chunk.slices[y], flags);

							if(floodSteps > minRange && floodSteps < maxRange){
								chunk.slices[y].cubes[x, z].topSquare.forwardLeft.isLiquidSource = true;
								return new Vector3(x, y, z);
							}
						}
					}
				}
			}
		}

		return Vector3.zero;
	}

	private static int CountPotentialLiquidTravelDistance(int x, int z, Slice slice, bool[,] flags){
		flags[x, z] = true;

		int currentSteps = 0;

		if(slice.cubes.GetLength(0) - 2 > x){
			if (!slice.cubes [x + 1, z].topSquare.forwardLeft.active) {
				if (!flags [x + 1, z]) {
					currentSteps += CountPotentialLiquidTravelDistance (x + 1, z, slice, flags);
				}
			}
		}
		if(x > 0){
			if (!slice.cubes [x - 1, z].topSquare.forwardLeft.active) {
				if (!flags [x - 1, z]) {
					currentSteps += CountPotentialLiquidTravelDistance (x - 1, z, slice, flags);
				}
			}
		}
		if(z < slice.cubes.GetLength(0) - 2){
			if (!slice.cubes [x, z + 1].topSquare.forwardLeft.active) {
				if (!flags [x, z + 1]) {
					currentSteps += CountPotentialLiquidTravelDistance (x, z + 1, slice, flags);
				}
			}
		}
		if(z > 0){
			if (!slice.cubes [x, z - 1].topSquare.forwardLeft.active) {
				if (!flags [x, z - 1]) {
					currentSteps += CountPotentialLiquidTravelDistance (x, z - 1, slice, flags);
				}
			}
		}

		return currentSteps + 1;
	}

	//Method for creating a lake:
	//When potential for a liquid source is evaaluated and confirmed, Fill out the surrounding available area
	//with liquid, to form a lake
	/*public Vector3 GenerateLake(Chunk chunk, Slice slice){
		for (int x = 0; x < Chunk.slices.Length - 1; x++) {
			for (int y = 0; y < Chunk.slices.Length - 1; y++) {
				for (int z = 0; z < Chunk.slices.Length - 1; z++) {
					if (Chunk.slices [y].cubes [x, z].topSquare.forwardLeft.isLiquidSource) {
						if(slice.cubes.GetLength(0) - 2 > x){
							if (!slice.cubes [x + 1, z].topSquare.forwardLeft.active) {
								//liquid
							}  
							if (x > 0) {
								if (!slice.cubes [x - 1, z].topSquare.forwardLeft.active) {
									//liquid
								}
							}
							if (z < slice.cubes.GetLength (0) - 2) {
								if (!slice.cubes [x, z + 1].topSquare.forwardLeft.active) {
									//liquid
								}
							}
							if (z > 0) {
								if (!slice.cubes [x, z - 1].topSquare.forwardLeft.active) {
									//liquid
								}
							}
					}
				}
			}
		}
	}*/
}