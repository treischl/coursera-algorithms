namespace Algorithms.Core.GraphSearch
{
    public interface IKosarajuFileParser
    {
        DirectedAdjacencyList ParseFile(string filePath);
    }
}
