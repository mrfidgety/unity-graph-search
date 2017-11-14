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
	}

	private void Kill () {
		while (CanMove()) {
			Move();
		}
	}

	private void Hunt() {
		courseComplete = true;

		MazeNode[] tempArray = new MazeNode[mazeNodes.GetLength(0) * mazeNodes.GetLength(1)];

		// Flatten mazeNodes into tempArray
		for(int row = 0; row < mazeNodes.GetLength(0); row++) {
			for(int col = 0; col < mazeNodes.GetLength(1); col++) {
				tempArray[(row * mazeNodes.GetLength(1)) + col] = mazeNodes[row, col];
			}
		}

		// Shuffle tempArray
		for(int i = 0; i < tempArray.Length; i++) {
			MazeNode tmp = tempArray[i];
			int random = Random.Range(0, tempArray.Length);
			tempArray[i] = tempArray[random];
			tempArray[random] = tmp;
		}

		// Hunt for unvisited adjacent node
		foreach(MazeNode node in tempArray) {
			if(!node.visited && node.VisitedAdjacentNodes().Count > 0) {
				courseComplete = false;

				// Choose random index
				int index = Random.Range(0, node.VisitedAdjacentNodes().Count);

				// Destroy wall to the visited node
				node.MoveTo(node.VisitedAdjacentNodes()[index]);

				// Set as current node
				currentNode = node;
				currentNode.visited = true;
			}
		}
	}

	private bool CanMove () {
		return currentNode.UnvisitedAdjacentNodes().Count > 0;
	}

	private void Move () {
		// Set current node to unvisited
		currentNode.visited = true;

		// Choose random index
		int index = Random.Range(0, currentNode.UnvisitedAdjacentNodes().Count);

		// Move to random unvisited node
		MazeNode nextNode = currentNode.UnvisitedAdjacentNodes()[index];

		// Destroy wall between nodes
		currentNode.MoveTo(nextNode);

		// Set new current node
		currentNode = nextNode;
	}
}
