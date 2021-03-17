using MSI2.Graphs;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MSI2.Algorithms
{
    public class BasicAntSolver : BaseAntSolver, ICVRPSolver
    {
        private const double AMOUNT_OF_PHEROMONE_TO_PLACE = 0.1;

        public (List<List<Node>> VisitedNodes, int TotalDistance) Solve(Graph graph, int vehiclesNumber, int capacity, int sMax)
        {
            Action<List<List<Node>>> globalAction = n => PlacePheromoneGloballyForEachAnt(graph, n);

            return Solve(graph, vehiclesNumber, capacity, sMax, null, globalAction);
        }

        private void PlacePheromoneGloballyForEachAnt(Graph graph, List<List<Node>> results)
        {
            results.ForEach(r =>
            {
                PlacePheromoneForSingleResult(graph, r);
            });
        }

        private void PlacePheromoneForSingleResult(Graph graph, List<Node> route)
        {
            for (int i = 1; i < route.Count; ++i)
            {
                graph.PerformOnBothEdges(route[i - 1].id, route[i].id, e => e.Pheromone += AMOUNT_OF_PHEROMONE_TO_PLACE);
            }
        }
    }
}
