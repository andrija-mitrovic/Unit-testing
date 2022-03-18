using Demo.Api.Models;
using Demo.Api.Repositories;
using Demo.Api.Services;
using FluentAssertions;
using Moq;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Api.Tests.Unit
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        //private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();

        public UserServiceTests()
        {
            //_userService = new UserService(_userRepository);
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            //Arrange
            //_userRepository.GetAllAsync().Returns(Array.Empty<User>());
            _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Array.Empty<User>());

            //Act
            var users = await _userService.GetAllAsync();

            //Assert
            users.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAListOfUsers_WhenUsersExist()
        {
            //Arrange
            var expectedUsers = new[]
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    FullName="Andrija Mitrovic"
                }
            };
            //_userRepository.GetAllAsync().Returns(expectedUsers);
            _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedUsers);

            //Act
            var users = await _userService.GetAllAsync();

            //Assert
            users.Should().ContainSingle(x => x.FullName == "Andrija Mitrovic");
        }
    }
}
