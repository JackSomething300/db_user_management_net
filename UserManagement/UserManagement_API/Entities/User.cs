﻿namespace UserManagement_API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
    }
}
