using System.Collections.Generic;
using System.Threading.Tasks;

namespace Algorithms.Core.GraphSearch
{
    public interface IMedianMaintenance
    {
        Task<long> CalculateSumOfMedians(IAsyncEnumerable<int> numbers);
    }
}
