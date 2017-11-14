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

    while(stack.Count > 0 && !solved) {
      MazeNode currentNode = stack.Pop();

      if(!currentNode.visited) {
        currentNode.Visit();

        if(currentNode == endNode) {
          solved = true;
        } else {
          foreach(MazeNode node in currentNode.connectedNodes) {
            stack.Push(node);
          }
        }

        yield return delay;
      }
    }
  }
}
