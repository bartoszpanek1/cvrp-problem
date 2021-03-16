using MSI2.Graphs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MSI2.Helpers
{
    public static class OutputWriter
    {
        public static async Task WriteOutput(string path, List<List<Node>> visitedNodes, int totalDistance)
        {
            try
            {
                await TryWriteOutput(path, visitedNodes, totalDistance);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static async Task TryWriteOutput(string path, List<List<Node>> vehicles, int totalDistance)
        {
            using StreamWriter writer = File.CreateText(path);

            if (vehicles == null)
            {
                await writer.WriteLineAsync("No result has been found");
                return;
            }

            await WriteTotalDistance(writer, totalDistance);

            await PrintVehicles(writer, vehicles);
        }

        private static async Task WriteTotalDistance(StreamWriter writer, int totalDistance) =>
            await writer.WriteLineAsync($"Total distance: {totalDistance}");

        private static async Task PrintVehicles(StreamWriter writer, List<List<Node>> vehicles)
        {
            foreach (var vehicle in vehicles)
            {
                await writer.WriteLineAsync(LineForVehicle(vehicle));
            }
        }

        private static string LineForVehicle(List<Node> vehicle)
        {
            StringBuilder sb = new StringBuilder();
            vehicle.ForEach(n => sb.Append($"{n.id} "));
            return sb.ToString();
        }
    }
}
