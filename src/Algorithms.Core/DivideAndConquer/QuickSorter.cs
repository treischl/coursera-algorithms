using System;

namespace Algorithms.Core.DivideAndConquer
{
    public class QuickSorter : IQuickSorter
    {
        public void SortInPlace(Span<int> integers, PivotChoice pivotChoice, ref int comparisons)
        {
            comparisons = SortInteral(integers, pivotChoice);
        }

        public int SortInteral(Span<int> integers, PivotChoice pivotChoice)
        {
            if (integers.Length <= 1)
            {
                return 0;
            }

            var i = ChoosePivot(integers, pivotChoice);
            Swap(integers, 0, i);
            var j = Partition(integers);
            var comparisons = integers.Length - 1;
            if (j > 0)
                comparisons += SortInteral(integers.Slice(0, j), pivotChoice);
            comparisons += SortInteral(integers.Slice(j + 1), pivotChoice);
            return comparisons;
        }

        private int Partition(Span<int> integers)
        {
            var p = integers[0];
            var i = 1;
            for (var j = 1; j <= integers.Length - 1; j++)
            {
                if (integers[j] < p)
                {
                    Swap(integers, i, j);
                    i++;
                }
            }
            Swap(integers, 0, i - 1);
            return i - 1;
        }

        private void Swap(Span<int> integers, int x, int y)
        {
            var temp = integers[x];
            integers[x] = integers[y];
            integers[y] = temp;
        }

        private readonly Lazy<Random> _pivotRandomizer = new Lazy<Random>();

        private int ChoosePivot(Span<int> integers, PivotChoice pivotChoice)
        {
            switch (pivotChoice)
            {
                case PivotChoice.LeftMost:
                    return 0;
                case PivotChoice.RightMost:
                    return integers.Length - 1;
                case PivotChoice.MedianOfThree:
                    var mid = integers.Length % 2 == 0
                        ? (integers.Length / 2) - 1
                        : integers.Length / 2;
                    if ((integers[0] > integers[mid]) != (integers[0] > integers[^1]))
                    {
                        return 0;
                    }
                    else if ((integers[mid] > integers[0]) != (integers[mid] > integers[^1]))
                    {
                        return mid;
                    }
                    else
                    {
                        return integers.Length - 1;
                    }
                default:
                    return _pivotRandomizer.Value.Next(0, integers.Length);
            }
        }
    }
}
