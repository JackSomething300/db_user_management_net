using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement_Application.DTO_Entities;
using UserManagement_Core.Entities;

namespace UserManagement_Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<UserGroup, UserGroupDTO>();
            CreateMap<UserGroupDTO, UserGroup>();

            CreateMap<Group, GroupDTO>();
            CreateMap<GroupDTO, Group>();

            CreateMap<GroupPermission, GroupPermissionDTO>();
            CreateMap<GroupPermissionDTO, GroupPermission>();

            CreateMap<Permission, PermissionDTO>();
            CreateMap<PermissionDTO, Permission>();
        }
    }
}
