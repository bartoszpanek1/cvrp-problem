using System.Threading.Tasks;
using CommandLine;
using MSI2.Algorithms;
using MSI2.Helpers;

namespace MSI2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ReadArgs(args, out string inputPath, out string outputPath, out AlgorithmType algType);

            ICVRPSolver solver = SolversFactory.CreateSolver(algType);

        }

        private static void ReadArgs(string[] args, out string inputPath, out string outputPath, out AlgorithmType algType)
        {
            string input = string.Empty, output = string.Empty;
            int type = 0;

            Parser.Default.ParseArguments<CmdOptions>(args)
                   .WithParsed(o =>
                   {
                       input = o.InputFile;
                       output = o.OutputFile;
                       type = o.AlgorithmType;
                   });

            inputPath = input;
            outputPath = output;
            algType = (AlgorithmType)type;
        }
    }
}
