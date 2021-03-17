using MSI2.Graphs;
using System;
using System.Collections.Generic;

namespace MSI2.Algorithms
{
    public class BasicAntSolver : BaseAntSolver, ICVRPSolver
    {
        public (List<List<Node>> VisitedNodes, int TotalDistance) Solve(Graph graph, int vehiclesNumber, int capacity, int sMax)
        {
            Action<List<List<Node>>> globalAction = null;

            return base.Solve(graph, vehiclesNumber, capacity, sMax, null, globalAction);
        }
    }
}
