using System.Collections.Generic;

namespace Algorithms.Core.GraphSearch.ShortestPath
{
    public class DijkstraVertex
    {
        public int Label { get; }
        public List<DijkstraEdge> Edges { get; } = new List<DijkstraEdge>();

        public DijkstraVertex(int label)
        {
            Label = label;
        }
    }
}