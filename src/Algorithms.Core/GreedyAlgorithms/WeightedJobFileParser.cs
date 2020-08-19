using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Algorithms.Core.GreedyAlgorithms
{
    public class WeightedJobFileParser : IWeightedJobFileParser
    {
        public async IAsyncEnumerable<WeightedJob> ParseFile(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            using var reader = new StreamReader(stream);

            string line = null;
            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                var jobParts = (line ?? "").Split(' ')
                    .Where(part => int.TryParse(part, out var _))
                    .Select(part => int.Parse(part))
                    .ToArray();
                if (jobParts.Length == 2)
                {
                    yield return new WeightedJob
                    {
                        Weight = jobParts[0],
                        Length = jobParts[1],
                    };
                }
            }
        }
    }
}
