using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

	public MazeNode mazeNodePrefab;
	public Camera worldCamera;
	public Camera playerCamera;
	public MazeSolver solver;
	public MazeNode[,] mazeNodes;
	public GameObject player;
	public GameObject beaconPrefab;
	private GameObject theBeacon;
	private MazeNode startNode, endNode;
	public int rows = 10, columns = 10;

	void Start () {
		playerCamera.enabled = false;
		worldCamera.enabled = true;

		MoveWorldCamera ();
		InitializeMazeNodes(rows, columns);
		GenerateMaze();
		CreateBeacon();
		ShowBeacon();
	}

	public void DepthFirstSearch() {
		solver = new DepthFirstMazeSolver (startNode, endNode);
		SolveMaze();
	}

	public void BreadthFirstSearch() {
		solver = new BreadthFirstMazeSolver (startNode, endNode);
		SolveMaze();
	}

	public void AStarSearch() {
		solver = new AStarMazeSolver (startNode, endNode);
		SolveMaze();
	}

	public void Reset() {
		StopAllCoroutines();
		ResetNodes();
		ResetPlayer();
	}

	public void RegenerateMaze() {
		StopAllCoroutines();

		foreach(MazeNode node in mazeNodes) {
			Destroy(node.gameObject);
		}

		InitializeMazeNodes(rows, columns);
		GenerateMaze();
	}

	public void SwitchCamera() {
		if(worldCamera.enabled) {
			worldCamera.enabled = false;
			playerCamera.enabled = true;
		} else {
			worldCamera.enabled = true;
			playerCamera.enabled = false;
		}
	}

	public void ShowBeacon() {
		if(theBeacon.GetComponent<Renderer>().enabled) {
			theBeacon.GetComponent<Renderer>().enabled = false;
		} else {
			theBeacon.GetComponent<Renderer>().enabled = true;
		}
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
		ResetNodes();
		ResetPlayer();
	}

	private void SolveMaze() {
		StopAllCoroutines();

		ResetNodes();

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

	private Vector3 CoordinatesLocation (IntVector2 coordinates) {
		return new Vector3 (coordinates.x * 10, 0, coordinates.z * 10);
	}

	private void ResetNodes() {
		UnvisitAllNodes();
		SetStartAndEndNodes();
	}

	private void ResetPlayer() {
		player.transform.position = new Vector3(0, 4, 0);
	}

	private void UnvisitAllNodes() {
		foreach(MazeNode node in mazeNodes) {
			 node.Unvisit();
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

	private void CreateBeacon() {
		theBeacon = Instantiate (
			beaconPrefab,
			CoordinatesLocation(endNode.coordinates),
			Quaternion.Euler(-90, 0, 0),
			this.transform
		);
	}
}
