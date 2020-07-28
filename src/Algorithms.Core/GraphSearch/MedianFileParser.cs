using System.Collections.Generic;
using System.IO;

namespace Algorithms.Core.GraphSearch
{
    public class MedianFileParser : IMedianFileParser
    {
        public async IAsyncEnumerable<int> ParseFile(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            using var reader = new StreamReader(stream);

            string line = null;
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                if (int.TryParse(line, out var number))
                {
                    yield return number;
                }
            }
        }
    }
}
