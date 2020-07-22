using System.Collections.Generic;

namespace Algorithms.Core.GraphSearch
{
    public class DirectedVertex
    {
        public int Label { get; }
        public List<DirectedEdge> EdgesIn { get; } = new List<DirectedEdge>();
        public List<DirectedEdge> EdgesOut { get; } = new List<DirectedEdge>();
        public bool Explored { get; set; }
        public int SccLeader { get; set; }

        public DirectedVertex(int label)
        {
            Label = label;
        }
    }
}
