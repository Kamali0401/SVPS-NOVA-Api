using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Services
{
    /// <summary>
    /// Service class for performing CRUD operations on HolidayCalendars.
    /// </summary>
    public class HolidayCalendarService : IHolidayCalendarService
    {
        private readonly IHolidayCalendarRepository _HolidayCalendarRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayCalendarService"/> class.
        /// </summary>
        /// <param name="HolidayCalendarRepository">The repository for accessing HolidayCalendarDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public HolidayCalendarService(IHolidayCalendarRepository HolidayCalendarRepository, IMapper mapper)
        {
            _HolidayCalendarRepository = HolidayCalendarRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<HolidayCalendarDto>> GetHolidayCalendarDetails(int? id)
        {
            var HolidayCalendars = await _HolidayCalendarRepository.GetHolidayCalendarDetails(id);

            var HolidayCalendarDetails = _mapper.Map<IEnumerable<HolidayCalendarDto>>(HolidayCalendars);
            return HolidayCalendarDetails;
        }
        /// <inheritdoc/>
        public async Task<HolidayCalendarDto> InsertHolidayCalendarDetails(HolidayCalendarDto HolidayCalendarDto)
        {

            var HolidayCalendar = _mapper.Map<HolidayCalendar>(HolidayCalendarDto);
            var insertedData = await _HolidayCalendarRepository.InsertHolidayCalendarDetails(HolidayCalendar);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("HolidayCalendar insertion failed.");
            }
            return _mapper.Map<HolidayCalendarDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateHolidayCalendarDetails(HolidayCalendarDto HolidayCalendarDto)
        {
            var HolidayCalendar = _mapper.Map<HolidayCalendar>(HolidayCalendarDto);
            await _HolidayCalendarRepository.UpdateHolidayCalendarDetails(HolidayCalendar);
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteHolidayCalendarDetails(int id)
        {
            return await _HolidayCalendarRepository.DeleteHolidayCalendarDetails(id);
        }


    }
}
