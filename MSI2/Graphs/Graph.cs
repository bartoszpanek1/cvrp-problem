using System.Collections.Generic;
using System.Linq;

namespace MSI2.Graphs
{
    public class Graph
    {
        Dictionary<Node, List<(Node node, int distance)>> AdjList { get; set; }

        public void AddNode(Node node)
        {
            if (!AdjList.ContainsKey(node))
            {
                AdjList.Add(node, new List<(Node, int)>());
            }
        }

        public void AddEdge(Node from, Node to, int distance)
        {
            if (!AdjList[from].Any(n => n.node == to))
            {
                AdjList[from].Add((to, distance));
            }
        }
    }
}
