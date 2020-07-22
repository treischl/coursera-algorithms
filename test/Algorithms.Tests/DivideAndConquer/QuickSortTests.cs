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
            public void WhenOnHappyPath_ReturnsNumberOfComparisons(
                int[] integers,
                int expectedLeft,
                int expectedRight,
                int expectedMedian
            )
            {
                // arrange
                var leftArray = integers.Clone() as int[];
                var rightArray = integers.Clone() as int[];
                var medianArray = integers.Clone() as int[];
                var sut = new QuickSorter();

                // act
                var actualLeft = int.MinValue;
                sut.SortInPlace(leftArray.AsSpan(), PivotChoice.LeftMost, ref actualLeft);
                var actualRight = int.MinValue;
                sut.SortInPlace(rightArray.AsSpan(), PivotChoice.RightMost, ref actualRight);
                var actualMedian = int.MinValue;
                sut.SortInPlace(medianArray.AsSpan(), PivotChoice.MedianOfThree, ref actualMedian);

                // assert
                Assert.Equal(expectedLeft, actualLeft);
                Assert.Equal(expectedRight, actualRight);
                Assert.Equal(expectedMedian, actualMedian);
            }

            public static IEnumerable<object[]> HappyPathData()
            {
                var inputFiles = TestCaseUtils.GetTestCaseFiles(1, 3);
                foreach (var inputFile in inputFiles)
                {
                    var array = GetTestArray(inputFile);
                    var outputFile = inputFile.Replace("input_", "output_");
                    var (left, right, median) = GetExpectedComparisonCounts(outputFile);
                    yield return new object[] { array, left, right, median };
                }
            }

            private static int[] GetTestArray(string filePath) =>
                File.ReadAllLines(filePath).Select(x => int.Parse(x)).ToArray();

            private static (int, int, int) GetExpectedComparisonCounts(string filePath)
            {
                var lines = File.ReadAllLines(filePath).Select(x => int.Parse(x)).ToArray();
                return (lines[0], lines[1], lines[2]);
            }
        }
    }
}
