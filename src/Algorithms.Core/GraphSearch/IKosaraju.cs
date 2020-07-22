namespace Algorithms.Core.GraphSearch
{
    public interface IKosaraju
    {
        int[] GetSccGroupSizes(DirectedAdjacencyList inputGraph);
    }
}
