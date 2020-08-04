using System.Collections.Generic;

namespace Algorithms.Core.GraphSearch
{
    public interface ITwoSumFileParser
    {
        IAsyncEnumerable<long> ParseFile(string filePath);
    }
}
