using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSI2.Helpers
{
    public class CmdOptions
    {
        [Option('i', "input", Required = true, HelpText = "Input file")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file")]
        public string OutputFile { get; set; }

        [Option('a', "algorithm", Required = true, HelpText = "Algorithm type")]
        public int AlgorithmType { get; set; }
    }
}
