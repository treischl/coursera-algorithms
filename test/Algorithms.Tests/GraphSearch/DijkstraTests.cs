using Algorithms.Core.GraphSearch.ShortestPath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Algorithms.Tests.GraphSearch
{
    public class DijkstraTests
    {
        public class CalculateShortestPath
        {
            [Theory]
            [MemberData(nameof(HappyPathData))]
            public async Task WhenOnHappyPath_ReturnsShortestPath(string inputFile, int[] expected)
            {
                // arrange
                var testLabels = new int[] { 7, 37, 59, 82, 99, 115, 133, 165, 188, 197 };
                var parser = new DijkstraFileParser();
                var inputGraph = await parser.ParseFile(inputFile).ConfigureAwait(false);
                var sut = new Dijkstra();

                // act
                var actual = sut.CalculateShortestPaths(inputGraph, 1);

                // assert
                for (var i = 0; i < expected.Length; i++)
                {
                    var testLabel = testLabels[i];
                    var testVertex = inputGraph.Vertices[testLabel - 1];
                    Assert.Equal(expected[i], actual[testVertex]);
                }
            }

            public static IEnumerable<object[]> HappyPathData()
            {
                var inputFiles = TestCaseUtils.GetTestCaseFiles(2, 2);
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
