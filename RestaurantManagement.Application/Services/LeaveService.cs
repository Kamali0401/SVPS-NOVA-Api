using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure.Interfaces;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using SonaNova.Domain.Entities;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Services
{
    public class LeaveService: ILeaveService
    {
        
            private readonly ILeaveRepository _LeaveRepository;
            private readonly IMapper _mapper;

            /// <summary>
            /// Initializes a new instance of the <see cref="LeaveService"/> class.
            /// </summary>
            /// <param name="LeaveRepository">The repository for accessing LeaveDto data.</param>
            /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
            public LeaveService(ILeaveRepository LeaveRepository, IMapper mapper)
            {
                _LeaveRepository = LeaveRepository;
                _mapper = mapper;
            }
            /// <inheritdoc/>
            public async Task<IEnumerable<LeaveDto>> GetLeave(string role, int? id)
            {
                var Leaves = await _LeaveRepository.GetLeave(role,id);

                var LeaveDetails = _mapper.Map<IEnumerable<LeaveDto>>(Leaves);
                return LeaveDetails;
            }

        public async Task<IEnumerable<LeaveDto>> GetLeaveById(int? id)
        {
            var Leaves = await _LeaveRepository.GetLeaveById( id);

            var LeaveDetails = _mapper.Map<IEnumerable<LeaveDto>>(Leaves);
            return LeaveDetails;
        }
        /// <inheritdoc/>
        public async Task<LeaveDto> InsertLeaveDetails(LeaveDto LeaveingDto)
            {

                var Leave = _mapper.Map<Leave>(LeaveingDto);
                var insertedData = await _LeaveRepository.InsertLeaveDetails(Leave);
                if (insertedData == null)
                {
                    // Handle the case where the insertion was not successful
                    throw new Exception("Leave insertion failed.");
                }
                return _mapper.Map<LeaveDto>(insertedData);

            }
            /// <inheritdoc/>
            public async Task UpdateLeaveDetails(LeaveDto LeaveDto)
            {
                var Leave = _mapper.Map<Leave>(LeaveDto);
                await _LeaveRepository.UpdateLeaveDetails(Leave);
            }
            /// <inheritdoc/>
            public async Task<bool> DeleteLeaveDetails(int id)
            {
                return await _LeaveRepository.DeleteLeaveDetails(id);
            }
           

        
    }
}
