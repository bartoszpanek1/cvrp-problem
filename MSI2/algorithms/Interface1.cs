using System;
using System.Collections.Generic;
using System.Text;
using MSI2.graph;

namespace MSI2.algorithms
{
    interface CVRPSolver
    {
        (List<List<Node>> VisitedNodes, int TotalDistance) solve(Graph graph);
    }
}
