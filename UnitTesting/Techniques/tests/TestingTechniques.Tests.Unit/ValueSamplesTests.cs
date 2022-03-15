using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestingTechniques.Tests.Unit
{
    public class ValueSamplesTests
    {
        private readonly ValueSamples _valueSamples = new ValueSamples();

        [Fact]
        public void StringAssertionExample()
        {
            var fullName = _valueSamples.FullName;

            fullName.Should().Be("Andrija Mitrovic");
            fullName.Should().NotBeEmpty();
            fullName.Should().StartWith("Andrija");
            fullName.Should().EndWith("Mitrovic");
        }

        [Fact]
        public void NumberAssertionExample()
        {
            var age = _valueSamples.Age;

            age.Should().Be(28);
            age.Should().BePositive();
            age.Should().BeGreaterThan(20);
            age.Should().BeLessThanOrEqualTo(29);
            age.Should().BeInRange(18, 60);
        }

        [Fact]
        public void DateAssertionExample()
        {
            var dateOfBirth = _valueSamples.DateOfBirth;

            dateOfBirth.Should().Be(new DateTime(1993, 6, 9));
        }

        [Fact]
        public void ObjectAssertionExample()
        {
            var expected = new User
            {
                FullName = "Andrija Mitrovic",
                Age = 21,
                DateOfBirth = new(2000, 6, 9)
            };

            var user = _valueSamples.AppUser;

            //Assert.Equal(expected, user);
            //user.Should().Be(expected);
            user.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void EnumerableObjectsAssertionExample()
        {
            var expected = new User
            {
                FullName = "Andrija Mitrovic",
                Age = 21,
                DateOfBirth = new(2000, 6, 9)
            };

            var users = _valueSamples.Users.As<User[]>();

            users.Should().ContainEquivalentOf(expected);
            users.Should().HaveCount(3);
            users.Should().Contain(x => x.FullName.StartsWith("Andrija") && x.Age > 5);
        }

        [Fact]
        public void EnumerableNumbersAssertionExample()
        {
            var numbers = _valueSamples.Numbers.As<int[]>();

            numbers.Should().Contain(5);
        }

        [Fact]
        public void ExceptionThrowAssertionExample()
        {
            var calculator = new Calculator();

            Action result = () => calculator.Divide(1, 0);

            result.Should().Throw<DivideByZeroException>().WithMessage("Attempted to divide by zero.");
        }

        [Fact]
        public void EventRaisedAssertionExample()
        {
            var monitorSubject = _valueSamples.Monitor();

            _valueSamples.RaiseExampleEvent();

            monitorSubject.Should().Raise("ExampleEvent");
        }

        [Fact]
        public void TestingInternalMembersExample()
        {
            var number = _valueSamples.InternalSecretNumber;

            number.Should().Be(42);
        }
    }
}
