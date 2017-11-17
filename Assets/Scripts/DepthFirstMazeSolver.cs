using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstMazeSolver : MazeSolver {
  protected Stack<MazeNode> stack;

  public DepthFirstMazeSolver(MazeNode start, MazeNode end) : base(start, end) {}

  public override IEnumerator SolveMaze() {
    stack = new Stack<MazeNode>();

    stack.Push(startNode);

    WaitForSeconds delay = new WaitForSeconds(0.025F);

    while(!solved && stack.Count > 0) {
      MazeNode currentNode = stack.Pop();

      currentNode.Visit();
      yield return delay;

      if(currentNode == endNode) {
        solved = true;
        ReconstructPath(currentNode);
        continue;
      }
      
      foreach(MazeNode neighbor in currentNode.connectedNodes) {
        if(!neighbor.visited) {
          parentSet[neighbor] = currentNode;
          stack.Push(neighbor);
        }
      }
    }
  }
}
