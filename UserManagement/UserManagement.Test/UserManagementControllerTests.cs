using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserManagement_API.Controllers;
using UserManagement_Application.DTO_Entities;
using UserManagement_Application.Interfaces;
using Xunit;

namespace UserManagement.Test
{
  public class UserManagementControllerTests
  {
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UserManagementController _controller;

    public UserManagementControllerTests()
    {
      _userServiceMock = new Mock<IUserService>();
      _controller        = new UserManagementController(
        _userServiceMock.Object
      );
    }

    [Fact]
    public async Task GetUser_WhenUserExists_ReturnsOkObjectResultWithUser()
    {
      // Arrange
      var userId       = 3;
      var expectedUser = new UserDTO { Id = userId, Name = "Jake Doe" };
      _userServiceMock
        .Setup(s => s.GetUserByIdAsync(userId))
        .ReturnsAsync(expectedUser);

      // Act
      var actionResult = await _controller.GetUser(userId);

      // Assert
      var okResult     = Assert.IsType<OkObjectResult>(actionResult.Result);
      var returnedUser = Assert.IsType<UserDTO>(okResult.Value);
      Assert.Equal(expectedUser.Id, returnedUser.Id);
      Assert.Equal(expectedUser.Name, returnedUser.Name);
    }

    [Fact]
    public async Task GetUser_WhenUserDoesNotExist_ReturnsNotFound()
    {
      // Arrange
      var userId = 1;
      _userServiceMock
        .Setup(s => s.GetUserByIdAsync(userId))
        .ReturnsAsync((UserDTO)null);

      // Act
      var actionResult = await _controller.GetUser(userId);

      // Assert
      Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetUsers_ReturnsOkObjectResultWithUsers()
    {
      // Arrange
      var expectedUsers = new List<UserDTO>
      {
        new() { Id = 1, Name = "John Doe"  },
        new() { Id = 2, Name = "Jane Doe"  }
      };
      _userServiceMock
        .Setup(s => s.GetAllUsersAsync())
        .ReturnsAsync(expectedUsers);

      // Act
      var actionResult = await _controller.GetUsers();

      // Assert
      var okResult      = Assert.IsType<OkObjectResult>(actionResult.Result);
      var returnedUsers = Assert.IsType<List<UserDTO>>(okResult.Value);
      Assert.Equal(expectedUsers.Count, returnedUsers.Count);
      for (int i = 0; i < expectedUsers.Count; i++)
      {
        Assert.Equal(expectedUsers[i].Id,   returnedUsers[i].Id);
        Assert.Equal(expectedUsers[i].Name, returnedUsers[i].Name);
      }
    }
  }
}
