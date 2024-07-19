using UserManagement_Application.DTO_Entities;

namespace UserManagement_Presentation.Models
{
    public class UserGroupViewModel
    {
        public UserDTO User { get; set; }
        public List<int> GroupIds { get; set; }
        public IEnumerable<GroupDTO> AllGroups { get; set; }
        public List<int> SelectedGroupIds { get; set; }

        public UserGroupViewModel()
        {
            AllGroups = new List<GroupDTO>();
            GroupIds = new List<int>();
            SelectedGroupIds = new List<int>(); 
        }
    }
}
