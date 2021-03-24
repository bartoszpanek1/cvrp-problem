using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MSI2.Graphs
{
    public class Graph
    {
        public const int START_INDEX = 0;
        public const double INITIAL_PHEROMONE = 0;
        public Dictionary<int, List<EdgeDetails>> AdjList { get; set; } = new Dictionary<int, List<EdgeDetails>>();
        public List<Node> NodeList { get; set; } = new List<Node>();

        public bool HasVisitedAllNodes => NodeList.All(n => n.visited || n.id == START_INDEX);

        public Graph(int[] capacities)
        {
            NodeList = new List<Node> { new Node(START_INDEX, 0) };
            NodeList.AddRange(capacities.Select((c, id) => new Node(id + 1, c)));
            AdjList = new Dictionary<int, List<EdgeDetails>>();
            NodeList.ForEach(n => AdjList.Add(n.id, new List<EdgeDetails>()));
        }

        public void AddEdge(int from, int to, int distance)
        {
            if (AdjList[from].Any(n => n.ToId == to))
                return;

            AdjList[from].Add(new EdgeDetails(to, distance, INITIAL_PHEROMONE));
            AdjList[to].Add(new EdgeDetails(from, distance, INITIAL_PHEROMONE));
            System.Console.WriteLine($"Adding {from} {to} {distance}");
            System.Console.WriteLine($"Adding {to} {from} {distance}");
        }

        public int GetDistance(int from, int to) =>
            from != to
                ? AdjList[from].First(n => n.ToId == to).Distance
                : 0;

        public (EdgeDetails e1, EdgeDetails e2) GetEdges(int from, int to) =>
            (AdjList[from].FirstOrDefault(e => e.ToId == to), AdjList[to].FirstOrDefault(e => e.ToId == from));

        public void PerformOnBothEdges(int from, int to, Action<EdgeDetails> action)
        {
            (EdgeDetails e1, EdgeDetails e2) = GetEdges(from, to);
            if(e1==null || e2==null)
            {
                return;
            }
            action(e1);
            action(e2);
        }

        public void UnvisitAllNodes() =>
            NodeList.ForEach(n => n.visited = false);

        public void ClearPheromone()
        {
            foreach (var item in AdjList)
            {
                item.Value.ForEach(e => e.Pheromone = INITIAL_PHEROMONE);
            }
        }

        public void UpdatePheromone(int from, int to, double pheromone)
        {
            AdjList[from].First(e => e.ToId == to).Pheromone = pheromone;
            AdjList[to].First(e => e.ToId == from).Pheromone = pheromone;
        }

        public void PrintPheromones()
        {
            Console.WriteLine("Pheromones");

            for(int i = 0; i < NodeList.Count; ++i)
            {
                for(int j = 0; j < NodeList.Count - 1; ++j)
                {
                    if(j == i)
                    {
                        Console.Write("xxxx ");
                    }

                    Console.Write($"{AdjList[i][j].Pheromone:0.##} ");
                }
                Console.Write("\n");
            }
            Console.WriteLine("End pheromones");
        }
    }
}
