using RestaurantManagement.Application.Dtos;
using SonaNova.Application.Dtos;
using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public  interface IAcademicCalendarService
    {
        // <summary>
        /// Retrieves Roles optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the AcademicCalendarDto to retrieve. If not provided, retrieves all Roles.</param>
        /// <returns>
        /// The task result contains a collection of AcademicCalendarDto DTOs. if successful, or null if no Roles match the provided identifier.
        /// </returns>
        Task<IEnumerable<AcademicCalendarDto>> GetAcademicCalendar(int? id);
        Task<IEnumerable<AcademicCalendarDto>> GetAllAcademicCalendar(string role);
        /// <summary>
        /// Inserts a new AcademicCalendarDto.
        /// </summary>
        /// <param name="AcademicCalendarDto">The DTO representing the AcademicCalendarDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<AcademicCalendarDto> InsertAcademicCalendarDetails(AcademicCalendarDto AcademicCalendarDto);

        /// <summary>
        /// Updates an existing AcademicCalendarDto.
        /// </summary>
        /// <param name="AcademicCalendarDto">The DTO representing the updated AcademicCalendarDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateAcademicCalendarDetails(AcademicCalendarDto AcademicCalendarDto);
        /// <summary>
        /// Deletes a AcademicCalendarDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AcademicCalendarDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteAcademicCalendarDetails(int id);
    }
}
