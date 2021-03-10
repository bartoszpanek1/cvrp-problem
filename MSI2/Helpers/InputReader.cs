using MSI2.Graphs;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MSI2.Helpers
{
    public static class InputReader
    {
        public static async Task<Graph> ReadInput(string path)
        {
            try
            {
                return await TryReadInput(path);
            }
            catch (Exception e) when(
                e is FileNotFoundException
                || e is FormatException
                || e is IndexOutOfRangeException)
            {
                Console.WriteLine("Incorrect input file");
                throw;
            }
        }

        private static async Task<Graph> TryReadInput(string path)
        {
            using StreamReader reader = File.OpenText(path);

            string line = await reader.ReadLineAsync();
            int[] capacities = line.Split(' ').Select(c => int.Parse(c)).ToArray();
            int n = capacities.Length + 1;
            Graph graph = new Graph(capacities);
            line = await reader.ReadLineAsync();

            for (int i = 0; i < n; ++i)
            {
                int[] distances = line.Split(' ').Select(c => int.Parse(c)).ToArray();

                for (int j = i + 1; j < n; ++j)
                {
                    graph.AddEdge(i, j, distances[j]);
                }

                line = await reader.ReadLineAsync();
            }

            return graph;
        }
    }
}
