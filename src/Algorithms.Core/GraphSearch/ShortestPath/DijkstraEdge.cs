namespace Algorithms.Core.GraphSearch.ShortestPath
{
    public class DijkstraEdge
    {
        public DijkstraVertex[] Vertices { get; }
        public int Length { get; }

        public DijkstraEdge(DijkstraVertex vertexA, DijkstraVertex vertexB, int length)
        {
            Vertices = new DijkstraVertex[] { vertexA, vertexB };
            Length = length;
        }
    }
}