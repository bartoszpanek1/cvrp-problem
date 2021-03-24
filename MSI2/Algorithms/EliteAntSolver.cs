using MSI2.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSI2.Algorithms
{
    public class EliteAntSolver : BaseAntSolver, ICVRPSolver
    {
        private const double AMOUNT_OF_PHEROMONE_TO_PLACE = 0.1;
        private const int NUMBER_OF_BEST_RESULTS_TAKEN = 3;

        public (List<List<Node>> VisitedNodes, int TotalDistance) Solve(Graph graph, int vehiclesNumber, int capacity, int sMax)
        {
            Action<List<List<Node>>> globalAction = n => PlacePheromoneGloballyForEachAnt(graph, n);

            return Solve(graph, vehiclesNumber, capacity, sMax, null, globalAction);
        }

        private void PlacePheromoneGloballyForEachAnt(Graph graph, List<List<Node>> results)
        {
            results.OrderByDescending(a => CalculateTotalDist(graph, a))
                .Take(NUMBER_OF_BEST_RESULTS_TAKEN)
                .ToList()
                .ForEach(r =>
                {
                    PlacePheromoneForSingleResult(graph, r);
                });
        }

        private int CalculateTotalDist(Graph graph, List<Node> route)
        {
            int dist = 0;

            for (int i = 1; i < route.Count; ++i)
            {
                dist += graph.GetDistance(route[i - 1].id, route[i].id);
            }

            return dist + graph.GetDistance(route[route.Count - 1].id, Graph.START_INDEX);
        }

        private void PlacePheromoneForSingleResult(Graph graph, List<Node> route)
        {
            for (int i = 1; route != null && i < route.Count; ++i)
            {
                graph.PerformOnBothEdges(route[i - 1].id, route[i].id, e => e.Pheromone += AMOUNT_OF_PHEROMONE_TO_PLACE);
            }
        }
    }
}
