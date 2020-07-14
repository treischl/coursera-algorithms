using CommandLine;

namespace Algorithms.Verbs
{
    [Verb("kosaraju", HelpText = "Compute the sizes of a graph's strongly connected components and returns them in descending order.")]
    public class KosarajuOptions
    {
        [Option('f', "file", HelpText = "Path to an text file representation of a directed acyclic graph.", Required = true)]
        public string File { get; set; } = string.Empty;

        [Option('n', "results", HelpText = "Number of results to display.")]
        public int ResultsCount { get; set; } = -1;
    }
}
