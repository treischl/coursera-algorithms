using Algorithms.Core;
using Algorithms.Core.DivideAndConquer;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Algorithms.Tests.DivideAndConquer
{
    public class KaratsubaTests
    {
        public class MultiplyXAndY
        {
            [Theory]
            [MemberData(nameof(HappyPathData))]
            public void WhenOnHappyPath_ReturnsProductOfXAndY(string x, string y, string product)
            {
                // arrange
                var first = x.ToSpanOfInt();
                var second = y.ToSpanOfInt();
                var expected = product.ToSpanOfInt();
                var sut = new Karatsuba();

                // act
                var actual = sut.MultiplyXAndY(first, second);

                // assert
                Assert.Equal(expected.Length, actual.Length);
                for (var i = 0; i < expected.Length; i++)
                {
                    Assert.Equal(expected[i], actual[i]);
                }
            }

            public static IEnumerable<object[]> HappyPathData()
            {
                var inputFiles = TestCaseUtils.GetTestCaseFiles(1, 1);
                foreach (var inputFile in inputFiles)
                {
                    var integers = File.ReadAllLines(inputFile);
                    var outputFile = inputFile.Replace("input_", "output_");
                    var expected = File.ReadLines(outputFile).First();
                    yield return new object[] { integers[0], integers[1], expected };
                }
            }
        }
    }
}
