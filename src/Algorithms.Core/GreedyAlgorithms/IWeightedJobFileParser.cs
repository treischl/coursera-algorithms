using System.Collections.Generic;

namespace Algorithms.Core.GreedyAlgorithms
{
    public interface IWeightedJobFileParser
    {
        IAsyncEnumerable<WeightedJob> ParseFile(string filePath);
    }
}
