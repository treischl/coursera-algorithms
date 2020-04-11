using System;

namespace Algorithms.Core.DivideAndConquer
{
    public interface IInversionCounter
    {
        long CountInversions(Span<int> integers);
    }
}
