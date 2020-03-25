using Algorithms;
using System;
using Xunit;

namespace Algorithm.Tests
{
    public class SpanOfIntExtensionsTests
    {
        private static readonly Random s_rand = new Random();

        public class ToSpanOfInt
        {
            [Fact]
            public void WhenOnHappyPath_ReturnsStringAsSpanOfInt()
            {
                // arrange
                var numStr = s_rand.Next().ToString();
                var expected = new int[numStr.Length].AsSpan();
                for (var i = 0; i < expected.Length; i++)
                    expected[i] = int.Parse(numStr[i].ToString());

                // act
                var actual = numStr.ToSpanOfInt();

                // assert
                Utils.AssertAreEqualSpans(expected, actual);
            }

            [Fact]
            public void WhenNonNumericStringIsPassed_ThrowsArgumentException()
            {
                // arrange
                var numStr = s_rand.Next().ToString() + "A";

                try
                {
                    // act
                    var _ = numStr.ToSpanOfInt();
                    Assert.True(false, $"{nameof(SpanOfIntExtensions.ToSpanOfInt)} succeeded unexpectedly");
                }
                catch (Exception ex)
                {
                    // assert
                    Assert.IsType<ArgumentException>(ex);
                }
            }
        }

        public class EnsureSameSize
        {
            [Fact]
            public void WhenOnHappyPath_MakeSpansTheSameLength()
            {
                // arrange
                var spanA = new int[s_rand.Next(1, 5)].AsSpan();
                var spanB = new int[s_rand.Next(6, 10)].AsSpan();

                // act
                spanA.EnsureSameSize(ref spanB);
                spanB.EnsureSameSize(ref spanA);

                // assert
                Assert.Equal(spanA.Length, spanB.Length);
            }
        }

        public class Concat
        {
            [Fact]
            public void WhenOnHappyPath_ConcatenatesSpans()
            {
                // arrange
                var spanA = FillSpan(new int[s_rand.Next(5, 10)].AsSpan());
                var spanB = FillSpan(new int[s_rand.Next(5, 10)].AsSpan());
                var expected = new int[spanA.Length + spanB.Length].AsSpan();
                spanA.CopyTo(expected);
                spanB.CopyTo(expected.Slice(spanA.Length));

                // act
                var actual = spanA.Concat(spanB);

                // assert
                Utils.AssertAreEqualSpans(expected, actual);

                static Span<int> FillSpan(Span<int> span)
                {
                    for (var i = 0; i < span.Length; i++)
                        span[i] = s_rand.Next(0, 10);
                    return span;
                }
            }
        }

        public class AddTo
        {
            [Fact]
            public void WhenOnHappyPath_AddsElementsTogether()
            {
                // arrange
                var x = s_rand.Next(int.MaxValue / 2);
                var y = s_rand.Next(int.MaxValue / 2);
                var sum = x + y;
                var spanX = Utils.IntToSpan(x);
                var spanY = Utils.IntToSpan(y);
                var expected = Utils.IntToSpan(sum);

                // act
                var actual = spanX.AddTo(spanY);

                // assert
                Utils.AssertAreEqualSpans(expected, actual);
            }
        }

        public class Subtract
        {
            [Fact]
            public void WhenOnHappyPath_ReturnsAbsoluteDifference()
            {
                // arrange
                var a = s_rand.Next(int.MaxValue / 2, int.MaxValue);
                var b = s_rand.Next(int.MaxValue / 2);
                var diff = Math.Abs(a - b);
                var spanA = Utils.IntToSpan(a);
                var spanB = Utils.IntToSpan(b);
                var expected = Utils.IntToSpan(diff);

                // act
                var actual = spanA.Subtract(spanB).TrimStart(0);

                // assert
                Utils.AssertAreEqualSpans(expected, actual);
            }
        }

        public class CompareTo
        {
            [Fact]
            public void WhenOnHappyPath_ReturnCorrectComparisonInteger()
            {
                // arrange
                var x = s_rand.Next();
                var y = s_rand.Next();
                var expected = x.CompareTo(y);
                var spanX = Utils.IntToSpan(x);
                var spanY = Utils.IntToSpan(y);

                // act
                var actual = spanX.CompareTo(spanY);

                // assert
                Assert.Equal(expected, actual);
            }
        }

        private static class Utils
        {
            public static Span<int> IntToSpan(int n)
            {
                var nStr = n.ToString();
                var result = new int[nStr.Length].AsSpan();
                for (var i = 0; i < nStr.Length; i++)
                    result[i] = int.Parse(nStr[i].ToString());
                return result;
            }

            public static void AssertAreEqualSpans(Span<int> expected, Span<int> actual)
            {
                Assert.Equal(expected.Length, actual.Length);
                for (var i = 0; i < expected.Length; i++)
                    Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}
