namespace Algorithms.Core.GraphSearch
{
    public class DirectedEdge
    {
        public DirectedVertex Tail { get; }
        public DirectedVertex Head { get; }

        public DirectedEdge(DirectedVertex tail, DirectedVertex head)
        {
            Tail = tail;
            Head = head;
        }
    }
}
