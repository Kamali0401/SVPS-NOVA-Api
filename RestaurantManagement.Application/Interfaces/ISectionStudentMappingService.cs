using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Domain.Entities;
using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public  interface ISectionStudentMappingService
    {

        /// <summary>
        /// Retrieves Roles optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the SectionStudentMappingDto to retrieve. If not provided, retrieves all Roles.</param>
        /// <returns>
        /// The task result contains a collection of SectionStudentMappingDto DTOs. if successful, or null if no Roles match the provided identifier.
        /// </returns>
        Task<IEnumerable<SectionStudentMappingDto>> GetSectionStudentMapping(int? id);
       
        /// <summary>
        /// Inserts a new SectionStudentMappingDto.
        /// </summary>
        /// <param name="SectionStudentMappingDto">The DTO representing the SectionStudentMappingDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<int> InsertSectionStudentMappingDetails(List<SectionStudentMappingDto> SectionStudentMappingDto);

        /// <summary>
        /// Updates an existing SectionStudentMappingDto.
        /// </summary>
        /// <param name="SectionStudentMappingDto">The DTO representing the updated SectionStudentMappingDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task<int> UpdateSectionStudentMappingDetails(List<SectionStudentMappingDto> SectionStudentMappingDto);
        /// <summary>
        /// Deletes a SectionStudentMappingDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the SectionStudentMappingDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<int> DeleteSectionStudentMappingDetails(int[] ids,  int batchId);
        Task<IEnumerable<StudentDropdownModelDto>> GetMappedStudentByName(string StudentName, int SectionId);
    }
}
