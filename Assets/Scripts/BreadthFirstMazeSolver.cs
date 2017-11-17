using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstMazeSolver : MazeSolver {
  protected Queue<MazeNode> queue;

  public BreadthFirstMazeSolver(MazeNode start, MazeNode end) : base(start, end) {}

  public override IEnumerator SolveMaze() {
    queue = new Queue<MazeNode>();

    queue.Enqueue(startNode);

    WaitForSeconds delay = new WaitForSeconds(0.025F);

    while(!solved && queue.Count > 0) {
      MazeNode currentNode = queue.Dequeue();

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
          queue.Enqueue(neighbor);
        }
      }
    }
  }
}
