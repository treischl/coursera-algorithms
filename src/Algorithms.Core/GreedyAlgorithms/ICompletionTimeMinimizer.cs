using System.Collections.Generic;

namespace Algorithms.Core.GreedyAlgorithms
{
    public interface ICompletionTimeMinimizer
    {
        long MinimizeWeightedSum(IEnumerable<WeightedJob> jobs, IComparer<WeightedJob> comparer);
    }
}
