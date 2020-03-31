using Algorithms.Commands;
using Algorithms.Verbs;
using Autofac.Extras.Moq;
using System.IO;
using Xunit;

namespace Algorithms.Tests.Commands
{
    public class CountInversionsTests
    {
        [Fact]
        public void WhenOnHappyPath_ReturnsNumberOfInversions()
        {
            // arrange
            var intArray = new int[] { 1, 3, 5, 2, 4, 6 };
            const int expected = 3;
            var options = new CountInversionsOptions
            {
                Integers = intArray,
            };
            using var mock = AutoMock.GetLoose();
            var sut = mock.Create<CountInversionsCommand>();

            // act
            sut.Execute(options);

            // assert
            mock.Mock<TextWriter>().Verify(
                x => x.WriteLine(expected));
        }
    }
}
