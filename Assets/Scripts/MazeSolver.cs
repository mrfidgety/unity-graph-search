using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeSolver {
	protected MazeNode startNode, endNode;
  protected bool solved = false;

	protected MazeSolver(MazeNode start, MazeNode end) {
		this.startNode = start;
		this.endNode = end;
	}

	public MazeSolver () {}

	public abstract IEnumerator SolveMaze ();
}
