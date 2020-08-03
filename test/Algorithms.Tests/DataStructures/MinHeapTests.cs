using Algorithms.Core.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Algorithms.Tests.DataStructures
{
    public class MinHeapTests
    {
        [Fact]
        public void CanExtractMinValuesInCorrectOrder()
        {
            // arrange
            var expected = new int[] { 4, 4, 5, 9, 4, 8, 9, 11, 13, 7, 10, 12 }
                .OrderBy(x => x).ToArray();
            var rand = new Random();
            var sut = new MinHeap<int>();

            // act
            foreach (var item in expected.OrderBy(_ => rand.Next()))
            {
                sut.Insert(item);
            }
            var actual = ExtractAll().ToArray();

            // assert
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }

            IEnumerable<int> ExtractAll()
            {
                while (sut.Size > 0)
                {
                    yield return sut.ExtractRoot();
                }
            }
        }
    }
}
