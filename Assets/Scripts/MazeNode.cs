using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeNode : MonoBehaviour {
	public GameObject northWall, southWall, eastWall, westWall, floor;
	public bool visited = false;
	public IntVector2 coordinates;
	public List<MazeNode> adjacentNodes;
	public List<MazeNode> connectedNodes;

	public List<MazeNode> UnvisitedAdjacentNodes () {
		List<MazeNode> unvisited = new List<MazeNode>();

		for(int i = 0; i < adjacentNodes.Count; i++) {
			if(!adjacentNodes[i].visited) {
				unvisited.Add(adjacentNodes[i]);
			}
		}

		return unvisited;
	}

	public List<MazeNode> VisitedAdjacentNodes () {
		return adjacentNodes.Except(UnvisitedAdjacentNodes()).ToList();
	}

	public void MoveTo(MazeNode node) {
		CreateConnectionTo(node);
		DestroyWallTo(node);
	}

	public void CreateConnectionTo(MazeNode node) {
		connectedNodes.Add(node);
		node.connectedNodes.Add(this);
	}

	public void DestroyWallTo(MazeNode node) {
		int direction_x = node.coordinates.x - coordinates.x;
		int direction_z = node.coordinates.z - coordinates.z;

		if (direction_x == 1) {
			Destroy(eastWall);
			Destroy(node.westWall);
		} else if (direction_x == -1) {
			Destroy(westWall);
			Destroy(node.eastWall);
		}

		if (direction_z == 1) {
			Destroy(northWall);
			Destroy(node.southWall);
		} else if (direction_z == -1) {
			Destroy(southWall);
			Destroy(node.northWall);
		}
	}
}
