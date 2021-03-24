using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphGenerator
{
    public class GraphCmdOptions
    {

        [Option('o', "output", Required = true, HelpText = "Output file")]
        public string OutputFile { get; set; }

        [Option('v', "vertives", Required = true, HelpText = "Number of vertices")]
        public int VerticesNumber { get; set; }

        [Option('d', "demand", Required = true, HelpText = "Max demand")]
        public int MaxDemand { get; set; }

        [Option('w', "weight", Required = true, HelpText = "Max edge weight")]
        public int MaxWeight { get; set; }
    }
}
