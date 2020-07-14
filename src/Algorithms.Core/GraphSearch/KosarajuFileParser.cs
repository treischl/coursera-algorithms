using System;
using System.IO;
using System.Linq;

namespace Algorithms.Core.GraphSearch
{
    public class KosarajuFileParser
    {
        public DirectedAdjacencyList ParseFile(string filePath)
        {
            var inputGraph = new DirectedAdjacencyList();

            int numVertices = GetNumberOfVertices(filePath);
            InitializeGraph(inputGraph, numVertices);
            PopulateInputGraph(inputGraph, filePath);

            return inputGraph;
        }

        private int GetNumberOfVertices(string inputFile)
        {
            using var inputFileStream = File.OpenRead(inputFile);
            var lastLine = inputFileStream.ReadLastLine();
            if (lastLine == null)
            {
                throw new ArgumentException("Invalid input file");
            }

            return int.Parse(lastLine.Split(' ')[0]);
        }

        private void InitializeGraph(DirectedAdjacencyList graph, int numberOfVertices)
        {
            for (var i = 0; i <= numberOfVertices - 1; i++)
            {
                var vertex = new DirectedVertex(i + 1);
                graph.Vertices.Add(vertex);
            }
        }

        private void PopulateInputGraph(DirectedAdjacencyList graph, string filePath)
        {
            using var stream = File.OpenRead(filePath);
            using var reader = new StreamReader(stream);

            string line;
            while ((line = reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
            {
                var vertexLabels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x))
                    .ToArray();
                var edge = new DirectedEdge(
                    tail: graph.Vertices[vertexLabels[0] - 1],
                    head: graph.Vertices[vertexLabels[1] - 1]
                );
                edge.Tail.EdgesOut.Add(edge);
                edge.Head.EdgesIn.Add(edge);
                graph.Edges.Add(edge);
            }
        }
    }
}
