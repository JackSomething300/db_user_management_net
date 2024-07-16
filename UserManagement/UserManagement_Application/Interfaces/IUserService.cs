using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement_Application.DTO_Entities;

namespace UserManagement_Application.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(UserDTO userDto);
        Task DeleteUserAsync(int id);
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task UpdateUserAsync(UserDTO userDto);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    }
}
