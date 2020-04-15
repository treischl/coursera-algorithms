using System;

namespace Algorithms.Core.DivideAndConquer
{
    public class InversionCounter : IInversionCounter
    {
        public long CountInversions(Span<int> integers)
        {
            if (integers.Length <= 1)
            {
                return 0;
            }
            else
            {
                var left = integers.Slice(0, integers.Length / 2);
                var right = integers.Slice(integers.Length / 2);
                var leftInv = CountInversions(left);
                var rightInv = CountInversions(right);
                var splitInv = MergeAndCountSplitInv(left, right);

                return leftInv + rightInv + splitInv;
            }
        }

        private long MergeAndCountSplitInv(Span<int> left, Span<int> right)
        {
            var leftCtr = 0;
            var rightCtr = 0;
            var splitInv = 0;
            var sorted = new int[left.Length + right.Length].AsSpan();

            for (var ctr = 0; ctr < sorted.Length; ctr++)
            {
                if (leftCtr >= left.Length)
                {
                    sorted[ctr] = right[rightCtr++];
                }
                else if (rightCtr >= right.Length
                    || left[leftCtr] < right[rightCtr])
                {
                    sorted[ctr] = left[leftCtr++];
                }
                else
                {
                    sorted[ctr] = right[rightCtr++];
                    splitInv += left.Length - leftCtr;
                }
            }

            sorted.Slice(0, left.Length).CopyTo(left);
            sorted.Slice(left.Length).CopyTo(right);

            return splitInv;
        }
    }
}
