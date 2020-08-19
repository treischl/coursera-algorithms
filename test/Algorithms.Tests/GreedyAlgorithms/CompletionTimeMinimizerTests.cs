using Algorithms.Core.GreedyAlgorithms;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Algorithms.Tests.GreedyAlgorithms
{
    public class CompletionTimeMinimizerTests
    {
        public class MinimizeWeightedSum
        {
            [Theory]
            [MemberData(nameof(ByDifferenceData))]
            public async Task WhenOrderingByDifference_ReturnsCorrectSum(
                string inputFile,
                long expectedSum
            )
            {
                // arrange
                var jobs = await ParseInputFile(inputFile).ConfigureAwait(false);
                var sut = new CompletionTimeMinimizer();

                // act
                var actualSum = sut.MinimizeWeightedSum(jobs, WeightedJobComparer.Difference);

                // assert
                Assert.Equal(expectedSum, actualSum);
            }

            private async Task<List<WeightedJob>> ParseInputFile(string inputFile)
            {
                var jobs = new List<WeightedJob>();
                var parser = new WeightedJobFileParser();

                await foreach (var job in parser.ParseFile(inputFile))
                {
                    jobs.Add(job);
                }

                return jobs;
            }

            private static IEnumerable<object[]> ByDifferenceData()
            {
                var inputFiles = TestCaseUtils.GetTestCaseFiles(3, 1, new TestCaseFileConfig
                {
                    AssignmentSubDirectory = "questions1And2",
                });
                foreach (var inputFile in inputFiles)
                {
                    var outputFile = inputFile.Replace("input", "output");
                    var expected = GetExpectedDifferenceOutput(outputFile);
                    yield return new object[] { inputFile, expected };
                }
            }

            private static long GetExpectedDifferenceOutput(string outputFile) =>
                long.Parse(File.ReadAllLines(outputFile).First());
        }
    }
}
