using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Services
{
    /// <summary>
    /// Service class for performing CRUD operations on Roles.
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _RoleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="RoleRepository">The repository for accessing RoleDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public RoleService(IRoleRepository RoleRepository, IMapper mapper)
        {
            _RoleRepository = RoleRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<RoleDto>> GetRolesDetails(int? id)
        {
            var Roles = await _RoleRepository.GetRolesDetails(id);

            var RoleDetails = _mapper.Map<IEnumerable<RoleDto>>(Roles);
            return RoleDetails;
        }
        /// <inheritdoc/>
        public async Task<RoleDto> InsertRoleDetails(RoleDto roleingDto)
        {

            var Role = _mapper.Map<Roles>(roleingDto);
            var insertedData = await _RoleRepository.InsertRoleDetails(Role);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Role insertion failed.");
            }
            return _mapper.Map<RoleDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateRoleDetails(RoleDto RoleDto)
        {
            var Role = _mapper.Map<Roles>(RoleDto);
            await _RoleRepository.UpdateRoleDetails(Role);
        }
        /// <inheritdoc/>
        public async Task<string> DeleteRoleDetails(int id)
        {
            return await _RoleRepository.DeleteRoleDetails(id);
        }


    }
}
