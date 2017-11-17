using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarMazeSolver : MazeSolver {
  protected List<MazeNode> openSet;

  public AStarMazeSolver(MazeNode start, MazeNode end) : base(start, end) {}

  public override IEnumerator SolveMaze() {
    // Create a list of nodes that are yet to be explored
    openSet = new List<MazeNode>();

    // Initialize gScore and fScore maps
    var gScore = new Dictionary<MazeNode, int>();
    var fScore = new Dictionary<MazeNode, int>();

    // Set up start of search
    openSet.Add(startNode);
    gScore.Add(startNode, 0);
    fScore.Add(startNode, HeuristicCostEstimate(startNode));

    WaitForSeconds delay = new WaitForSeconds(0.01F);

    while(!solved && openSet.Count > 0) {
      // Find open node with lowest fScore
      MazeNode currentNode = openSet.OrderBy(n => fScore[n]).First();

      // Display visually
      currentNode.Visit();
      yield return delay;

      // Check if we have reached the goal
      if(currentNode == endNode) {
        solved = true;
        ReconstructPath(currentNode);
        continue;
      }

      // Remove current node from open set
      openSet.Remove(currentNode);

      // Discover connected nodes
      foreach(MazeNode neighbor in currentNode.connectedNodes) {
        // Ignore already visited nodes
        if(neighbor.visited) continue;

        // Add to open set if not already there
        if(!openSet.Any(n => n.coordinates == neighbor.coordinates)) {
          openSet.Add(neighbor);
          gScore[neighbor] = 1000;
          fScore[neighbor] = 1000;
        }

        // Distance between connected nodes is 1
        int tentativeScore = gScore[currentNode] + 1;

        // Continue if this is not a better path
        if(tentativeScore >= gScore[neighbor]) continue;

        // Best path so far
        parentSet[neighbor] = currentNode;
        gScore[neighbor] = tentativeScore;
        fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor);
      }
    }
  }
}
