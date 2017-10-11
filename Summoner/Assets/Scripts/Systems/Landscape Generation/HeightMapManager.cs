using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMapManager : MonoBehaviour {

	public Texture2D heightMap;

	private static int[,] heightMapValues;
	private static int width;
	private static int height;

	[Range(0,255)]
	public float heightLimit = 10;

	[Range(0,500)]
	public int xOffset = 10;
	[Range(0,500)]
	public int zOffset = 10;

	private static int xStatic;
	private static int zStatic;

	void Awake(){
		height = heightMap.height;
		width = heightMap.width;

		heightMapValues = new int[width, height];

		Color[] colors = heightMap.GetPixels ();

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				heightMapValues [x, y] = (int) (heightLimit * (colors [x * width + y].r + colors [x * width + y].g + colors [x * width + y].b));
			}
		}

		xStatic = xOffset;
		zStatic = zOffset;
	}

	public static bool GetActiveState(int x, int y, int z){
		return heightMapValues [Mathf.Abs ((x + xStatic)) % width, Mathf.Abs ((z + zStatic)) % height] > y;
	}

	public static List<List<HeightPairings>> GetHeightPairings(){
		List<List<HeightPairings>> heightPairings = new List<List<HeightPairings>> ();

		//TODO

		return heightPairings;
	}

	public static List<List<int[,]>> GetSegmentedHeightMap(){
		List<List<int[,]>> segmentedHeightMaps = new List<List<int[,]>> ();

		for (int xOffset = 0; xOffset < width / WorldManager.dimension; xOffset++) {
			for (int zOffset = 0; zOffset < height / WorldManager.dimension; zOffset++) {
				segmentedHeightMaps.Add (new List<int[,]> ());
				segmentedHeightMaps [xOffset] [zOffset] = new int[width, height];

				for (int x = 0; x < WorldManager.dimension; x++) {
					for (int z = 0; z < WorldManager.dimension; z++) {
						if (x + xOffset * WorldManager.dimension < width && z + zOffset * WorldManager.dimension < height) {
							segmentedHeightMaps [xOffset] [zOffset] [x, z] = heightMapValues [x + xOffset * WorldManager.dimension, z + zOffset * WorldManager.dimension];
						}
					}
				}
			}
		}

		return segmentedHeightMaps;
	}

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			Awake ();
		}
	}
}

public struct HeightPairings{
	public int maxHeight;
	public int minHeight;

	public HeightPairings(int min, int max){
		maxHeight = max;
		minHeight = min;
	}
}