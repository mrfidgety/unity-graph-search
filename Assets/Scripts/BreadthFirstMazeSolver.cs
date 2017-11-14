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

    while(queue.Count > 0 && !solved) {
      MazeNode currentNode = queue.Dequeue();

      if(!currentNode.visited) {
        currentNode.Visit();

        if(currentNode == endNode) {
          solved = true;
        } else {
          foreach(MazeNode node in currentNode.connectedNodes) {
            queue.Enqueue(node);
          }
        }

        yield return delay;
      }
    }
  }
}
