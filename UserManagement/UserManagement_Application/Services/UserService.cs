using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement_Application.DTO_Entities;
using UserManagement_Application.Interfaces;
using UserManagement_Core.Entities;
using UserManagement_Core.Interfaces;

namespace UserManagement_Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            // Map entities to DTOs
            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name
                // Other properties
            }).ToList();
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            // Map entity to DTO
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name
                // Other properties
            };
        }

        public async Task AddUserAsync(UserDTO userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.Name
                // Other properties
            };
            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.Name
                // Other properties
            };
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(user => new UserDTO { Id = user.Id, Name = user.Name /* Map other properties */ });
        }
    }
}
