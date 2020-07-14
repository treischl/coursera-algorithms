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
                DirectedAdjacencyList inputGraph,
                int[] expected)
            {
                // arrange
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
                var testCaseDir = Path.Combine(Environment.CurrentDirectory, "GraphSearch", "KosarajuTestCases");
                var parser = new KosarajuFileParser();

                foreach (var inputFile in Directory.GetFiles(testCaseDir, "input*.txt"))
                {
                    var outputFile = inputFile.Replace("input", "output");
                    var inputGraph = parser.ParseFile(inputFile);
                    var expected = GetExpectedOutput(outputFile);
                    yield return new object[] { inputGraph, expected };
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
