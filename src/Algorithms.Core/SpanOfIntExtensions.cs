using System;
using System.Linq;

namespace Algorithms.Core
{
    public static class SpanOfIntExtensions
    {
        public static Span<int> ToSpanOfInt(this string numericString)
        {
            if (!numericString.All(c => int.TryParse(c.ToString(), out var _)))
            {
                throw new ArgumentException($"Value must be numeric: {numericString}");
            }

            return numericString.Select(c => int.Parse(c.ToString()))
                .ToArray()
                .AsSpan();
        }

        public static void EnsureSameSize(this Span<int> spanA, ref Span<int> spanB)
        {
            if (spanA.Length > spanB.Length)
            {
                spanB = new int[spanA.Length - spanB.Length].AsSpan().Concat(spanB);
            }
        }

        public static Span<int> Concat(this Span<int> span, Span<int> elements)
        {
            var concated = new int[span.Length + elements.Length].AsSpan();
            span.CopyTo(concated);
            elements.CopyTo(concated.Slice(span.Length));
            return concated;
        }

        public static Span<int> AddTo(this Span<int> spanA, Span<int> spanB)
        {
            spanA.EnsureSameSize(ref spanB);
            spanB.EnsureSameSize(ref spanA);

            var result = new int[spanA.Length].AsSpan();
            for (var i = spanA.Length - 1; i >= 0; i--)
            {
                var sum = result[i] + spanA[i] + spanB[i];
                if (sum > 0) { result[i] = 0; }
                var tensDigit = sum / 10;
                if (tensDigit == 0)
                {
                    result[i] += sum;
                }
                else if (i == 0)
                {
                    result = new int[1] { tensDigit }.AsSpan().Concat(result);
                    result[1] += sum % 10;
                }
                else
                {
                    result[i] += sum % 10;
                    result[i - 1] += tensDigit;
                }
            }
            return result;
        }

        public static Span<int> Subtract(this Span<int> spanA, Span<int> spanB)
        {
            spanA.EnsureSameSize(ref spanB);
            spanB.EnsureSameSize(ref spanA);

            if (spanA.CompareTo(spanB) < 0)
            {
                var temp = spanB;
                spanB = spanA;
                spanA = temp;
            }

            var result = new int[spanA.Length].AsSpan();
            for (var i = spanA.Length - 1; i >= 0; i--)
            {
                while (spanA[i] < spanB[i])
                {
                    spanA[i - 1]--;
                    spanA[i] += 10;
                }

                result[i] = spanA[i] - spanB[i];
            }

            return result;
        }

        public static int CompareTo(this Span<int> a, Span<int> b)
        {
            a.EnsureSameSize(ref b);
            b.EnsureSameSize(ref a);

            for (var i = 0; i < a.Length; i++)
            {
                if (a[i] > b[i]) { return 1; }
                if (a[i] < b[i]) { return -1; }
            }
            return 0;
        }
    }
}
