using System.Collections.Generic;
using MSI2.Graphs;

namespace MSI2.Algorithms
{
    public interface ICVRPSolver
    {
        (List<List<Node>> VisitedNodes, int TotalDistance) Solve(Graph graph, int vehiclesNumber, int capacity, int sMax);
    }
}
