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
            int totalDistance = 0;
            var visitedNodes = new List<List<Node>>();

            for (int i = 0; i < vehiclesNumber; i++)
            {
                Node lastNode = graph.NodeList[Graph.START_INDEX]; ;
                Node currentNode = graph.NodeList[Graph.START_INDEX];
                int currentDistance = sMax;
                int currentCapacity = capacity;
                var visited = new List<Node>();

                while (currentNode != null)
                {
                    visited.Add(currentNode);
                    currentDistance -= graph.GetDistance(lastNode.id, currentNode.id);
                    totalDistance += graph.GetDistance(lastNode.id, currentNode.id);

                    currentCapacity -= currentNode.demand;
                    lastNode = currentNode;
                    currentNode = NextNode(graph, currentNode.id, currentDistance, currentCapacity);

                    if (currentNode.id == Graph.START_INDEX)
                    {
                        visitedNodes.Add(visited);
                        break;
                    }

                    currentNode.visited = true;
                }

                if (graph.HasVisitedAllNodes)
                {
                    return (visitedNodes, totalDistance);
                }
            }
            return (null, -1);
        }

        private Node NextNode(Graph graph, int currentNodeNumber, int currentDistance, int currentCapacity)
        {
            List<(int id, int distance)> sortedByValidCapacity = graph.AdjList[currentNodeNumber]
                .Where(n => currentCapacity - graph.NodeList[n.id].demand >= 0
                    && !graph.NodeList[n.id].visited
                    && n.id != Graph.START_INDEX)
                .OrderBy(n => n.distance)
                .ToList();

            PushStartNode(sortedByValidCapacity, graph, currentNodeNumber);

            return sortedByValidCapacity.Any() && sortedByValidCapacity[0].distance <= currentDistance
                ? graph.NodeList[sortedByValidCapacity[0].id]
                : null;
        }

        private void PushStartNode(List<(int id, int distance)> sortedByValidCapacity, Graph graph, int currentNodeNumber)
        {
            if(currentNodeNumber != Graph.START_INDEX)
                sortedByValidCapacity.Add(graph.AdjList[currentNodeNumber][Graph.START_INDEX]);
        }
    }
}
