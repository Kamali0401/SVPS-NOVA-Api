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
    /// <summary>
    /// Service class for performing CRUD operations on AcademicCalendars.
    /// </summary>
    public class AcademicCalendarService : IAcademicCalendarService
    {
        private readonly IAcademicCalendarRepository _AcademicCalendarRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AcademicCalendarService"/> class.
        /// </summary>
        /// <param name="AcademicCalendarRepository">The repository for accessing AcademicCalendarDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public AcademicCalendarService(IAcademicCalendarRepository AcademicCalendarRepository, IMapper mapper)
        {
            _AcademicCalendarRepository = AcademicCalendarRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<AcademicCalendarDto>> GetAcademicCalendar(int? id)
        {
            var AcademicCalendars = await _AcademicCalendarRepository.GetAcademicCalendar(id);

            var AcademicCalendarDetails = _mapper.Map<IEnumerable<AcademicCalendarDto>>(AcademicCalendars);
            return AcademicCalendarDetails;
        }

        public async Task<IEnumerable<AcademicCalendarDto>> GetAllAcademicCalendar(string  role)
        {
            var AcademicCalendars = await _AcademicCalendarRepository.GetAllAcademicCalendar(role);

            var AcademicCalendarDetails = _mapper.Map<IEnumerable<AcademicCalendarDto>>(AcademicCalendars);
            return AcademicCalendarDetails;
        }
        /// <inheritdoc/>
        public async Task<AcademicCalendarDto> InsertAcademicCalendarDetails(AcademicCalendarDto AcademicCalendaringDto)
        {

            var AcademicCalendar = _mapper.Map<AcademicCalendar>(AcademicCalendaringDto);
            var insertedData = await _AcademicCalendarRepository.InsertAcademicCalendarDetails(AcademicCalendar);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("AcademicCalendar insertion failed.");
            }
            return _mapper.Map<AcademicCalendarDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateAcademicCalendarDetails(AcademicCalendarDto AcademicCalendarDto)
        {
            var AcademicCalendar = _mapper.Map<AcademicCalendar>(AcademicCalendarDto);
            await _AcademicCalendarRepository.UpdateAcademicCalendarDetails(AcademicCalendar);
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteAcademicCalendarDetails(int id)
        {
            return await _AcademicCalendarRepository.DeleteAcademicCalendarDetails(id);
        }
    }
 }
