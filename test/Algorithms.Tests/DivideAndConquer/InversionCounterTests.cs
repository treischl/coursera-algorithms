using Algorithms.Core.DivideAndConquer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            public static IEnumerable<object[]> HappyPathData()
            {
                var inputFiles = TestCaseUtils.GetTestCaseFiles(1, 2);
                foreach (var inputFile in inputFiles)
                {
                    var integers = ReadIntegerArrayFile(inputFile);
                    var outputFile = inputFile.Replace("input_", "output_");
                    var expected = GetExpectedInversionsCount(outputFile);
                    yield return new object[] { integers, expected };
                }
            }
            private static int[] ReadIntegerArrayFile(string filePath) =>
                File.ReadAllLines(filePath).Select(x => int.Parse(x)).ToArray();

            private static long GetExpectedInversionsCount(string filePath) =>
                File.ReadLines(filePath).Select(x => long.Parse(x)).First();
        }
    }
}
