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
            var (processed, frontier, scores) = InitializeCollections(graph, sourceLabel);

            while (frontier.Count > 0)
            {
                var (minEdge, minScore) = FindMinimizingEdge(processed, frontier, scores);
                var unprocessedVertex = minEdge.Vertices.First(vertex => !processed.Contains(vertex));
                processed.Add(unprocessedVertex);
                scores[unprocessedVertex] = minScore;
                UpdateFrontierEdges(frontier, processed, unprocessedVertex);
            }

            return scores;
        }

        private (
            List<DijkstraVertex> processedVertices,
            List<DijkstraEdge> frontierEdges,
            Dictionary<DijkstraVertex, int> shortestPaths
        ) InitializeCollections(DijkstraAdjacencyList graph, int sourceLabel)
        {
            var sourceVertex = graph.Vertices.First(vertex => vertex.Label == sourceLabel);
            var processedVertices = new List<DijkstraVertex>() { sourceVertex };
            var frontierEdges = new List<DijkstraEdge>(sourceVertex.Edges);
            var shortestPaths = graph.Vertices.ToDictionary(
                vertex => vertex,
                vertex => vertex == sourceVertex ? 0 : int.MaxValue);

            return (processedVertices, frontierEdges, shortestPaths);
        }

        private (DijkstraEdge edge, int score) FindMinimizingEdge(
            List<DijkstraVertex> processedVertices,
            List<DijkstraEdge> frontierEdges,
            Dictionary<DijkstraVertex, int> shortestPaths
        )
        {
            DijkstraEdge minEdge = null;
            var minScore = int.MaxValue;
            foreach (var edge in frontierEdges)
            {
                var processedVertex = processedVertices.Contains(edge.Vertices[0])
                    ? edge.Vertices[0] : edge.Vertices[1];
                var edgeScore = shortestPaths[processedVertex] + edge.Length;
                if (edgeScore < minScore)
                {
                    minEdge = edge;
                    minScore = edgeScore;
                }
            }
            return (minEdge, minScore);
        }

        private void UpdateFrontierEdges(
            List<DijkstraEdge> frontierEdges,
            List<DijkstraVertex> processedVertices,
            DijkstraVertex justProcessedVertex)
        {
            frontierEdges.RemoveAll(BothVerticesAreProcessed);
            var edgesToAdd = justProcessedVertex.Edges.Where(HasAnUnprocessedVertex);
            frontierEdges.AddRange(edgesToAdd);

            bool BothVerticesAreProcessed(DijkstraEdge edge) =>
                processedVertices.Contains(edge.Vertices[0]) &&
                processedVertices.Contains(edge.Vertices[1]);

            bool HasAnUnprocessedVertex(DijkstraEdge edge) =>
                !BothVerticesAreProcessed(edge);
        }
    }
}
