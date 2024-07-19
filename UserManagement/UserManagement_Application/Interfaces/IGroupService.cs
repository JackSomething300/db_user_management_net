using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement_Application.DTO_Entities;
using UserManagement_Core.Entities;
using UserManagement_Core.Interfaces;

namespace UserManagement_Application.Interfaces
{
    public interface IGroupService
    {
        Task AddGroupAsync(GroupDTO group);
        Task<IEnumerable<GroupDTO>> GetAllGroups();
        Task<GroupDTO> GetGroupByIdAsync(int id);
    }
}
