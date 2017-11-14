using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

	public MazeNode mazeNodePrefab;
	public Camera worldCamera;
	public int rows = 10, columns = 10;

	public MazeNode[,] mazeNodes;

	void Start () {
		MoveWorldCamera ();
		InitializeMazeNodes(rows, columns);


		//MazeAlgorithm algorithm = new HuntAndKillMazeAlgorithm (mazeNodes);
		//algorithm.CreateMaze ();
	}

	private void MoveWorldCamera() {
		// Set central position
		worldCamera.transform.position = new Vector3 (rows * 10 / 2, 100, columns * 10 / 2);

		// Adjust camera view size
		worldCamera.orthographicSize = (rows + columns) / 2 * 8;
	}

	private void InitializeMazeNodes(int mazeRows, int mazeColumns) {
		// Create two-dimensional array for maze nodes
		mazeNodes = new MazeNode[mazeRows, mazeColumns];

		// Fill array of maze nodes
		for (int row = 0; row < mazeRows; row++) {
			for (int col = 0; col < mazeColumns; col++) {
				CreateNode (new IntVector2(row, col));
			}
		}

		// Associate maze nodes with those connected physically
		ConnectMazeNodes();
	}

	private void CreateNode(IntVector2 coordinates) {
		// Instantiate new maze node at the coordinates
		MazeNode newNode = Instantiate (
			mazeNodePrefab,
			CoordinatesLocation(coordinates),
			Quaternion.identity,
			this.transform
		) as MazeNode;

		// Set the coordinates attribute on the new node
		newNode.coordinates = new IntVector2(coordinates.x, coordinates.z);

		// Add the new node to the mazeNodes
		mazeNodes [coordinates.x, coordinates.z] = newNode;

		// Naming object for ease of visual identification
		newNode.name = "Node (" + coordinates.x + "," + coordinates.z + ")";
	}

	private void ConnectMazeNodes () {
		foreach(MazeNode node in mazeNodes) {
			AddConnectedNodes (node);
		}
	}

	private void AddConnectedNodes(MazeNode node) {
		node.connectedNodes = new List<MazeNode>();

		int x_coord = node.coordinates.x;
		int z_coord = node.coordinates.z;

		if(x_coord > 0) {
			node.connectedNodes.Add(
				mazeNodes[x_coord - 1, z_coord]);
		}
		if(x_coord < mazeNodes.GetLength(0) - 1) {
			node.connectedNodes.Add(mazeNodes[x_coord + 1, z_coord]);
		}
		if(z_coord > 0) {
			node.connectedNodes.Add(mazeNodes[x_coord, z_coord - 1]);
		}
		if(z_coord < mazeNodes.GetLength(1) - 1) {
			node.connectedNodes.Add(mazeNodes[x_coord, z_coord + 1]);
		}
	}

	private Vector3 CoordinatesLocation (IntVector2 coordinates) {
		return new Vector3 (coordinates.x * 10, 0, coordinates.z * 10);
	}
}
