using MSI2.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSI2.Algorithms
{
    public class GreedySolver : ICVRPSolver
    {
        private static readonly (List<List<Node>> VisitedNodes, int TotalDistance) NO_RESULT = (null, -1);

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
                    Console.WriteLine($"Vehicle num {i}, current node {currentNode.id}");
                    visited.Add(currentNode);
                    currentDistance -= graph.GetDistance(lastNode.id, currentNode.id);
                    totalDistance += graph.GetDistance(lastNode.id, currentNode.id);
                    currentCapacity -= currentNode.demand;

                    lastNode = currentNode;
                    currentNode = NextNode(graph, currentNode.id, currentDistance, currentCapacity);

                    if (currentNode == null)
                    {
                        return NO_RESULT;
                    }

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

            return NO_RESULT;
        }

        private Node NextNode(Graph graph, int currentNodeNumber, int currentDistance, int currentCapacity)
        {
            List<EdgeDetails> sortedByValidCapacity = graph.AdjList[currentNodeNumber]
                .Where(n => currentCapacity - graph.NodeList[n.ToId].demand >= 0
                    && !graph.NodeList[n.ToId].visited
                    && n.ToId != Graph.START_INDEX
                    && graph.GetDistance(currentNodeNumber, n.ToId) <= currentDistance)
                .OrderBy(n => n.Distance)
                .ToList();

            PushStartNode(sortedByValidCapacity, graph, currentNodeNumber);

            return sortedByValidCapacity.Any() && sortedByValidCapacity[0].Distance <= currentDistance
                ? graph.NodeList[sortedByValidCapacity[0].ToId]
                : null;
        }

        private void PushStartNode(List<EdgeDetails> sortedByValidCapacity, Graph graph, int currentNodeNumber)
        {
            if (currentNodeNumber != Graph.START_INDEX)
                sortedByValidCapacity.Add(graph.AdjList[currentNodeNumber][Graph.START_INDEX]);
        }

    }
}
