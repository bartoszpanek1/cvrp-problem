using MSI2.Graphs;
using MSI2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSI2.Algorithms
{
    public class BaseAntSolver
    {
        private const int ANTS_NUMBER = 10;
        private const int ITERATIONS = 10;
        private const int ALPHA = 10;
        private const int BETA = 1;
        private const double EVAPORATION_RATIO = 0.1;
        private readonly Random _rand = new Random();

        public (List<List<Node>> VisitedNodes, int TotalDistance) Solve(
            Graph graph,
            int vehiclesNumber,
            int capacity,
            int sMax,
            Action<EdgeDetails> localAction,
            Action<List<List<Node>>> globalAction
            )
        {
            List<List<Node>> result = new List<List<Node>>();
            int bestDistance = int.MaxValue;

            for (int i = 0; i < ITERATIONS; ++i)
            {
                List<List<Node>> antsRoutes = new List<List<Node>>();

                for (int j = 0; j < ANTS_NUMBER; ++j)
                {
                    antsRoutes.Add(FindResultForSingleAnt(graph, localAction, vehiclesNumber, capacity, sMax));
                    graph.UnvisitAllNodes();
                }

                EvaporatePheromone(graph);
                globalAction(antsRoutes);
                graph.PrintPheromones();

                (List<List<Node>> bestResultOfIteration, int bestDistanceOfIteration) = FindBestResult(graph, antsRoutes);
                (bestDistance, result) = bestDistance > bestDistanceOfIteration
                    ? (bestDistanceOfIteration, bestResultOfIteration)
                    : (bestDistance, result);
            }

            return (result, bestDistance);
        }

        private (List<List<Node>> bestResult, int distance) FindBestResult(Graph graph, List<List<Node>> antsRoutes)
        {
            List<Node> bestResult = antsRoutes.OrderBy(a => CalculateDistance(graph, a)).FirstOrDefault();
            return (TranslateResult(bestResult), CalculateDistance(graph, bestResult));
        }

        private List<List<Node>> TranslateResult(List<Node> route) 
        {
            if (route == null)
                return null;

            List<List<Node>> result = new List<List<Node>>();
            List<Node> temp = new List<Node>();

            route.ForEach(n =>
            {
                if (n.id == Graph.START_INDEX)
                {
                    if (temp.Any())
                        result.Add(temp);

                    temp = new List<Node>();
                }

                temp.Add(n);
            });

            return result;
        }

        private int CalculateDistance(Graph graph, List<Node> route)
        {
            if (route == null)
                return int.MaxValue;

            int distance = 0;
            for(int i = 1; i < route.Count; ++i)
            {
                distance += graph.GetDistance(route[i].id, route[i - 1].id);
            }

            return distance;
        }

        private List<Node> FindResultForSingleAnt(Graph graph, Action<EdgeDetails> localAction, int vehiclesNumber, int capacity, int sMax) // 0 1 2 3 0 4 5 6 0 7 8 0
        {
            List<Node> visitedNodes = new List<Node>();
            Node lastNode = graph.NodeList[Graph.START_INDEX]; ;
            Node currentNode = graph.NodeList[Graph.START_INDEX];
            int currentDistance = sMax;
            int currentCapacity = capacity;

            while (currentNode != null && vehiclesNumber > 0)
            {
                visitedNodes.Add(currentNode);
                currentDistance -= graph.GetDistance(lastNode.id, currentNode.id);
                currentCapacity -= currentNode.demand;

                lastNode = currentNode;
                currentNode.visited = true;
                currentNode = NextNode(graph, currentNode.id, currentDistance, currentCapacity);

                if (currentNode.id == Graph.START_INDEX)
                {
                    --vehiclesNumber;
                    currentDistance = sMax;
                    currentCapacity = capacity;
                }

                UpdateEdges(graph, currentNode, lastNode, localAction);
            }

            visitedNodes?.Add(graph.NodeList[Graph.START_INDEX]);
            //PrintHelper.PrintNodeList(visitedNodes);

            return graph.HasVisitedAllNodes
                ? visitedNodes
                : null;
        }

        private void UpdateEdges(Graph graph, Node currentNode, Node lastNode, Action<EdgeDetails> localAction)
        {
            if (lastNode == null || currentNode == null || localAction == null)
                return;

            graph.PerformOnBothEdges(currentNode.id, lastNode.id, localAction);
        }

        private Node NextNode(Graph graph, int id, int currentDistance, int currentCapacity)
        {
            List<EdgeDetails> availableEdges = graph.AdjList[id]
                .Where(e => !graph.NodeList[e.ToId].visited
                    && e.Distance <= currentDistance
                    && graph.NodeList[e.ToId].demand <= currentCapacity
                    && e.ToId != Graph.START_INDEX)
                .ToList();

            if (!availableEdges.Any())
            {
                return graph.GetDistance(id, Graph.START_INDEX) <= currentDistance
                    ? graph.NodeList[Graph.START_INDEX]
                    : null;
            }

            List<double> probabilities = availableEdges
                .Select(e => CalculateEdgeRatio(e, availableEdges.Count)).ToList();
            double sum = probabilities.Sum();

            double rand = _rand.NextDouble();
            int i = -1;

            while(rand >= 0)
            {
                rand -= probabilities[++i] / sum;
            }

            return graph.NodeList[availableEdges[i].ToId];
        }

        private double CalculateEdgeRatio(EdgeDetails e, double n) =>
            Math.Pow(e.Pheromone + 1.0 / n, ALPHA) * Math.Pow(1.0 / e.Distance, BETA) / n + 1.0 / n;

        private void EvaporatePheromone(Graph graph)
        {
            foreach(var item in graph.AdjList)
            {
                item.Value.ForEach(e => e.Pheromone *= 1 - EVAPORATION_RATIO);
            }
        }
    }
}
