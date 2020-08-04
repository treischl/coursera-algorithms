using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Algorithms.Core.GraphSearch
{
    public class TwoSum : ITwoSum
    {
        public IEnumerable<long> GetDistinctSums(
            IDictionary<long, long> numbers,
            IEnumerable<long> targets
        )
        {
            var distinctSums = new ConcurrentBag<long>();

            Parallel.ForEach(targets, target =>
            {
                if (HasTarget(numbers, target))
                {
                    distinctSums.Add(target);
                }
            });

            return distinctSums;
        }

        private bool HasTarget(IDictionary<long, long> numbers, long target)
        {
            foreach (var number in numbers.Keys)
            {
                if (numbers.ContainsKey(target - number))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
