using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntAndKillMazeAlgorithm : MazeAlgorithm {
	
	private int currentRow = 0, currentCol = 0;
	private bool courseComplete = false;

	public HuntAndKillMazeAlgorithm(MazeNode[,] mazeNodes) : base(mazeNodes) {}

	public override void CreateMaze() {
		HuntAndKill ();
	}

	private void HuntAndKill() {
		while (!courseComplete) {
			Kill ();
			Hunt ();
		}
	}

	private void Kill () {
		while (CanMove (currentRow, currentCol)) {

		}
	}

	private void Hunt() {

	}

	private bool CanMove(int row, int col) {
		return true;
		//NeighbourNodes (row, col);
	}

	/**private GameObject[] NeighbourNodes (int row, int col) {
		return GameObject[] neighbours =-;
	}**/
}
