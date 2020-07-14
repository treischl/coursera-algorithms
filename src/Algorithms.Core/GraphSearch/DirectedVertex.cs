using System.Collections.Generic;

namespace Algorithms.Core.GraphSearch
{
    public class DirectedVertex
    {
        public int Label { get; }
        public List<DirectedEdge> EdgesIn { get; } = new List<DirectedEdge>();
        public List<DirectedEdge> EdgesOut { get; } = new List<DirectedEdge>();

        public DirectedVertex(int label)
        {
            Label = label;
        }
    }
}
