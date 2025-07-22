using RestaurantManagement.Domain.Entities;
using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Interfaces
{
    public  interface ISectionStudentMappingRepository
    {
        /// <summary>
        /// Retrieves SectionStudentMappings optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the SectionStudentMappings to retrieve. If not provided, retrieves all SectionStudentMappingss.</param>
        /// <returns>
        /// The task result contains a collection of SectionStudentMappings if successful, or null if no SectionStudentMappings match the provided identifier.
        /// </returns>
        Task<IEnumerable<SectionStudentMapping>> GetSectionStudentMapping(int? id);
       
        /// <summary>
        /// Inserts a new SectionStudentMapping.
        /// </summary>
        /// <param name="roles">The SectionStudentMapping to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<int> InsertSectionStudentMappingDetails(List<SectionStudentMapping> sectionStudentMappings);
        /// <summary>
        /// Updates an existing SectionStudentMapping.
        /// </summary>
        /// <param name="roles">The SectionStudentMapping to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task <int>UpdateSectionStudentMappingDetails(List<SectionStudentMapping> sectionStudentMappings);
        /// <summary>
        /// Deletes a SectionStudentMapping by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the SectionStudentMapping to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<int> DeleteSectionStudentMappingDetails(int[] ids, int batchId);
        Task<IEnumerable<StudentDropdownModel>> GetMappedStudentByName(string StudentName, int SectionId);
    }
}
