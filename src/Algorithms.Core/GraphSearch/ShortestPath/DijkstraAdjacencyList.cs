using System.Collections.Generic;

namespace Algorithms.Core.GraphSearch.ShortestPath
{
    public class DijkstraAdjacencyList
    {
        public List<DijkstraVertex> Vertices { get; } = new List<DijkstraVertex>();
        public List<DijkstraEdge> Edges { get; } = new List<DijkstraEdge>();
    }
}