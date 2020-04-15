using Algorithms.Core.DivideAndConquer;
using System;
using System.Collections.Generic;
using Xunit;

namespace Algorithms.Tests.DivideAndConquer
{
    public class InversionCounterTests
    {
        public class CountInversions
        {
            [Theory]
            [MemberData(nameof(HappyPathData))]
            public void WhenOnHappyPath_ReturnsNumberOfInversions(int[] integers, long expected)
            {
                // arrange
                var sut = new InversionCounter();

                // act
                var actual = sut.CountInversions(integers.AsSpan());

                // assert
                Assert.Equal(expected, actual);
            }

            public static IEnumerable<object[]> HappyPathData() => new List<object[]>
        {
            new object[] { new int[] { 1, 3, 5, 2, 4, 6 }, 3 },
            new object[] { new int[] { 6, 5, 4, 3, 2, 1 }, 15 },
            new object[] { new int[] { 1, 2, 3, 4, 5, 6 }, 0 },
        };
        }
    }
}
