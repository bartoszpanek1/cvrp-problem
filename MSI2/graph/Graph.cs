using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSI2.graph
{
    class Node
    {
        public int id, demand;
        public bool visited;
        Node(int id, int demand)
        {
            this.id = id;
            this.demand = demand;
            visited = false;
        }
        public override bool Equals(object obj) => id == ((Node)obj).id;
    }
    class Graph
    {
        Dictionary<Node, List<(Node node, int distance)>> AdjList { get; set; }

        void AddNode(Node node)
        {
            if (!AdjList.ContainsKey(node))
            {
                AdjList.Add(node, new List<(Node, int)>());
            }
        }

        void AddEdge(Node from, Node to, int distance)
        {
            if (!AdjList[from].Any(n => n.node == to))
            {
                AdjList[from].Add((to, distance));
            }
        }
    }
}
