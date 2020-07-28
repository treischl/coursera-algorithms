using System.Threading.Tasks;

namespace Algorithms.Core.GraphSearch.ShortestPath
{
    public interface IDijkstraFileParser
    {
        Task<DijkstraAdjacencyList> ParseFile(string filePath);
    }
}
