using Algorithms.Commands;
using Algorithms.Verbs;
using Autofac.Extras.Moq;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Algorithms.Tests.Commands
{
    public class CountInversionsTests
    {
        [Theory]
        [MemberData(nameof(HappyPathData))]
        public void WhenOnHappyPath_ReturnsNumberOfInversions(int[] integers, long expected)
        {
            // arrange
            var options = new CountInversionsOptions
            {
                Integers = integers,
            };
            using var mock = AutoMock.GetLoose();
            var sut = mock.Create<CountInversionsCommand>();

            // act
            sut.Execute(options);

            // assert
            mock.Mock<TextWriter>().Verify(
                x => x.WriteLine(expected));
        }

        public static IEnumerable<object[]> HappyPathData() => new List<object[]>
        {
            new object[] { new int[] { 1, 3, 5, 2, 4, 6 }, 3 },
            new object[] { new int[] { 6, 5, 4, 3, 2, 1 }, 15 },
            new object[] { new int[] { 1, 2, 3, 4, 5, 6 }, 0 },
        };
    }
}
