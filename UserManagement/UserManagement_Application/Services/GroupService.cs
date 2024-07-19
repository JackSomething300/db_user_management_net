using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement_Application.DTO_Entities;
using UserManagement_Application.Interfaces;
using UserManagement_Core.Entities;
using UserManagement_Core.Interfaces;

namespace UserManagement_Application.Services
{
    public class GroupService : IGroupService
    {

        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task AddGroupAsync(GroupDTO groupdto)
        {
            var groupdb = _mapper.Map<Group>(groupdto);
            await _groupRepository.AddGroupAsync(groupdb);
        }

        public async Task<IEnumerable<GroupDTO>> GetAllGroups()
        {
            var groups = await _groupRepository.GetAllGroups();
            return _mapper.Map<IEnumerable<GroupDTO>>(groups);
        }

        public async Task<GroupDTO> GetGroupByIdAsync(int id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);
            return _mapper.Map<GroupDTO>(group);
        }
    }
}
