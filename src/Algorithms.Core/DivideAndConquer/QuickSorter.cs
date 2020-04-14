using System;

namespace Algorithms.Core.DivideAndConquer
{
    public class QuickSorter : IQuickSorter
    {
        private int Comparisons = -1;

        public int CountComparisons(Span<int> integers, PivotChoice pivotChoice)
        {
            Comparisons = 0;
            Sort(integers, 0, integers.Length - 1, pivotChoice);
            return Comparisons;
        }

        private void Sort(Span<int> integers, int left, int right, PivotChoice pivotChoice)
        {
            if (left >= right)
            {
                return;
            }

            var i = ChoosePivot(integers, left, right, pivotChoice);
            Swap(integers, left, i);
            var j = Partition(integers, left, right);
            Sort(integers, left, j - 1, pivotChoice);
            Sort(integers, j + 1, right, pivotChoice);
        }

        private int Partition(Span<int> integers, int left, int right)
        {
            Comparisons += right - left;
            var p = integers[left];
            var i = left + 1;
            for (var j = left + 1; j <= right; j++)
            {
                if (integers[j] < p)
                {
                    Swap(integers, i, j);
                    i++;
                }
            }
            Swap(integers, left, i - 1);
            return i - 1;
        }

        private void Swap(Span<int> integers, int x, int y)
        {
            var temp = integers[x];
            integers[x] = integers[y];
            integers[y] = temp;
        }

        private int ChoosePivot(Span<int> integers, int left, int right, PivotChoice pivotChoice)
        {
            switch (pivotChoice)
            {
                case PivotChoice.LeftMost:
                    return left;
                case PivotChoice.RightMost:
                    return right;
                case PivotChoice.MedianOfThree:
                    var length = right - left + 1;
                    var mid = length % 2 == 0
                        ? left + (length / 2) - 1
                        : left + (length / 2);
                    if ((integers[left] > integers[mid]) != (integers[left] > integers[right]))
                    {
                        return left;
                    }
                    else if ((integers[mid] > integers[left]) != (integers[mid] > integers[right]))
                    {
                        return mid;
                    }
                    else
                    {
                        return right;
                    }
                default:
                    return new Random().Next(left, right + 1);
            }
        }
    }
}
