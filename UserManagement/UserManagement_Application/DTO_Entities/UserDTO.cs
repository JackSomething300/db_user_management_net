namespace UserManagement_Application.DTO_Entities
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
       public ICollection<UserGroupDTO> UserGroups { get; set; }

        public UserDTO()
        {
            UserGroups = new List<UserGroupDTO>();
        }
    }
}
