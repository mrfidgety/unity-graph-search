using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntAndKillMazeAlgorithm : MazeAlgorithm {

	private MazeNode currentNode;
	private bool courseComplete = false;

	public HuntAndKillMazeAlgorithm(MazeNode[,] mazeNodes) : base(mazeNodes) {}

	public override void CreateMaze() {
		HuntAndKill ();
	}

	private void HuntAndKill() {
		// Set starting maze node
		currentNode = mazeNodes[0,0];

		// Loop through Kill and Hunt process
		while (!courseComplete) {
			Kill ();
			Hunt ();
		}

		// Set all nodes to not visited
		UnvisitAllNodes();
	}

	private void Kill () {
		while (CanMove()) {
			Move();
		}
	}

	private void Hunt() {
		// Assume course is complete until we find otherwise
		courseComplete = true;

		MazeNode[] tempArray = FlattenAndShuffleNodes();

		// Hunt for random unvisited node with a visited adjacent node
		foreach(MazeNode node in tempArray) {
			if(!node.visited && node.VisitedAdjacentNodes().Count > 0) {
				courseComplete = false;

				// Choose random index
				int index = Random.Range(0, node.VisitedAdjacentNodes().Count);

				// Move to the random unvisited node
				node.MoveTo(node.VisitedAdjacentNodes()[index]);

				// Set as current node
				currentNode = node;
				currentNode.Visit();
			}
		}
	}

	private bool CanMove () {
		return currentNode.UnvisitedAdjacentNodes().Count > 0;
	}

	private void Move () {
		// Set current node to unvisited
		currentNode.Visit();

		// Choose random index
		int index = Random.Range(0, currentNode.UnvisitedAdjacentNodes().Count);

		// Move to random unvisited node
		MazeNode nextNode = currentNode.UnvisitedAdjacentNodes()[index];

		// Destroy wall between nodes
		currentNode.MoveTo(nextNode);

		// Set new current node
		currentNode = nextNode;
	}

	private MazeNode[] FlattenAndShuffleNodes() {
		int rows = mazeNodes.GetLength(0);
		int cols = mazeNodes.GetLength(1);

		MazeNode[] result = new MazeNode[rows * cols];

		// Flatten mazeNodes into new one dimensional array
		for(int row = 0; row < rows; row++) {
			for(int col = 0; col < cols; col++) {
				result[(row * cols) + col] = mazeNodes[row, col];
			}
		}

		// Shuffle result
		for(int i = 0; i < result.Length; i++) {
			MazeNode tmp = result[i];
			int random = Random.Range(0, result.Length);
			result[i] = result[random];
			result[random] = tmp;
		}

		return result;
	}

	private void UnvisitAllNodes() {
		foreach(MazeNode node in mazeNodes) {
			 node.Unvisit();
		}
	}
}
