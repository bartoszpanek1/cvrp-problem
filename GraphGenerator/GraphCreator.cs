using System;
using System.Collections.Generic;
using System.Linq;


namespace GraphGenerator
{
    public static class GraphCreator
    {
        public static List<int> GenerateDemands(int verticesNumber, int maxDemand)
        {
            Random rand = new Random();
            return Enumerable.Range(0, verticesNumber - 1).Select(n => rand.Next(1, maxDemand)).ToList();
        }

        public static int[,] GeneratetMatrix(int n, int maxWeight)
        {
            Random rand = new Random();
            int[,] m = new int[n, n];

            for (int i = 0; i < m.GetLength(0); ++i)
            {
                for (int j = i + 1; j < m.GetLength(1); ++j)
                {
                    m[i, j] = rand.Next(1, maxWeight);
                }
            }

            return m;
        }
    }
}
