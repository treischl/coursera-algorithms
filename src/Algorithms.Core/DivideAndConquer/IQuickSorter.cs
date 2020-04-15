using System;

namespace Algorithms.Core.DivideAndConquer
{
    public interface IQuickSorter
    {
        void SortInPlace(Span<int> integers, PivotChoice pivotChoice, ref int comparisons);
    }
}
