using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNode : MonoBehaviour {
	public GameObject northWall, southWall, eastWall, westWall, floor;
	public bool visited = false;
	public IntVector2 coordinates;
	public List<MazeNode> connectedNodes;

	public bool UnvisitedNeighbourNodes () {
		bool unvisited = false;

		for(int i = 0; i < connectedNodes.Count; i++) {
			if(!connectedNodes[i].visited) {
				unvisited = true;
				break;
			}
		}

		return unvisited;
	}
}
