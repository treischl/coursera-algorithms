using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Core.GraphSearch.ShortestPath
{
    public class Dijkstra : IDijkstra
    {
        public IDictionary<DijkstraVertex, int> CalculateShortestPaths(
            DijkstraAdjacencyList graph,
            int sourceLabel
        )
        {
            var sourceVertex = graph.Vertices.First(x => x.Label == sourceLabel);
            var processedVertices = new List<DijkstraVertex> { sourceVertex };
            var shortestPaths = graph.Vertices.ToDictionary(
               vertex => vertex,
               vertex => vertex.Label == sourceLabel ? 0 : int.MaxValue
            );

            DijkstraEdge[] frontierEdges = graph.Edges
                .Where(edge =>
                    processedVertices.Contains(edge.Vertices[0]) !=
                    processedVertices.Contains(edge.Vertices[1])
                )
                .ToArray();
            while (frontierEdges.Length > 0)
            {
                var lengths = frontierEdges.ToDictionary(
                    edge => edge,
                    edge =>
                    {
                        var processedVertex = edge.Vertices
                            .First(vertex => processedVertices.Contains(vertex));
                        var notProcessedVertex = edge.Vertices
                            .First(vertex => !processedVertices.Contains(vertex));

                        return shortestPaths[processedVertex] + edge.Length;
                    }
                );
                var shortestPath = lengths.Values.Min();
                var edgeWithShortestPath = lengths.First(kvp => kvp.Value == shortestPath).Key;
                var notProcessedVertex = edgeWithShortestPath.Vertices
                            .First(vertex => !processedVertices.Contains(vertex));
                processedVertices.Add(notProcessedVertex);
                shortestPaths[notProcessedVertex] = shortestPath;

                frontierEdges = graph.Edges
                    .Where(edge =>
                        processedVertices.Contains(edge.Vertices[0]) !=
                        processedVertices.Contains(edge.Vertices[1])
                    )
                    .ToArray();
            }

            return shortestPaths;
        }
    }
}
