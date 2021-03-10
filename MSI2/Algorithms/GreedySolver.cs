using MSI2.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSI2.Algorithms
{
    public class GreedySolver : ICVRPSolver
    {
        public (List<List<Node>> VisitedNodes, int TotalDistance) Solve(Graph graph, int vehiclesNumber, int capacity, int sMax)
        {

            int visitedNodesNum = 0;
            int totalDistance = 0;
            var visitedNodes = new List<List<Node>>();
            for (int i = 0; i < vehiclesNumber; i++)
            {
                Node lastNode = graph.NodeList[0]; ;
                Node currentNode = graph.NodeList[0];
                int currentDistance = sMax;
                int currentCapacity = capacity;
                var visited = new List<Node>();
                while (currentNode != null)
                {
                    visitedNodesNum++;
                    visited.Add(currentNode);
                    currentDistance -= graph.GetDistance(lastNode.id, currentNode.id);
                    totalDistance += graph.GetDistance(lastNode.id, currentNode.id);

                    currentCapacity -= currentNode.demand;
                    lastNode = currentNode;
                    currentNode = NextNode(graph, currentDistance, currentNode.id, currentCapacity);
                    if (currentNode.id == 0)
                    {
                        visitedNodes.Add(visited);
                        break;
                    }
                    currentNode.visited = true;
                }

                if (visitedNodesNum == graph.NodeList.Count)
                {
                    return (visitedNodes, totalDistance);
                }
            }
            return (null, -1);
        }

        public Node NextNode(Graph graph, int currentDistance, int currentNodeNumber, int currentCapacity)
        {
            var sortedByValidCapacity = graph.AdjList[currentNodeNumber]
                .Where(n => currentCapacity - graph.NodeList[n.id].demand >= 0 && !graph.NodeList[n.id].visited)
                .OrderBy(n => n.distance).ToList();
            if (sortedByValidCapacity.Any() && sortedByValidCapacity[0].distance <= currentDistance)
            {
                return graph.NodeList[sortedByValidCapacity[0].id];
            }
            return null;
        }
    }
}
