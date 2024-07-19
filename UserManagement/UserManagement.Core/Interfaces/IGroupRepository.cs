using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement_Core.Entities;

namespace UserManagement_Core.Interfaces
{
    public interface IGroupRepository
    {
        Task AddGroupAsync(Group group);
        Task<IEnumerable<Group>> GetAllGroups();
        Task<Group> GetGroupByIdAsync(int id);
    }
}
