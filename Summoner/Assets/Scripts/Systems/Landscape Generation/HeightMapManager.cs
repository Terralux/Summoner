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

	public GameObject chunkManagerPrefab;
	public static GameObject staticChunkManagerPrefab;

	[Range(0.1f,1f)]
	public float portionOfTextureGenerated;
	private static float staticPortionOfTextureGenerated;

	void Awake(){
		staticChunkManagerPrefab = chunkManagerPrefab;
		staticPortionOfTextureGenerated = portionOfTextureGenerated;

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
		return heightMapValues [Mathf.Abs ((z + zStatic)) % height, Mathf.Abs ((x + xStatic)) % width] > y;
	}

	public static List<List<HeightPairings>> GetHeightPairings(){
		List<List<HeightPairings>> heightPairings = new List<List<HeightPairings>> ();



		//TODO

		return heightPairings;
	}

	public static HeightPairings GetHeightPairing(int[,] heightMap){
		int min = int.MaxValue;
		int max = int.MinValue;

		for (int x = 0; x < heightMap.GetLength (0); x++) {
			for (int z = 0; z < heightMap.GetLength (1); z++) {
				if (heightMap [x, z] < min) {
					min = heightMap [x, z];
				} else if(heightMap[x,z] > max){
					max = heightMap [x, z];
				}
			}
		}

		return new HeightPairings (min, max);
	}

	public static void SegmentHeightMap(){
		List<List<int[,]>> segmentedHeightMaps = GetSegmentedHeightMap();

		HeightPairings heightPair;

		for (int xOffset = 0; xOffset < (width * staticPortionOfTextureGenerated) / (WorldManager.dimension - 1); xOffset++) {
			for (int zOffset = 0; zOffset < (height * staticPortionOfTextureGenerated) / (WorldManager.dimension - 1); zOffset++) {
				heightPair = GetHeightPairing (segmentedHeightMaps [xOffset] [zOffset]);

				int currentY = heightPair.maxHeight;

				while (currentY >= heightPair.minHeight) {
					Vector3 position = new Vector3 (
						(WorldManager.dimension - 1) * xOffset,
						((int)(currentY / (WorldManager.dimension - 1)) * (WorldManager.dimension - 1)),
						(WorldManager.dimension - 1) * zOffset
					);

					GameObject go = Instantiate (staticChunkManagerPrefab, position, Quaternion.identity);
					WorldManager.RegisterChunk ((int)(position.x / (WorldManager.dimension - 1)), (int)(position.y / (WorldManager.dimension - 1)), (int)(position.z / (WorldManager.dimension - 1)), go.GetComponent<ChunkManager> ());

					currentY -= WorldManager.dimension - 1;
				}
			}
		}
	}

	public static List<List<int[,]>> GetSegmentedHeightMap(){
		List<List<int[,]>> segmentedHeightMaps = new List<List<int[,]>> ();

		for (int xOffset = 0; xOffset < width / (WorldManager.dimension - 1); xOffset++) {
			segmentedHeightMaps.Add (new List<int[,]> ());
			for (int zOffset = 0; zOffset < height / (WorldManager.dimension - 1); zOffset++) {
				segmentedHeightMaps [xOffset].Add (new int[width, height]);

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