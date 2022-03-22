﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Api.Contracts;
using Users.Api.Controllers;
using Users.Api.Mappers;
using Users.Api.Models;
using Users.Api.Services;
using Xunit;

namespace Users.Api.Tests.Unit
{
    public class UsersControllerTests
    {
        private readonly UsersController _usersController;
        private readonly IUserService _userService = Substitute.For<IUserService>();

        public UsersControllerTests()
        {
            _usersController = new UsersController(_userService);
        }

        [Fact]
        public async Task GetById_ReturnOkAndObject_WhenUserExists()
        {
            //Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Andrija Mitrovic"
            };
            _userService.GetByIdAsync(user.Id).Returns(user);
            var userResponse = user.ToUserResponse();

            //Act
            var result = (OkObjectResult)await _usersController.GetById(user.Id);

            //Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(userResponse);
        }

        [Fact]
        public async Task GetById_ReturnNotFound_WhenUserDoesntExist()
        {
            //Arrange
            _userService.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

            //Act
            var result = (NotFoundResult)await _usersController.GetById(Guid.NewGuid());

            //Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            //Arrange
            _userService.GetAllAsync().Returns(Enumerable.Empty<User>());

            //Act
            var result = (OkObjectResult)await _usersController.GetAll();

            //Assert
            result.StatusCode.Should().Be(200);
            result.Value.As<IEnumerable<UserResponse>>().Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_ShouldReturnUsers_WhenUsersExist()
        {
            //Arrange
            var users = new User[]
            {
                new User
                {
                    Id=Guid.NewGuid(),
                    FullName="Andrija Mitrovic"
                }
            };
            var usersResponse = users.Select(x => x.ToUserResponse());
            _userService.GetAllAsync().Returns(users);

            //Act
            var result = (OkObjectResult)await _usersController.GetAll();

            //Assert
            result.StatusCode.Should().Be(200);
            result.Value.As<IEnumerable<UserResponse>>().Should().BeEquivalentTo(usersResponse);
        }

        [Fact]
        public async Task Create_ShouldCreateUser_WhenCreateUserRequestIsValid()
        {
            //Arrange
            var user = new User
            {
                Id=Guid.NewGuid(),
                FullName = "Andrija Mitrovic"
            };

            _userService.CreateAsync(Arg.Do<User>(x => user = x)).Returns(true);

            //Act
            var result = (CreatedAtActionResult)await _usersController.Create(new CreateUserRequest { FullName = user.FullName });

            //Assert
            var userResponse = user.ToUserResponse();
            result.StatusCode.Should().Be(201);
            //result.Value.As<UserResponse>().Should().BeEquivalentTo(userResponse, options => options.Excluding(x => x.Id));
            result.Value.As<UserResponse>().Should().BeEquivalentTo(userResponse);
            result.RouteValues["id"].Should().BeEquivalentTo(user.Id);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenCreateUserRequestIsInvalid()
        {
            //Arrange
            _userService.CreateAsync(Arg.Any<User>()).Returns(false);

            //Act
            var result = (BadRequestResult)await _usersController.Create(new CreateUserRequest());

            //Assert
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task DeleteById_ReturnOk_WhenUserWasDeleted()
        {
            //Arrange
            _userService.DeleteByIdAsync(Arg.Any<Guid>()).Returns(true);

            //Act
            var result = (OkResult)await _usersController.DeleteById(Guid.NewGuid());

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteById_ReturnNotFound_WhenUserWasntDeleted()
        {
            //Arrange
            _userService.DeleteByIdAsync(Arg.Any<Guid>()).Returns(false);

            //Act
            var result = (NotFoundResult)await _usersController.DeleteById(Guid.NewGuid());

            //Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
