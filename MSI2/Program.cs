using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CommandLine;
using MSI2.Algorithms;
using MSI2.Graphs;
using MSI2.Helpers;

namespace MSI2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ReadArgs(
                args,
                out string inputPath,
                out string outputPath,
                out AlgorithmType algType,
                out int numberOfVehicles,
                out int capacity,
                out int SMax);

            Graph graph = await InputReader.ReadInput(inputPath);
            ICVRPSolver solver = SolversFactory.CreateSolver(algType);

            Stopwatch stopwatch = Stopwatch.StartNew();
            (List<List<Node>> vehicles, int TotalDistance) = solver.Solve(graph, numberOfVehicles, capacity, SMax);
            stopwatch.Stop();
            Console.WriteLine($"Total time: {stopwatch.ElapsedMilliseconds} ms");

            await OutputWriter.WriteOutput(outputPath, vehicles, TotalDistance);
            Console.WriteLine($"Best total distance: {TotalDistance}");
            PrintHelper.PrintNodeListList(vehicles);
        }

        private static void ReadArgs(
            string[] args,
            out string inputPath,
            out string outputPath,
            out AlgorithmType algType,
            out int numberOfVehicles,
            out int capacity,
            out int SMax)
        {
            string input = string.Empty, output = string.Empty;
            int type = 0, n = 0, c = 0, s = 0;

            Parser.Default.ParseArguments<CmdOptions>(args)
                   .WithParsed(o =>
                   {
                       input = o.InputFile;
                       output = o.OutputFile;
                       type = o.AlgorithmType;
                       n = o.VehiclesNumber;
                       c = o.Capacity;
                       s = o.SMax;
                   });

            inputPath = input;
            outputPath = output;
            algType = (AlgorithmType)type;
            numberOfVehicles = n;
            capacity = c;
            SMax = s;
        }
    }
}
