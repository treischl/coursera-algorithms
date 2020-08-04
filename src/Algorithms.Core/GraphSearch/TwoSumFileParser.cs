using System.Collections.Generic;
using System.IO;

namespace Algorithms.Core.GraphSearch
{
    public class TwoSumFileParser : ITwoSumFileParser
    {
        public async IAsyncEnumerable<long> ParseFile(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            using var reader = new StreamReader(stream);

            string line = null;
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                if (long.TryParse(line, out var number))
                {
                    yield return number;
                }
            }
        }
    }
}
