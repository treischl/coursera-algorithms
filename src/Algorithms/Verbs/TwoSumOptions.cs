using CommandLine;

namespace Algorithms.Verbs
{
    [Verb("two-sum", HelpText = "Calculate the number of distinct target values which can be produced from a list of integers.")]
    public class TwoSumOptions
    {
        [Option('f', "file", HelpText = "Path to a text file with a list of integers.", Required = true)]
        public string File { get; set; } = string.Empty;

        [Option('s', "start", HelpText = "Starting integer for the target range.")]
        public long Start { get; set; } = -10000;

        [Option('e', "end", HelpText = "Ending integer for the target range.")]
        public long End { get; set; } = 10000;
    }
}
