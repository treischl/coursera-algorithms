using Algorithms.Core;
using Algorithms.Core.DivideAndConquer;
using System;
using Xunit;

namespace Algorithms.Tests.DivideAndConquer
{
    public class KaratsubaTests
    {
        public class MultiplyXAndY
        {
            [Theory]
            [InlineData(
                "3141592653589793238462643383279502884197169399375105820974944592",
                "2718281828459045235360287471352662497757247093699959574966967627",
                "8539734222673567065463550869546574495034888535765114961879601127067743044893204848617875072216249073013374895871952806582723184")]
            [InlineData("1234", "5678", "7006652")]
            [InlineData(null, null, null)]
            public void WhenOnHappyPath_ReturnsProductOfXAndY(string x, string y, string product)
            {
                // arrange
                if (x is null || y is null)
                {
                    var rand = new Random();
                    x = rand.Next(1000, 9999).ToString();
                    y = rand.Next(1000, 9999).ToString();
                    product = (int.Parse(x) * int.Parse(y)).ToString();
                }
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
        }
    }
}
