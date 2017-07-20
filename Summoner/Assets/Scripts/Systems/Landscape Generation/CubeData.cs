using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public struct SlicePair {
	public bool[,] top;
	public bool[,] bottom;

	public SlicePair(float ratio){
		this.top = new bool[2,2];
		this.bottom = new bool[2,2];

		for(int x = 0; x < 2; x++){
			for(int y = 0; y < 2; y++){
				top[x,y] = Random.value * 100f < ratio;
				bottom[x,y] = Random.value * 100f < ratio;
			}
		}
	}

	public SlicePair(bool[,] top, bool[,] bottom){
		this.top = new bool[2,2];
		this.bottom = new bool[2,2];

		for(int x = 0; x < 2; x++){
			for(int y = 0; y < 2; y++){
				this.top[x,y] = top[x,y];
				this.bottom[x,y] = bottom[x,y];
			}
		}
	}
}
*/

public struct Chunk {
	public Slice[] slices;

	public Chunk(float squareSize) {
		throw new System.NotImplementedException();
	}

	public Chunk(List<bool[,]> maps, float squareSize) {

		int nodeCount = maps[0].GetLength(0);

		/*
		for(int y = 0; y < maps.Count; y ++) {
			for (int x = 0; x < nodeCountX; x ++) {
				for (int z = 0; z < nodeCountZ; z ++) {
					Vector3 pos = new Vector3(-mapWidth/2 + x * squareSize + squareSize/2, -maps.Count/2 + y * squareSize + squareSize/2, -mapDepth/2 + z * squareSize + squareSize/2);

					if(maps[y][x,z]){
						if(y - 1 > 0 && y + 1 < maps.Count && x - 1 > 0 && x + 1 < nodeCountX && z - 1 > 0 && z + 1 < nodeCountZ){
							if(maps[y - 1][x,z] && maps[y + 1][x,z] && maps[y][x - 1,z] && maps[y][x + 1,z] && maps[y][x,z - 1] && maps[y][x,z + 1]){
								controlNodes[y, x, z] = new ControlNode(pos, false, squareSize);
							}else{
								controlNodes[y, x, z] = new ControlNode(pos, true, squareSize);
							}
						}else{
							controlNodes[y, x, z] = new ControlNode(pos, true, squareSize);
						}
					}else{
						controlNodes[y, x, z] = new ControlNode(pos, maps[y][x, z], squareSize);
					}
				}
			}
		}
		*/

		slices = new Slice[nodeCount];
		//slices [0] = new Slice(maps[0], squareSize);

		for(int y = 1; y < nodeCount * 2; y += 2) {
			//float height = -(float)maps.Count/2 + y * squareSize + squareSize/2;
			if(y > 1){
				slices [(y - 1)/2] = new Slice(maps[y - 1], maps[y], squareSize, (y - 1)/2);
			}else{
				slices [0] = new Slice(maps[y - 1], maps[y], squareSize, y - 1);
			}
			//slices[y] = new Slice(maps [y], squareSize, height, slices [y - 1]);
		}
	}
}

public struct Slice {
	public Cube[,] cubes;

	public Slice(bool[,] bottom, bool[,] top, float squareSize, int iteration){
		int nodeCount = bottom.GetLength(0);

		float mapWidth = nodeCount * squareSize;
		float mapDepth = nodeCount * squareSize;

		ControlNode [,] controlNodesTop = new ControlNode [nodeCount, nodeCount];
		ControlNode [,] controlNodesBottom = new ControlNode [nodeCount, nodeCount];

		for (int x = 0; x < nodeCount; x ++) {
			for (int z = 0; z < nodeCount; z ++) {
				Vector3 pos = new Vector3(-mapWidth/2 + x * squareSize + squareSize/2, 0f, -mapDepth/2 + z * squareSize + squareSize/2);
				controlNodesTop [x, z] = new ControlNode (pos + Vector3.up * (-(float)nodeCount/2 + iteration * squareSize + squareSize/2), top[x, z], squareSize);
				controlNodesBottom [x, z] = new ControlNode (pos + Vector3.up * (-(float)nodeCount/2 + (iteration - 1f) * squareSize + squareSize/2), bottom[x, z], squareSize);
			}
		}

		cubes = new Cube[nodeCount - 1, nodeCount - 1];

		for (int x = 0; x < nodeCount - 1; x ++) {
			for (int z = 0; z < nodeCount - 1; z ++) {
				cubes[x, z] = new Cube(controlNodesTop[x, z + 1], controlNodesTop[x + 1, z + 1], controlNodesTop[x + 1, z], controlNodesTop[x, z],
					controlNodesBottom[x, z + 1], controlNodesBottom[x + 1, z + 1], controlNodesBottom[x + 1, z], controlNodesBottom[x, z]);
			}
		}
	}
}

public struct Cube {
	public Square topSquare, bottomSquare;
	public Node middleForwardLeft, middleForwardRight, middleBackwardRight, middleBackwardLeft;
	public int configuration;
	public int controlNodesActive;

	public Cube (ControlNode _topHalfTopLeft, ControlNode _topHalfTopRight, ControlNode _topHalfBottomRight, ControlNode _topHalfBottomLeft,
		ControlNode _bottomHalfTopLeft, ControlNode _bottomHalfTopRight, ControlNode _bottomHalfBottomRight, ControlNode _bottomHalfBottomLeft) {

		topSquare = new Square(_topHalfTopLeft, _topHalfTopRight, _topHalfBottomRight, _topHalfBottomLeft);
		bottomSquare = new Square(_bottomHalfTopLeft, _bottomHalfTopRight, _bottomHalfBottomRight, _bottomHalfBottomLeft);

		middleForwardLeft = bottomSquare.forwardLeft.above;
		middleForwardRight = bottomSquare.forwardRight.above;
		middleBackwardRight = bottomSquare.backwardRight.above;
		middleBackwardLeft = bottomSquare.backwardLeft.above;

		configuration = 0;
		controlNodesActive = 0;

		if (bottomSquare.backwardLeft.active){
			configuration += 128;
			controlNodesActive++;
		}
		if (bottomSquare.backwardRight.active){
			configuration += 64;
			controlNodesActive++;
		}
		if (bottomSquare.forwardRight.active){
			configuration += 32;
			controlNodesActive++;
		}
		if (bottomSquare.forwardLeft.active){
			configuration += 16;
			controlNodesActive++;
		}

		if (topSquare.backwardLeft.active){
			configuration += 8;
			controlNodesActive++;
		}
		if (topSquare.backwardRight.active){
			configuration += 4;
			controlNodesActive++;
		}
		if (topSquare.forwardRight.active){
			configuration += 2;
			controlNodesActive++;
		}
		if (topSquare.forwardLeft.active){
			configuration += 1;
			controlNodesActive++;
		}
	}
}

public struct Square {
	public ControlNode forwardLeft, forwardRight, backwardRight, backwardLeft;
	public Node centreForward, centreRight, centreBackward, centreLeft;

	public Square (ControlNode _forwardLeft, ControlNode _forwardRight, ControlNode _backwardRight, ControlNode _backwardLeft) {
		forwardLeft = _forwardLeft;
		forwardRight = _forwardRight;
		backwardRight = _backwardRight;
		backwardLeft = _backwardLeft;

		centreForward = forwardLeft.right;
		centreRight = backwardRight.forward;
		centreBackward = backwardLeft.right;
		centreLeft = backwardLeft.forward;
	}
}

public class Node {
	public Vector3 position;
	public int vertexIndex;

	public Node(Vector3 _pos) {
		position = _pos;
		vertexIndex = -1;
	}
}

public class ControlNode : Node {
	public bool active;
	public Node above, right, forward;

	public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos) {
		active = _active;

		above = new Node(position + Vector3.up * squareSize/2f);
		right = new Node(position + Vector3.right * squareSize/2f);
		forward = new Node(position + Vector3.forward * squareSize/2f);
	}
}