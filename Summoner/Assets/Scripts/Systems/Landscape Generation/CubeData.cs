﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Chunk {
	public Slice[] slices;

	public Chunk(float squareSize) {
		throw new System.NotImplementedException();
	}

	public Chunk(List<bool[,]> maps, float squareSize) {

		int nodeCount = maps[0].GetLength(0);

		slices = new Slice[nodeCount];

		for(int y = 1; y < nodeCount * 2; y += 2) {
			if(y > 1){
				slices [(y - 1)/2] = new Slice(maps[y - 1], maps[y], squareSize, (y - 1)/2);
			}else{
				slices [0] = new Slice(maps[y - 1], maps[y], squareSize, y - 1);
			}
		}

		for(int y = 1; y < slices.Length - 1; y++){
			for(int x = 1; x < slices[y].cubes.GetLength(0) - 1; x++){
				for(int z = 1; z < slices[y].cubes.GetLength(0) - 1; z++){
					if(slices[y + 1].cubes[x, z].SideIsActive(Direction.bottom) &&
						slices[y - 1].cubes[x, z].SideIsActive(Direction.top) &&
						slices[y].cubes[x + 1, z].SideIsActive(Direction.left) &&
						slices[y].cubes[x - 1, z].SideIsActive(Direction.right) &&
						slices[y].cubes[x, z + 1].SideIsActive(Direction.back) &&
						slices[y].cubes[x, z - 1].SideIsActive(Direction.forward))
					{
						slices[y].cubes[x, z].isBurried = true;
					}				
				}
			}
		}

		/*
		for(int i = 1; i < slices.Length - 1; i++){
			for(int j = 1; j < slices[i].cubes.GetLength(0) - 1; j++){
				for(int k = 1; k < slices[i].cubes.GetLength(0) - 1; k++){
					if(slices[i - 1].cubes[j, k])
				}
			}
		}
		*/
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

		for (int x = 0; x < nodeCount; x++) {
			for (int z = 0; z < nodeCount; z++) {
				Vector3 pos = new Vector3(-mapWidth/2 + x * squareSize + squareSize/2, 0f, -mapDepth/2 + z * squareSize + squareSize/2);
				controlNodesTop [x, z] = new ControlNode (pos + Vector3.up * (-(float)nodeCount/2 + iteration * squareSize + squareSize/2), top[x, z], squareSize);
				controlNodesBottom [x, z] = new ControlNode (pos + Vector3.up * (-(float)nodeCount/2 + (iteration - 1f) * squareSize + squareSize/2), bottom[x, z], squareSize);
			}
		}

		cubes = new Cube[nodeCount - 1, nodeCount - 1];

		for (int x = 0; x < nodeCount - 1; x++) {
			for (int z = 0; z < nodeCount - 1; z++) {
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
	public bool isBurried;

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

		this.isBurried = false;
	}

	public bool IsEmpty(){
		if(controlNodesActive < 1){
			return true;
		}
		return false;
	}

	public bool ContainsAndIsActive(Node n1, Node n2, Node n3){
		//Contains and is active
		if(n1 as ControlNode != null){
			if(topSquare.IsControlNodeActive(n1 as ControlNode) || bottomSquare.IsControlNodeActive(n1 as ControlNode)){
				return true;
			}
		}
		if(n2 as ControlNode != null){
			if(topSquare.IsControlNodeActive(n2 as ControlNode) || bottomSquare.IsControlNodeActive(n2 as ControlNode)){
				return true;
			}
		}
		if(n3 as ControlNode != null){
			if(topSquare.IsControlNodeActive(n3 as ControlNode) || bottomSquare.IsControlNodeActive(n3 as ControlNode)){
				return true;
			}
		}
		return false;
	}

	public bool SideIsActive(Direction dir){
		switch(dir){
		case Direction.top:
			return (topSquare.forwardLeft.active && topSquare.forwardRight.active && topSquare.backwardRight.active && topSquare.backwardLeft.active);
		case Direction.bottom:
			return (bottomSquare.forwardLeft.active && bottomSquare.forwardRight.active && bottomSquare.backwardRight.active && bottomSquare.backwardLeft.active);
		case Direction.right:
			return (topSquare.forwardRight.active && bottomSquare.forwardRight.active && topSquare.backwardRight.active && bottomSquare.backwardRight.active);
		case Direction.left:
			return (topSquare.forwardLeft.active && bottomSquare.forwardLeft.active && topSquare.backwardLeft.active && bottomSquare.backwardLeft.active);
		case Direction.forward:
			return (topSquare.forwardLeft.active && topSquare.forwardRight.active && bottomSquare.forwardLeft.active && bottomSquare.forwardRight.active);
		case Direction.back:
			return (bottomSquare.backwardRight.active && bottomSquare.backwardLeft.active && topSquare.backwardRight.active && topSquare.backwardLeft.active);
		}
		return false;
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

	public bool IsControlNodeActive(ControlNode cn){
		if(forwardLeft.position == cn.position && forwardLeft.active)
			return true;
		if(forwardRight.position == cn.position && forwardRight.active)
			return true;
		if(backwardRight.position == cn.position && backwardRight.active)
			return true;
		if(backwardLeft.position == cn.position && backwardLeft.active)
			return true;

		return false;
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