using RestaurantManagement.Application.Dtos;
using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public  interface ISectionSubjectMappingService
    {
        /// <summary>
        /// Retrieves Roles optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the SectionSubjectMappingDto to retrieve. If not provided, retrieves all Roles.</param>
        /// <returns>
        /// The task result contains a collection of SectionSubjectMappingDto DTOs. if successful, or null if no Roles match the provided identifier.
        /// </returns>
        Task<IEnumerable<SectionSubjectMappingDto>> GetSectionSubjectMapping(int? id);
        /// <summary>
        /// Inserts a new SectionSubjectMappingDto.
        /// </summary>
        /// <param name="SectionSubjectMappingDto">The DTO representing the SectionSubjectMappingDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<SectionSubjectMappingDto> InsertSectionSubjectMappingDetails(SectionSubjectMappingDto SectionSubjectMappingDto);

        /// <summary>
        /// Updates an existing SectionSubjectMappingDto.
        /// </summary>
        /// <param name="SectionSubjectMappingDto">The DTO representing the updated SectionSubjectMappingDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateSectionSubjectMappingDetails(SectionSubjectMappingDto SectionSubjectMappingDto);
        /// <summary>
        /// Deletes a SectionSubjectMappingDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the SectionSubjectMappingDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<string> DeleteSectionSubjectMappingDetails(int id);
        Task<IEnumerable<SectionSubjectMappingDto>> GetFacultyListBySectionIdDetails(int sectionId);
    }
}
