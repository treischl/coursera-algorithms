using Algorithms.Core.GraphSearch;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Algorithms.Tests.GraphSearch
{
    public class TwoSumTests
    {
        public class GetDistinctSums
        {
            [Theory]
            [MemberData(nameof(HappyPathData))]
            public async Task WhenOnHappyPath_ReturnsCorrectNumberOfTwoSums(
                string inputFile,
                int expectedCount
            )
            {
                // arrange
                var inputFileNumbers = await GetNumbers(inputFile).ConfigureAwait(false);
                var targets = new int[20001].Select((_, index) => index - 10000L);
                var sut = new TwoSum();

                // act
                var actual = sut.GetDistinctSums(inputFileNumbers, targets).ToList();

                // assert
                Assert.Equal(expectedCount, actual.Count);
            }

            private async Task<IDictionary<long, long>> GetNumbers(string inputFile)
            {
                var numbers = new Dictionary<long, long>();
                var parser = new TwoSumFileParser();

                await foreach (var number in parser.ParseFile(inputFile))
                {
                    numbers[number] = number;
                }

                return numbers;
            }

            public static IEnumerable<object[]> HappyPathData()
            {
                var inputFiles = TestCaseUtils.GetTestCaseFiles(2, 4);
                foreach (var inputFile in inputFiles)
                {
                    var outputFile = inputFile.Replace("input", "output");
                    var expected = GetExpectedOutput(outputFile);
                    yield return new object[] { inputFile, expected };
                }
            }

            private static int GetExpectedOutput(string outputFile) =>
                int.Parse(File.ReadAllLines(outputFile).First());
        }
    }
}
