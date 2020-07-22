using Algorithms.Core.GraphSearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Algorithms.Tests.GraphSearch
{
    public class KosarajuTests
    {
        public class GetSccGroupSizes
        {
            [Theory]
            [MemberData(nameof(HappyPathData))]
            public void WhenOnHappyPath_ReturnsLargestStronglyConnectedComponents(
                string inputFile,
                int[] expected)
            {
                // arrange
                var parser = new KosarajuFileParser();
                var inputGraph = parser.ParseFile(inputFile);
                var sut = new Kosaraju();

                // act
                var actual = sut.GetSccGroupSizes(inputGraph).ToList();
                while (actual.Count < 5)
                {
                    actual.Add(0);
                }

                // assert
                Assert.True(expected.Length <= actual.Count);
                for (var i = 0; i < expected.Length - 1; i++)
                {
                    Assert.Equal(expected[i], actual[i]);
                }
            }

            public static IEnumerable<object[]> HappyPathData()
            {
                var inputFiles = TestCaseUtils.GetTestCaseFiles(2, 1);
                foreach (var inputFile in inputFiles)
                {
                    var outputFile = inputFile.Replace("input", "output");
                    var expected = GetExpectedOutput(outputFile);
                    yield return new object[] { inputFile, expected };
                }
            }

            private static int[] GetExpectedOutput(string outputFile) =>
                File.ReadLines(outputFile)
                    .First()
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x))
                    .ToArray();
        }
    }
}
