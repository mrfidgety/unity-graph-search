using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

	public MazeNode mazeNodePrefab;
	public Camera worldCamera;
	public MazeSolver mazeSolver;
	public MazeNode[,] mazeNodes;
	public MazeNode startNode, endNode;
	public int rows = 10, columns = 10;

	void Start () {
		MoveWorldCamera ();
		InitializeMazeNodes(rows, columns);
		GenerateMaze();
		SolveMaze();
	}

	private void MoveWorldCamera() {
		// Set central position
		worldCamera.transform.position = new Vector3 (
			rows * 10 / 2, 100, columns * 10 / 2
		);

		// Adjust camera view size
		worldCamera.orthographicSize = (rows + columns) / 2 * 5;
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
		AssociateMazeNodes();
	}

	private void GenerateMaze() {
		// Set maze generation algorithm
		MazeAlgorithm algorithm = new HuntAndKillMazeAlgorithm (mazeNodes);

		algorithm.CreateMaze ();

		// Set start and end nodes
		SetStartAndEndNodes();
	}

	private void SolveMaze() {
		// Set maze solving algorithm
		//MazeSolver solver = new BreadthFirstMazeSolver (startNode, endNode);
		MazeSolver solver = new DepthFirstMazeSolver (startNode, endNode);


		StartCoroutine(solver.SolveMaze ());
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

	private void AssociateMazeNodes () {
		foreach(MazeNode node in mazeNodes) {
			AddAdjacentNodes (node);
		}
	}

	private void AddAdjacentNodes(MazeNode node) {
		node.adjacentNodes = new List<MazeNode>();

		int x_coord = node.coordinates.x;
		int z_coord = node.coordinates.z;

		if(x_coord > 0) {
			node.adjacentNodes.Add(
				mazeNodes[x_coord - 1, z_coord]);
		}
		if(x_coord < mazeNodes.GetLength(0) - 1) {
			node.adjacentNodes.Add(mazeNodes[x_coord + 1, z_coord]);
		}
		if(z_coord > 0) {
			node.adjacentNodes.Add(mazeNodes[x_coord, z_coord - 1]);
		}
		if(z_coord < mazeNodes.GetLength(1) - 1) {
			node.adjacentNodes.Add(mazeNodes[x_coord, z_coord + 1]);
		}
	}

	private void SetStartAndEndNodes() {
		SetStartNode(mazeNodes[0,0]);
		SetEndNode(mazeNodes[rows - 1, columns - 1]);
	}

	private void SetStartNode(MazeNode node) {
		startNode = node;
		startNode.floor.GetComponent<Renderer>().material.color = new Color(1.0F, 0.1F, 0.4F, 1.0F);
	}

	private void SetEndNode(MazeNode node) {
		endNode = node;
		endNode.floor.GetComponent<Renderer>().material.color = new Color(0.3F, 0.5F, 1.0F, 1.0F);
	}

	private Vector3 CoordinatesLocation (IntVector2 coordinates) {
		return new Vector3 (coordinates.x * 10, 0, coordinates.z * 10);
	}
}
