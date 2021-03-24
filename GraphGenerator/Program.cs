using CommandLine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Random rand = new Random();

            ReadArgs(
                args,
                out string outputPath,
                out int verticesNumber,
                out int maxDemand,
                out int maxWeight);

            List<int> demands = GraphCreator.GenerateDemands(verticesNumber, maxDemand);
            int[,] weights = GraphCreator.GeneratetMatrix(verticesNumber, maxWeight);

            await OutputWriter.WriteOutput(outputPath, demands, weights);
        }

        private static void ReadArgs(
            string[] args,
            out string outputPath,
            out int verticesNumber,
            out int maxDemand,
            out int maxWeight)
        {
            string output = string.Empty;
            int v = 0, d = 0, w = 0;

            Parser.Default.ParseArguments<GraphCmdOptions>(args)
                   .WithParsed(o =>
                   {
                       output = o.OutputFile;
                       v = o.VerticesNumber;
                       d = o.MaxDemand;
                       w = o.MaxWeight;
                   });

            outputPath = output;
            verticesNumber = v;
            maxDemand = d;
            maxWeight = w;
        }
    }
}
