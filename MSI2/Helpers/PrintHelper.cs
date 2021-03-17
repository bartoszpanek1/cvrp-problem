using MSI2.Graphs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSI2.Helpers
{
    public static class PrintHelper
    {
        public static void PrintNodeList(List<Node> nodes)
        {
            StringBuilder sb = new StringBuilder();
            nodes?.ForEach(n => sb.Append($"{n.id} "));
            Console.WriteLine(sb.ToString());
        }

        public static void PrintNodeListList(List<List<Node>> nodes)
        {
            foreach (var vehicle in nodes)
            {
                Console.WriteLine(LineForVehicle(vehicle));
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
