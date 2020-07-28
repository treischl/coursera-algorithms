using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Core.GraphSearch.ShortestPath
{
    public class DijkstraEdgeComparer : IEqualityComparer<DijkstraEdge>
    {
        public bool Equals(DijkstraEdge edge1, DijkstraEdge edge2)
        {
            if (edge1 == null && edge2 == null)
            {
                return true;
            }
            else if (edge1 == null || edge2 == null)
            {
                return false;
            }
            else if (edge1.Vertices.Contains(edge2.Vertices[0])
                && edge1.Vertices.Contains(edge2.Vertices[1])
                && edge1.Length == edge2.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(DijkstraEdge edge)
        {
            int hCode = edge.Vertices[0].Label ^ edge.Vertices[1].Label ^ edge.Length;
            return hCode.GetHashCode();
        }
    }
}
