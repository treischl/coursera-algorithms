using CommandLine;

namespace Algorithms.Verbs
{
    [Verb("dijkstra", HelpText = "Calculate the shortest paths from a source vertex to all other accessible vertices in a graph.")]
    public class DijkstraOptions
    {
        [Option('f', "file", HelpText = "Path to an text file representation of an undirected weighted graph in adjacency list format.", Required = true)]
        public string File { get; set; } = string.Empty;

        [Option('s', "source-label", HelpText = "Label of the source vertex.", Required = true)]
        public int SourceLabel { get; set; } = -1;
    }
}
