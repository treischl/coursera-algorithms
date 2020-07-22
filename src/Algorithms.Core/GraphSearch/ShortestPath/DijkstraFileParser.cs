using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms.Core.GraphSearch.ShortestPath
{
    public class DijkstraFileParser : IDijkstraFileParser
    {
        public async Task<DijkstraAdjacencyList> ParseFile(string filePath)
        {
            var inputGraph = new DijkstraAdjacencyList();
            int numVertices = GetNumberOfVertices(filePath);
            InitializeGraph(inputGraph, numVertices);
            await PopulateInputGraph(inputGraph, filePath).ConfigureAwait(false);
            return inputGraph;
        }

        private async Task PopulateInputGraph(DijkstraAdjacencyList inputGraph, string filePath)
        {
            var edges = new List<DijkstraEdge>();
            await ParseEdgesFromFile(edges, filePath, inputGraph.Vertices).ConfigureAwait(false);
            AddEdgesToGraph(edges, inputGraph);
        }

        private void AddEdgesToGraph(List<DijkstraEdge> edges, DijkstraAdjacencyList graph)
        {
            var edgeComparer = new DijkstraEdgeComparer();
            var distinctEdges = edges.Distinct(edgeComparer).ToArray();
            foreach (var edge in distinctEdges)
            {
                edge.Vertices[0].Edges.Add(edge);
                edge.Vertices[1].Edges.Add(edge);
            }
            graph.Edges.AddRange(distinctEdges);
        }

        private async Task ParseEdgesFromFile(
            List<DijkstraEdge> edges,
            string filePath,
            List<DijkstraVertex> vertices
        )
        {
            using var stream = File.OpenRead(filePath);
            using var reader = new StreamReader(stream);

            string line = null;
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null
                && !string.IsNullOrWhiteSpace(line))
            {
                var lineParts = line.Split('\t');
                var headLabel = int.Parse(lineParts[0]);
                var lineEdges = lineParts[1..]
                    .Select(TailTupleSelector)
                    .Select(tail => new DijkstraEdge(
                        vertexA: vertices[headLabel - 1],
                        vertexB: vertices[tail.label - 1],
                        length: tail.distance
                    ));
                edges.AddRange(lineEdges);
            }

            static (int label, int distance) TailTupleSelector(string tupleString)
            {
                var parts = tupleString.Split(',').Select(part => int.Parse(part)).ToArray();
                return (parts[0], parts[1]);
            }
        }

        private void InitializeGraph(DijkstraAdjacencyList graph, int numberOfVertices)
        {
            for (var i = 1; i <= numberOfVertices; i++)
            {
                var vertex = new DijkstraVertex(i);
                graph.Vertices.Add(vertex);
            }
        }

        private int GetNumberOfVertices(string filePath)
        {
            using var inputFileStream = File.OpenRead(filePath);
            var lastLine = inputFileStream.ReadLastLine();
            if (lastLine == null)
            {
                throw new ArgumentException("Invalid Dijkstra graph file");
            }
            return int.Parse(lastLine.Split('\t')[0]);
        }
    }
}
