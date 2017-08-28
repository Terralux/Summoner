using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkType {

	public SpawnPercentagePair[] neighbourPairs;
	public SpawnPercentagePair[] topPairs;
	public SpawnPercentagePair[] bottomPairs;

	public ChunkType EvaluateChunkType (Direction dir){
		switch(dir){
		case Direction.top:
			return CalculateType(topPairs);
		case Direction.bottom:
			return CalculateType(bottomPairs);
		case Direction.back:
		case Direction.forward:
		case Direction.left:
		case Direction.right:
			return CalculateType(neighbourPairs);
		}
		return this;
	}

	public ChunkType CalculateType(SpawnPercentagePair[] pairs){
		float total = 0f;

		foreach(SpawnPercentagePair spp in neighbourPairs){
			total += spp.percentage;
		}

		float random = Random.Range(0f, total);
		float count = 0f;

		foreach(SpawnPercentagePair spp in neighbourPairs){
			count += spp.percentage;
			if(random >= count){
				return spp.type;
			}
		}
		return pairs[pairs.Length - 1].type;
	}
}

public class Flat : ChunkType {
	public ChunkType EvaluateType (Direction dir){
		neighbourPairs = new SpawnPercentagePair[]{ 
			new SpawnPercentagePair(new Flat(), 50f),
			new SpawnPercentagePair(new Slope(), 50f),
			new SpawnPercentagePair(new Wall(), 50f)
		};

		topPairs = new SpawnPercentagePair[]{ 
			new SpawnPercentagePair(new Sky(), 50f)
		};

		bottomPairs = new SpawnPercentagePair[]{ 
			new SpawnPercentagePair(new Wall(), 50f),
			new SpawnPercentagePair(new Cave(), 50f)
		};
		return EvaluateChunkType(dir);
	}
}

public class Empty : ChunkType {
	
}

public class Sky : ChunkType {
	
}

public class Wall : ChunkType {
	
}

public class Slope : ChunkType {
	
}

public class Cave : ChunkType {
	
}

public struct SpawnPercentagePair{
	public ChunkType type;
	public float percentage;

	public SpawnPercentagePair(ChunkType type, float percentage){
		this.type = type;
		this.percentage = percentage;
	}
}