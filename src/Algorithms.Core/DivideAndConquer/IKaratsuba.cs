using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Core.DivideAndConquer
{
    public interface IKaratsuba
    {
        Span<int> MultiplyXAndY(Span<int> x, Span<int> y);
    }
}
