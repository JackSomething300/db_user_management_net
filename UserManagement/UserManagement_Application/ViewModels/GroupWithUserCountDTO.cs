using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement_Application.DTO_Entities;

namespace UserManagement_Application.ViewModels
{
    public class GroupWithUserCountDTO
    {
        public GroupDTO Group { get; set; }
        public int UserCount { get; set; }
    }
}
