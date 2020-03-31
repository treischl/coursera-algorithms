using CommandLine;
using System.Collections.Generic;

namespace Algorithms.Verbs
{
    [Verb("count-inversions", HelpText = "Count the number of inversions in an array of integers.")]
    public class CountInversionsOptions
    {
        [Option('i', SetName = "integers", HelpText = "The integers to find inversions in.")]
        public IEnumerable<int> Integers { get; set; } = new int[0];

        [Option('p', SetName = "integers", HelpText = "Path to a file with a integers to find inversions in.")]
        public string Path { get; set; } = string.Empty;
    }
}
