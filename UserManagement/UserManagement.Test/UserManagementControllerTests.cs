using Microsoft.AspNetCore.Mvc;
using Moq;
using UserManagement_API.Controllers;
using UserManagement_Application.DTO_Entities;
using UserManagement_Application.Interfaces;

namespace UserManagement.Test
{
    public class UserManagementControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserManagementController _controller;

        public UserManagementControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserManagementController(_userServiceMock.Object);
        }


        [Fact]
        public async Task GetUser_WhenUserExists_ReturnsOkObjectResultWithUser()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new UserDTO { Id = userId, Name = "John Doe" };
            _userServiceMock.Setup(service => service.GetUserByIdAsync(userId))
                            .ReturnsAsync(expectedUser);

            // Act
            var result = await _controller.GetUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<UserDTO>(okResult.Value);
            returnedUser.Equals(expectedUser);
        }

        [Fact]
        public async Task GetUser_WhenUserDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var userId = 1;
            _userServiceMock.Setup(service => service.GetUserByIdAsync(userId))
                            .ReturnsAsync((UserDTO)null);

            // Act
            var result = await _controller.GetUser(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetUsers_ReturnsOkObjectResultWithUsers()
        {
            // Arrange
            var expectedUsers = new List<UserDTO>
            {
                new UserDTO { Id = 1, Name = "John Doe" },
                new UserDTO { Id = 2, Name = "Jane Doe" }
            };
            _userServiceMock.Setup(service => service.GetAllUsersAsync())
                            .ReturnsAsync(expectedUsers);

            // Act
            var result = await _controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsers = Assert.IsType<List<UserDTO>>(okResult.Value);
            returnedUsers.Equals(expectedUsers);
        }
    }
}