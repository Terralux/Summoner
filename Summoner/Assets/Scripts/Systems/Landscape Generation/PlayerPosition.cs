using UnityEngine;

[System.Serializable]
public struct PlayerPosition{
	int x;
	int y;
	int z;

	public Vector3 pos{
		get{
			return new Vector3(x, y, z);
		}
	}

	public PlayerPosition(int x, int y, int z){
		this.x = x;
		this.y = y;
		this.z = z;
	}
}