using MSI2.Graphs;
using System;
using System.Collections.Generic;

namespace MSI2.Algorithms
{
    public class LocalAntSolver : BaseAntSolver, ICVRPSolver
    {
        private const double AMOUNT_OF_PHEROMONE_TO_PLACE = 0.1;

        public (List<List<Node>> VisitedNodes, int TotalDistance) Solve(Graph graph, int vehiclesNumber, int capacity, int sMax)
        {
            Action<EdgeDetails> localAction = e => PlacePheromoneOnEdge(e);

            return Solve(graph, vehiclesNumber, capacity, sMax, localAction, _ => { });
        }

        private void PlacePheromoneOnEdge(EdgeDetails e) =>
            e.Pheromone += AMOUNT_OF_PHEROMONE_TO_PLACE;
    }
}
