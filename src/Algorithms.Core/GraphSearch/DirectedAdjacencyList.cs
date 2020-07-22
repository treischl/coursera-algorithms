using System.Collections.Generic;

namespace Algorithms.Core.GraphSearch
{
    public class DirectedAdjacencyList
    {
        public List<DirectedVertex> Vertices { get; } = new List<DirectedVertex>();
        public List<DirectedEdge> Edges { get; } = new List<DirectedEdge>();
    }
}
