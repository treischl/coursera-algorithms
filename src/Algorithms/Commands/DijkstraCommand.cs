using Algorithms.Core.GraphSearch.ShortestPath;
using Algorithms.Verbs;
using System;

namespace Algorithms.Commands
{
    public class DijkstraCommand : ICommand<DijkstraOptions>
    {
        private readonly IDijkstra _dijkstra;
        private readonly IDijkstraFileParser _parser;

        public DijkstraCommand(
            IDijkstra dijkstra,
            IDijkstraFileParser parser)
        {
            _dijkstra = dijkstra;
            _parser = parser;
        }

        public void Execute(DijkstraOptions options)
        {
            var graph = _parser.ParseFile(options.File).GetAwaiter().GetResult();
            var results = _dijkstra.CalculateShortestPaths(graph, options.SourceLabel);

            foreach (var result in results)
            {
                Console.WriteLine($"{result.Key.Label}: {result.Value}");
            }
        }
    }
}
