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

	public void Visit() {
		visited = true;
		floor.GetComponent<Renderer>().material.color = new Color(1.0F, 0.1F, 0.4F, 1.0F);
	}

	public void Unvisit() {
		visited = false;
		floor.GetComponent<Renderer>().material.color = new Color(0F, 0F, 0F, 0F);
	}

	public void SetPath() {
		floor.GetComponent<Renderer>().material.color = new Color(0.3F, 0.5F, 1.0F, 1.0F);
	}

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

		// East/West
		if (direction_x == 1) {
			Destroy(eastWall);
			Destroy(node.westWall);
		} else if (direction_x == -1) {
			Destroy(westWall);
			Destroy(node.eastWall);
		}

		// North/South
		if (direction_z == 1) {
			Destroy(northWall);
			Destroy(node.southWall);
		} else if (direction_z == -1) {
			Destroy(southWall);
			Destroy(node.northWall);
		}
	}

	void OnTriggerEnter (Collider other) {
			Visit();
			Debug.Log("Trigger");
	}
}
