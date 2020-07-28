using Algorithms.Core.GraphSearch;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Algorithms.Tests.GraphSearch
{
    public class MedianMaintenanceTests
    {
        public class CalculateSumOfMedians
        {
            [Theory]
            [MemberData(nameof(HappyPathData))]
            public async Task WhenOnHappyPath_ReturnsMedianInteger(string inputFile, int expected)
            {
                // arrange
                var parser = new MedianFileParser();
                var inputFileNumbers = parser.ParseFile(inputFile);
                var sut = new MedianMaintenance();

                // act
                var actual = await sut.CalculateSumOfMedians(inputFileNumbers).ConfigureAwait(false);

                // assert
                Assert.Equal(expected, actual);
            }

            public static IEnumerable<object[]> HappyPathData()
            {
                var inputFiles = TestCaseUtils.GetTestCaseFiles(2, 3);
                foreach (var inputFile in inputFiles)
                {
                    var outputFile = inputFile.Replace("input", "output");
                    var expected = GetExpectedOutput(outputFile);
                    yield return new object[] { inputFile, expected };
                }
            }

            private static int GetExpectedOutput(string outputFile) =>
                int.Parse(File.ReadLines(outputFile).First());
        }
    }
}
