using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Users.Api.Logging;
using Users.Api.Models;
using Users.Api.Repositories;
using Users.Api.Services;
using Xunit;

namespace Users.Api.Tests.Unit
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        private readonly ILoggerAdapter<UserService> _logger = Substitute.For<ILoggerAdapter<UserService>>();

        public UserServiceTests()
        {
            _userService = new UserService(_userRepository, _logger);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            //Arrange
            _userRepository.GetAllAsync().Returns(Array.Empty<User>());

            //Act
            var result = await _userService.GetAllAsync();

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnList_WhenUsersExist()
        {
            //Arrange
            var expectedUsers = new[]
            {
                new User
                {
                    Id=Guid.NewGuid(),
                    FullName="Andrija Mitrovic"
                }
            };

            _userRepository.GetAllAsync().Returns(expectedUsers);

            //Act
            var result = await _userService.GetAllAsync();

            //Assert
            result.Should().BeEquivalentTo(expectedUsers);
        }

        [Fact]
        public async Task GetAllAsync_ShouldLogMessages_WhenInvoked()
        {
            //Arrange
            _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());

            //Act
            await _userService.GetAllAsync();

            //Assert
            _logger.Received(1).LogInformation(Arg.Is("Retrieving all users"));
            _logger.Received(1).LogInformation(Arg.Is("All users retrieved in {0}ms"), Arg.Any<long>());
        }

        [Fact]
        public async Task GetAllAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
        {

            //Arrange
            var sqliteException = new SqliteException("Something went wrong", 500);
            _userRepository.GetAllAsync().Throws(sqliteException);

            //Act
            //Action requestAction = async () => await _userService.GetAllAsync();

            //Assert
            //await requestAction.Should().ThrowAsync<SqliteException>().WithMessage("Something went wrong");
            _logger.Received(1).LogError(Arg.Is(sqliteException), Arg.Is("Something went wrong"));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            //Arrange
            var expectedResult = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Andrija Mitrovic"
            };
            
            _userRepository.GetByIdAsync(expectedResult.Id).Returns(expectedResult);

            //Act
            var result = await _userService.GetByIdAsync(expectedResult.Id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNoUserExists()
        {
            //Arrange
            _userRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

            //Act
            var result = await _userService.GetByIdAsync(Guid.NewGuid());

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldLogMessages_WhenInvoked()
        {
            //Arrange
            var userId = Arg.Any<Guid>();
            _userRepository.GetByIdAsync(userId).ReturnsNull();

            //Act
            await _userService.GetByIdAsync(userId);

            //Assert
            _logger.Received(1).LogInformation(Arg.Is("Retrieving user with id: {0}"), Arg.Is(userId));
            _logger.Received(1).LogInformation(Arg.Is("User with id {0} retrieved in {1}ms"), Arg.Is(userId), Arg.Any<long>());
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateUser_WhenDetailsAreValid()
        {
            //Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Andrija Mitrovic"
            };

            _userRepository.CreateAsync(user).Returns(true);

            //Act
            var result = await _userService.CreateAsync(user);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldDeleteUser_WhenUserExists()
        {
            //Arrange
            _userRepository.DeleteByIdAsync(Arg.Any<Guid>()).Returns(true);

            //Act
            var result = await _userService.DeleteByIdAsync(Arg.Any<Guid>());

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldNotDeleteUser_WhenUserDoesntExist()
        {
            //Arrange
            _userRepository.DeleteByIdAsync(Arg.Any<Guid>()).Returns(false);

            //Act
            var result = await _userService.DeleteByIdAsync(Arg.Any<Guid>());

            //Assert
            result.Should().BeFalse();
        }
    }
}
