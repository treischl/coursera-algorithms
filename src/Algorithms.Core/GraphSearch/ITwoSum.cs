using System.Collections.Generic;

namespace Algorithms.Core.GraphSearch
{
    public interface ITwoSum
    {
        IEnumerable<long> GetDistinctSums(
            IDictionary<long, long> numbers,
            IEnumerable<long> targets
        );
    }
}