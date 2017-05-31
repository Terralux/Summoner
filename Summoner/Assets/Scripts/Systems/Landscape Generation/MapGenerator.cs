using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapGenerator {

	public int width;
	public int depth;
	public int height;

	public string seed;
	public bool useRandomSeed;

	[Range(0,100)]
	public int randomFillPercent;

	int[,] map;

	public void Create(MeshFilter target) {
		GenerateMap(target);
	}

	void Update(MeshFilter target) {
		if (Input.GetMouseButtonDown(0)) {
			GenerateMap(target);
			//UpdateCollision (target);
		}
	}

	/*
	void UpdateCollision(MeshFilter target){
		if (target.GetComponent<MeshCollider> () == null) {
			target.AddComponent<MeshCollider> ();
		}
		target.GetComponent<MeshCollider>().sharedMesh = target.sharedMesh;
	}
	*/

	void GenerateMap(MeshFilter target) {
		List<int[,]> maps = new List<int[,]> ();

		for(int i = 0; i < height; i++){
			map = new int[width, depth];
			RandomFillMap();

			for (int s = 0; s < 5; s++) {
				SmoothMap();
			}
			maps.Add (map);
		}

		MeshGenerator meshGen = new MeshGenerator ();

		meshGen.GenerateMesh (maps, 1, target);
	}


	void RandomFillMap() {
		if (useRandomSeed) {
			seed = System.DateTime.Now.Millisecond.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < depth; y ++) {
				if (x == 0 || x == width-1 || y == 0 || y == depth -1) {
					map[x,y] = 1;
				}
				else {
					map[x,y] = (pseudoRandom.Next(0,100) < randomFillPercent)? 1: 0;
				}
			}
		}
	}

	void SmoothMap() {
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < depth; y ++) {
				int neighbourWallTiles = GetSurroundingWallCount(x,y);

				if (neighbourWallTiles > 4)
					map[x,y] = 1;
				else if (neighbourWallTiles < 4)
					map[x,y] = 0;

			}
		}
	}

	int GetSurroundingWallCount(int gridX, int gridY) {
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < depth) {
					if (neighbourX != gridX || neighbourY != gridY) {
						wallCount += map[neighbourX,neighbourY];
					}
				}
				else {
					wallCount ++;
				}
			}
		}

		return wallCount;
	}

	/*
	void OnDrawGizmos() {
		if (map != null) {
			for (int x = 0; x < width; x ++) {
				for (int y = 0; y < depth; y ++) {
					Gizmos.color = (map[x,y] == 1)?Color.black:Color.white;
					Vector3 pos = new Vector3(-width/2 + x + .5f,0, -depth/2 + y+.5f);
					Gizmos.DrawCube(pos,Vector3.one);
				}
			}
		}
	}
	*/
}