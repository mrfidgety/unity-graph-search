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

		while (!courseComplete) {
			Kill ();
			Hunt ();
		}
	}

	private void Kill () {
		while (CanMove()) {
			
		}
	}

	private void Hunt() {

	}

	private bool CanMove () {
		return currentNode.UnvisitedNeighbourNodes();
	}
}
