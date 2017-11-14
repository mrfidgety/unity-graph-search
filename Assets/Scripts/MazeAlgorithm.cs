using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeAlgorithm {
	protected MazeNode[,] mazeNodes;

	protected MazeAlgorithm(MazeNode[,] mazeNodes) {
		this.mazeNodes = mazeNodes;
	}

	public MazeAlgorithm () {}

	public abstract void CreateMaze ();
}
