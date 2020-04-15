using Algorithms.Core.DivideAndConquer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Algorithms.Tests.DivideAndConquer
{
    public class QuickSortTests
    {
        public class CountComparisons
        {
            [Theory]
            [MemberData(nameof(HappyPathData))]
            public void WhenOnHappyPath_ReturnsNumberOfComparisons(int[] integers, Comparisons expected)
            {
                // arrange
                var leftArray = integers.Clone() as int[];
                var rightArray = integers.Clone() as int[];
                var medianArray = integers.Clone() as int[];
                var sut = new QuickSorter();

                // act
                var actual = new Comparisons();
                sut.SortInPlace(leftArray.AsSpan(), PivotChoice.LeftMost, ref actual.Left);
                sut.SortInPlace(rightArray.AsSpan(), PivotChoice.RightMost, ref actual.Right);
                sut.SortInPlace(medianArray.AsSpan(), PivotChoice.MedianOfThree, ref actual.Median);

                // assert
                Assert.Equal(expected.Left, actual.Left);
                Assert.Equal(expected.Right, actual.Right);
                Assert.Equal(expected.Median, actual.Median);
            }

            public struct Comparisons
            {
                public int Left;
                public int Right;
                public int Median;
            }

            public static IEnumerable<object[]> HappyPathData()
            {
                var testCaseDir = Path.Combine(Environment.CurrentDirectory, "DivideAndConquer", "QuickSortTestCases");
                for (var i = 1; i <= 15; i++)
                {
                    var array = GetTestArray(Path.Combine(testCaseDir, $"input_{i:d2}.txt"));
                    var expected = GetExpectedComparisonCounts(Path.Combine(testCaseDir, $"output_{i:d2}.txt"));
                    yield return new object[] { array, expected };
                }
            }

            private static int[] GetTestArray(string filePath) =>
                File.ReadAllLines(filePath).Select(x => int.Parse(x)).ToArray();

            private static Comparisons GetExpectedComparisonCounts(string filePath)
            {
                var lines = File.ReadAllLines(filePath).Select(x => int.Parse(x)).ToArray();
                return new Comparisons
                {
                    Left = lines[0],
                    Right = lines[1],
                    Median = lines[2],
                };
            }
        }
    }
}
