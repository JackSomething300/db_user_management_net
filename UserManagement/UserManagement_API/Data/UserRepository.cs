using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement_API.Data;
using UserManagement_Core.Entities;
using UserManagement_Core.Interfaces;

namespace UserManagement_API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            if (id > 0)
            {
                return await _context.Users.Include(u => u.UserGroups)
                    .FirstOrDefaultAsync(u => u.Id == id);
            }

            return null;
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user != null && user.Id > 0)
            {
                try
                {
                    var existingUser = await _context.Users
                        .Include(u => u.UserGroups)
                        .FirstOrDefaultAsync(u => u.Id == user.Id);

                    if (existingUser == null)
                    {
                        throw new Exception("User not found");
                    }

                    _context.Entry(existingUser).CurrentValues.SetValues(user);

                    foreach (var existingGroup in existingUser.UserGroups.ToList())
                    {
                        if (!user.UserGroups.Any(ug => ug.GroupId == existingGroup.GroupId))
                        {
                            _context.UserGroups.Remove(existingGroup);
                        }
                    }

                    foreach (var incomingGroup in user.UserGroups)
                    {
                        var existingGroup = existingUser.UserGroups
                            .FirstOrDefault(ug => ug.GroupId == incomingGroup.GroupId);

                        if (existingGroup == null)
                        {
                            existingUser.UserGroups.Add(new UserGroup { UserId = user.Id, GroupId = incomingGroup.GroupId });
                        }

                    }

                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    var error = e.ToString();
                }
            }

        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users
                .Include(u => u.UserGroups)
                .FirstOrDefaultAsync(u => u.Id == id);

                if (user != null)
                {
                    foreach (var userGroup in user.UserGroups.ToList())
                    {
                        _context.UserGroups.Remove(userGroup);
                    }

                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var error = e.ToString();
            }

        }

        public async Task<int> GetUsersTotalCount()
        {
            return await _context.Users.CountAsync();
        }
    }
}
