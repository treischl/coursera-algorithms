using System;

namespace Algorithms.Core.DivideAndConquer
{
    public class Karatsuba : IKaratsuba
    {
        public Span<int> MultiplyXAndY(Span<int> x, Span<int> y)
        {
            x.EnsureSameSize(ref y);
            y.EnsureSameSize(ref x);
            if (x.Length > 1 && y.Length > 1)
            {
                EnsureLengthIsPowerOf2(ref x);
                EnsureLengthIsPowerOf2(ref y);
            }

            if (x.Length == 1)
            {
                return GetBaseCase(x[0], y[0]);
            }
            else
            {
                return GetRecursiveCase(x, y);
            }
        }

        private void EnsureLengthIsPowerOf2(ref Span<int> span)
        {
            if (span.Length % 2 == 1)
            {
                span = new int[1].AsSpan().Concat(span);
            }
        }

        private Span<int> GetBaseCase(int x, int y)
        {
            var product = x * y;
            return new int[]
            {
                product / 10,
                product % 10
            }.AsSpan();
        }

        private Span<int> GetRecursiveCase(Span<int> x, Span<int> y)
        {
            var a = x.Slice(0, x.Length / 2);
            var b = x.Slice(x.Length / 2);
            var c = y.Slice(0, y.Length / 2);
            var d = y.Slice(y.Length / 2);

            var ac = MultiplyXAndY(a, c);
            var bd = MultiplyXAndY(b, d);
            var step3 = MultiplyXAndY(a.AddTo(b), c.AddTo(d));
            var ad_bc = step3.Subtract(ac).Subtract(bd);

            var added = ac.Concat(new int[x.Length].AsSpan())
                .AddTo(ad_bc.Concat(new int[x.Length / 2].AsSpan()))
                .AddTo(bd);
            var firstNonZero = 0;
            for (; firstNonZero < added.Length; firstNonZero++)
                if (added[firstNonZero] != 0) break;
            return added.Slice(firstNonZero);
        }
    }
}
