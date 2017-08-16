using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHeightGenerator : MonoBehaviour {
	
	public int size;
	public float[,] dataArray;
	public float h = 0.5f;
	private float height = 0.5f;

	[Range(0f,0.8f)]
	public float smoothingLimit = 0.2f;

	private Terrain terrain;

	void Awake(){
		terrain = GetComponent<Terrain>();
		height = h;
	}

	void Update() {
		if ( Input.GetKeyDown( KeyCode.Space ) ){
			DiamondSquareDataArray();
		}
	}

	void DiamondSquareDataArray() 
	{
		h = height;
		// declare the data array
		dataArray = new float[ size, size ];

		// set the 4 corners
		dataArray[ 0, 0 ] = 1;
		dataArray[ size - 1, 0 ] = 1;
		dataArray[ 0, size - 1 ] = 1;
		dataArray[ size - 1, size - 1 ] = 1;

		float val = 0f;
		float rnd = 0f;

		int sideLength = 0;
		int x = 0;
		int y = 0;

		int halfSide = 0;

		for ( sideLength = size - 1; sideLength >= 2; sideLength /= 2 ){
			halfSide = sideLength / 2;

			// square values
			for ( x = 0; x < size - 1; x += sideLength )
			{
				for ( y = 0; y < size - 1; y += sideLength )
				{
					val = dataArray[ x, y ];
					val += dataArray[ x + sideLength, y ];
					val += dataArray[ x, y + sideLength ];
					val += dataArray[ x + sideLength, y + sideLength ];

					val /= 4.0f;

					// add random
					rnd = ( Random.value * 2.0f * h ) - h;
					val = Mathf.Clamp01( val + rnd );

					dataArray[ x + halfSide, y + halfSide ] = val;
				}
			}

			// diamond values
			for ( x = 0; x < size - 1; x += halfSide )
			{
				for ( y = ( x + halfSide ) % sideLength; y < size - 1; y += sideLength )
				{
					val = dataArray[ ( x - halfSide + size - 1 ) % ( size - 1 ), y ];
					val += dataArray[ ( x + halfSide ) % ( size - 1 ), y ];
					val += dataArray[ x, ( y + halfSide ) % ( size - 1 ) ];
					val += dataArray[ x, ( y - halfSide + size - 1 ) % ( size - 1 ) ];

					val /= 4.0f;

					// add random
					rnd = ( Random.value * 2.0f * h ) - h;
					val = Mathf.Clamp01( val + rnd );

					dataArray[ x, y ] = val;

					if ( x == 0 ) dataArray[ size - 1, y ] = val;
					if ( y == 0 ) dataArray[ x, size - 1 ] = val;
				}
			}

			h /= 2.0f; // cannot include this in for loop (dont know how in uJS)
		}

		OrganizeMap();
	}

	void OrganizeMap(){
		bool[,] flags = new bool[size - 1, size - 1];
		dataArray = RecursiveSmoothMap(flags, 0, 0, size - 1, 30f);

		GenerateTerrain();

		Debug.Log( "DiamondSquareDataArray completed" );
	}

	float[,] RecursiveSmoothMap(bool[,] flags, int x, int y, int dimension, float percentageLimitToFill) {
		float neighbourWallOnPercentage = GetSurroundingWallCount(x, y, dimension);

		flags[x, y] = true;

		if (neighbourWallOnPercentage > (percentageLimitToFill)){
			dataArray[x, y] = neighbourWallOnPercentage;

			if(x > 0){
				if(!flags[x - 1, y]){
					dataArray = RecursiveSmoothMap(flags, x - 1, y, dimension, percentageLimitToFill);
				}
			}
			if(x < dimension - 1){
				if(!flags[x + 1, y]){
					dataArray = RecursiveSmoothMap(flags, x + 1, y, dimension, percentageLimitToFill);
				}
			}
			if(y > 0){
				if(!flags[x, y - 1]){
					dataArray = RecursiveSmoothMap(flags, x, y - 1, dimension, percentageLimitToFill);
				}
			}
			if(y < dimension - 1){
				if(!flags[x, y + 1]){
					dataArray = RecursiveSmoothMap(flags, x, y + 1, dimension, percentageLimitToFill);
				}
			}

			for (int x1 = 0; x1 < dimension; x1 ++) {
				for (int y1 = 0; y1 < dimension; y1 ++) {
					if(!flags[x1, y1]){
						dataArray = RecursiveSmoothMap(flags, x1, y1, dimension, percentageLimitToFill);
					}
				}
			}
		}

		return dataArray;
	}

	float GetSurroundingWallCount(int gridX, int gridY, int dimension) {
		int wallCount = 0;
		int totalCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				totalCount++;

				if (neighbourX >= 0 && neighbourX < dimension && neighbourY >= 0 && neighbourY < dimension) {

					if (neighbourX != gridX || neighbourY != gridY) {
						if(Mathf.Abs(dataArray[gridX, gridY] - dataArray[neighbourX, neighbourY]) < smoothingLimit){
							wallCount++;
						}
					}
				}
				else {
					wallCount ++;
				}
			}
		}

		return (float)wallCount/(float)totalCount;
	}

	void GenerateTerrain(){
		if(!terrain){
			return;
		}
		if(terrain.terrainData.heightmapResolution != size){
			terrain.terrainData.heightmapResolution = size;
		}

		terrain.terrainData.SetHeights(0, 0, dataArray);
	}
}