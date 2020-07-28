using CommandLine;

namespace Algorithms.Verbs
{
    [Verb("median-maintenance", HelpText = "Calculate the sum of medians from a list of integers")]
    public class MedianMaintenanceOptions
    {
        [Option('f', "file", HelpText = "Path to a text file with a list of integers.", Required = true)]
        public string File { get; set; } = string.Empty;
    }
}
