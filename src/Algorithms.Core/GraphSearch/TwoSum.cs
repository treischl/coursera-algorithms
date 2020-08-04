using System.Collections.Generic;

namespace Algorithms.Core.GraphSearch
{
    public class TwoSum : ITwoSum
    {
        public IEnumerable<long> GetDistinctSums(
            IDictionary<long, long> numbers,
            IEnumerable<long> targets
        )
        {
            foreach (var target in targets)
            {
                if (HasTarget(numbers, target))
                {
                    yield return target;
                }
            }
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
