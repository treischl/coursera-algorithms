using System;

namespace Algorithms.Core.DivideAndConquer
{
    public interface IQuickSorter
    {
        int CountComparisons(Span<int> integers, PivotChoice pivotChoice);
    }
}
