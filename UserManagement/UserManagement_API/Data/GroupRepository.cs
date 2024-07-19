using Microsoft.EntityFrameworkCore;
using UserManagement_Core.Entities;
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

    }
}
