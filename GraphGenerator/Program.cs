using CommandLine;
using System;

namespace GraphGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadArgs(
                args,
                out string outputPath,
                out int verticesNumber,
                out int maxDemand,
                out int maxWeight);


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
