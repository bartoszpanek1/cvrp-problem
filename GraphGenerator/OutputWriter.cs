using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerator
{
    public static class OutputWriter
    {
        public static async Task WriteOutput(string path, List<int> demands, int[,] matrix)
        {
            try
            {
                await TryWriteOutput(path, demands, matrix);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static async Task TryWriteOutput(string path, List<int> demands, int[,] matrix)
        {
            using StreamWriter writer = File.CreateText(path);

            await WriteDemandsLine(writer, demands);
            await WriteMatrix(writer, matrix);
        }

        private static async Task WriteDemandsLine(StreamWriter writer, List<int> demands)
        {
            StringBuilder sb = new StringBuilder();
            demands.ForEach(d => sb.Append($"{d} "));

            await writer.WriteLineAsync(sb.ToString().Trim());
        }

        private static async Task WriteMatrix(StreamWriter writer, int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); ++i)
            {
                for (int j = 0; j < m.GetLength(1); ++j)
                {
                    await writer.WriteAsync($"{m[i, j]}");
                    if(j != m.GetLength(1) - 1)
                        await writer.WriteAsync(" ");
                }

                await writer.WriteAsync("\n");
            }
        }
    }
}
