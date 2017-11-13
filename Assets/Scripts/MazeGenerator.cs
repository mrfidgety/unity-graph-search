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
		worldCamera.transform.position = new Vector3 (rows * 10 / 2, 100, columns * 10 / 2);
		worldCamera.orthographicSize = (rows + columns) / 2 * 8;
	}

	private void InitializeMazeNodes(int mazeRows, int mazeColumns) {
		mazeNodes = new MazeNode[mazeRows, mazeColumns];
			
		for (int row = 0; row < mazeRows; row++) {
			for (int col = 0; col < mazeColumns; col++) {
				CreateNode (row, col);
			}
		}
	}

	private void CreateNode(int row, int col) {
		MazeNode newNode = Instantiate (
			mazeNodePrefab, 
			new Vector3 (row * 10, 0, col * 10), 
			Quaternion.identity, 
			this.transform
		) as MazeNode;

		newNode.name = "Node (" + row + "," + col + ")";
		mazeNodes [row, col] = newNode;
	}
}
