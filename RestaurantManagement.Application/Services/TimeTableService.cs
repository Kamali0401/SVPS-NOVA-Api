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
    /// Service class for performing CRUD operations on TimeTables.
    /// </summary>
    public class TimeTableService : ITimeTableService
    {
        private readonly ITimeTableRepository _TimeTableRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeTableService"/> class.
        /// </summary>
        /// <param name="UserRepository">The repository for accessing UserDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public TimeTableService(ITimeTableRepository TimeTableRepository, IMapper mapper)
        {
            _TimeTableRepository = TimeTableRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        /// <inheritdoc/>
        public async Task<IEnumerable<TimeTableDto>> GetTimeTableDetails(int? id)
        {
            var TimeTables = await _TimeTableRepository.GetTimeTableDetails(id);

            var TimeTableDetails = _mapper.Map<IEnumerable<TimeTableDto>>(TimeTables);
            return TimeTableDetails;
        }
        /// <inheritdoc/>
        public async Task<TimeTableDto> InsertTimeTableDetails(TimeTableDto TimeTableDto)
        {

            var TimeTable = _mapper.Map<TimeTable>(TimeTableDto);
            var insertedData = await _TimeTableRepository.InsertTimeTableDetails(TimeTable);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("TimeTable insertion failed.");
            }
            return _mapper.Map<TimeTableDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateTimeTableDetails(TimeTableDto TimeTableDto)
        {
            var TimeTable = _mapper.Map<TimeTable>(TimeTableDto);
            await _TimeTableRepository.UpdateTimeTableDetails(TimeTable);
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteTimeTableDetails(int id)
        {
            return await _TimeTableRepository.DeleteTimeTableDetails(id);
        }
        public async Task<IEnumerable<TimeTableDto>> GetTimeTableBySectionId(int sectionId, string role)
        {
            var TimeTables = await _TimeTableRepository.GetTimeTableBySectionId(sectionId,role);

            var TimeTableDetails = _mapper.Map<IEnumerable<TimeTableDto>>(TimeTables);
            return TimeTableDetails;
        }

    }
}
