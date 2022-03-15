using CalculatorLibrary;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CalculatorLibraryTest
{
    public class CalculatorTests : IDisposable
    {
        private readonly Calculator _calculator = new ();
        private readonly ITestOutputHelper _outputHelper;

        //Setup goes here
        public CalculatorTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _outputHelper.WriteLine("Hello from ctor");
        }

        [Theory]
        [InlineData(4, 5, 9)]
        [InlineData(1, 1, 2)]
        [InlineData(2, 3, 5)]
        [InlineData(5, 5, 10)]
        [InlineData(-5, 5, 0)]
        [InlineData(-15, -5, -20)]
        public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
        {
            //Act
            var result = _calculator.Add(a, b);

            //Assert
            Assert.Equal(expected, result);

            _outputHelper.WriteLine("Hello from the Add test");
        }

        [Theory]
        [InlineData(5,5,0)]
        [InlineData(15,5,10)]
        [InlineData(-5,-5,0)]
        [InlineData(-15,-5,-10)]
        [InlineData(5,10,-5)]
        //[Fact]
        public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
        {
            //Act
            var result = _calculator.Subtract(a, b);

            //Assert
            Assert.Equal(expected, result);

            _outputHelper.WriteLine("Hello from the Subtract test");
        }

        [Theory]
        [InlineData(5,5,25)]
        [InlineData(50,0,0)]
        [InlineData(-5,5,-25)]
        public void Multiply_ShouldMultiplyTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
        {
            //Act
            var result = _calculator.Multiply(a, b);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5,5,1)]
        [InlineData(15,5,3)]
        public void Dividing_ShouldDivideTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
        {
            //Act
            var result = _calculator.Divide(a, b);

            //Assert
            Assert.Equal(expected, result);
        }

        public void Dispose()
        {
            _outputHelper.WriteLine("Hello from cleanup");
        }
    }
}
