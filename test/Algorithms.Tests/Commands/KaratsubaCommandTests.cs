﻿using Algorithms.Commands;
using Algorithms.Verbs;
using Autofac.Extras.Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Algorithms.Tests.Commands
{
    public class KaratsubaCommandTests
    {
        public class Execute
        {
            [Theory]
            [InlineData(
                "3141592653589793238462643383279502884197169399375105820974944592",
                "2718281828459045235360287471352662497757247093699959574966967627",
                "8539734222673567065463550869546574495034888535765114961879601127067743044893204848617875072216249073013374895871952806582723184")]
            [InlineData("1234", "5678", "7006652")]
            [InlineData(null, null, null)]
            public void WhenOnHappyPath_ReturnsProductOfValues(string x, string y, string product)
            {
                // arrange
                var rand = new Random();
                if (x is null || y is null)
                {
                    x = rand.Next(1000, 9999).ToString();
                    y = rand.Next(1000, 9999).ToString();
                    product = (int.Parse(x) * int.Parse(y)).ToString();
                }
                var options = new KaratsubaOptions
                {
                    X = x,
                    Y = y,
                };
                using var mock = AutoMock.GetLoose();
                var sut = mock.Create<KaratsubaCommand>();

                // act
                sut.Execute(options);

                // assert
                mock.Mock<TextWriter>().Verify(
                    x => x.WriteLine(product));
            }
        }
    }
}