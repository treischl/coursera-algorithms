using Algorithms.Core.DataStructures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Algorithms.Core.GraphSearch
{
    public class MedianMaintenance : IMedianMaintenance
    {
        public async Task<long> CalculateSumOfMedians(IAsyncEnumerable<int> numbers)
        {
            long medianSum = 0;
            var heap1 = new MaxHeap<int>();
            var heap2 = new MinHeap<int>();

            await foreach (var number in numbers)
            {
                var heap1Max = heap1.Root();
                var heap2Min = heap2.Root();

                if (number <= heap1Max || heap1.Size == 0)
                {
                    heap1.Insert(number);
                }
                else
                {
                    heap2.Insert(number);
                }

                var sizeDifference = heap1.Size - heap2.Size;
                if (sizeDifference == 2)
                {
                    heap2.Insert(heap1.ExtractRoot());
                }
                else if (sizeDifference < 0)
                {
                    heap1.Insert(heap2.ExtractRoot());
                }

                medianSum += heap1.Root();
            }

            return medianSum;
        }
    }
}
