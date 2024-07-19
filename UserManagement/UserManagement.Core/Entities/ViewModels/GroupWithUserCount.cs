using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement_Core.Entities.ViewModels
{
    public class GroupWithUserCount
    {
        public Group Group { get; set; }
        public int UserCount { get; set; }
    }
}
