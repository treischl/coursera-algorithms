using System.Collections.Generic;

namespace Algorithms.Core.GraphSearch
{
    public interface IMedianFileParser
    {
        IAsyncEnumerable<int> ParseFile(string filePath);
    }
}
