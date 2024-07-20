using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserManagement_API.Data;
using UserManagement_Core.Entities;


namespace UserManagement.Test
{
    public class UserManagementDBRepositoryTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly UserRepository _userRepository;
        private readonly Mock<DbSet<User>> _mockSet;

        public UserManagementDBRepositoryTests()
        {
            _mockContext = new Mock<DataContext>(new DbContextOptions<DataContext>());
            _userRepository = new UserRepository(_mockContext.Object);
            _mockSet = new Mock<DbSet<User>>();

            _mockContext.Setup(m => m.Users).Returns(_mockSet.Object);
        }

        [Fact]
        public async Task AddUserAsync_AddsUser()
        {
            var user = new User { Id = 3, Name = "New Jack" };

            _mockSet.Setup(m => m.Add(It.IsAny<User>())).Verifiable();
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1).Verifiable();

            await _userRepository.AddUserAsync(user);

            _mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task GetUsersAsync_ReturnsUsers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new DataContext(options))
            {
                context.Users.AddRange(
                    new User { Id = 1, Name = "John" },
                    new User { Id = 2, Name = "Jane" },
                    new User { Id = 3, Name = "Jack" }
                );
                context.SaveChanges();
            }

            // Act
            using (var context = new DataContext(options))
            {
                var userRepository = new UserRepository(context);
               var result = await userRepository.GetUsersAsync();

                // Assert
                Assert.Equal(3, result.Count());
                Assert.Contains(result, u => u.Name == "John");
                Assert.Contains(result, u => u.Name == "Jane");
                Assert.Contains(result, u => u.Name == "Jack");
            }

            
        }
    }
}
