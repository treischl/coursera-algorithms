using System.Collections.Generic;

namespace Algorithms.Core.GraphSearch.ShortestPath
{
    public interface IDijkstra
    {
        IDictionary<DijkstraVertex, int> CalculateShortestPaths(DijkstraAdjacencyList graph, int sourceLabel);
    }
}
