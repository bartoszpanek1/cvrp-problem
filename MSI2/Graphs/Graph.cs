using System.Collections.Generic;
using System.Linq;

namespace MSI2.Graphs
{
    public class Graph
    {
        public Dictionary<int, List<(int id, int distance)>> AdjList { get; set; } = new Dictionary<int, List<(int id, int distance)>>();
        public List<Node> NodeList { get; set; } = new List<Node>();

        public Graph(int[] capacities)
        {
            NodeList = new List<Node> { new Node(0, 0) };
            NodeList.AddRange(capacities.Select((c, id) => new Node(id + 1, c)));
            AdjList = new Dictionary<int, List<(int id, int distance)>>();
            NodeList.ForEach(n => AdjList.Add(n.id, new List<(int id, int distance)>()));
        }

        public void AddEdge(int from, int to, int distance)
        {
            if (AdjList[from].Any(n => n.id == to))
                return;

            AdjList[from].Add((to, distance));
            AdjList[to].Add((from, distance));
            System.Console.WriteLine($"Adding {from} {to} {distance}");
            System.Console.WriteLine($"Adding {to} {from} {distance}");
        }

        public int GetDistance(int from, int to) =>
            from != to
                ? AdjList[from].First(n => n.id == to).distance
                : 0;
    }
}
