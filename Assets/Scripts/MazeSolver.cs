using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeSolver {
	protected MazeNode startNode, endNode;
	protected Dictionary<MazeNode, MazeNode> parentSet;
  protected bool solved = false;

	protected MazeSolver(MazeNode start, MazeNode end) {
		this.startNode = start;
		this.endNode = end;

		// Initialize parent set solution map
		parentSet = new Dictionary<MazeNode, MazeNode>();
	}

	public MazeSolver () {}

	public abstract IEnumerator SolveMaze ();

	protected void ReconstructPath(MazeNode node) {
		node.SetPath();

    while(parentSet.ContainsKey(node)) {
      node = parentSet[node];
			node.SetPath();
    }
  }

	protected int HeuristicCostEstimate(MazeNode node) {
    return node.coordinates.ManhattanDistanceTo(endNode.coordinates);
  }
}
