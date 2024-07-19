using Microsoft.EntityFrameworkCore;
using UserManagement_Application.DTO_Entities;
using UserManagement_Core.Entities;
using UserManagement_Core.Entities.ViewModels;
using UserManagement_Core.Interfaces;

namespace UserManagement_API.Data
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task AddGroupAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUsersPerGroupTotalCount()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<List<GroupWithUserCount>> GetGroupsWithUserCounts()
        {
            return await _context.UserGroups
                .GroupBy(ug => ug.Group)
                .Select(g => new GroupWithUserCount
                {
                    Group = new Group
                    {
                        Id = g.Key.Id,
                        Name = g.Key.Name,
                    },
                    UserCount = g.Count()
                })
                .ToListAsync();
        }

    }
}
