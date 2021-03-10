using System.Collections.Generic;
using System.Linq;

namespace MSI2.Graphs
{
    public class Graph
    {
        public Dictionary<int, List<(int id, int distance)>> AdjList { get; set; }
        public List<Node> NodeList { get; set; }

        public void AddNode(Node node)
        {
            if (!AdjList.ContainsKey(node.id))
            {
                AdjList.Add(node.id, new List<(int, int)>());
                NodeList.Add(node);
            }
        }

        public void AddEdge(int from, int to, int distance)
        {
            if (!AdjList[from].Any(n => n.id == to))
            {
                AdjList[from].Add((to, distance));
            }
        }

        public int GetDistance(int from, int to)
        {
            if(from == to)
            {
                return 0;
            }
            return AdjList[from].First(n => n.id == to).distance;
        }
    }
}
