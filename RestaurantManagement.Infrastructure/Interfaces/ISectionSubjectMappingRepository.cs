using RestaurantManagement.Domain.Entities;
using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Interfaces
{
    public interface ISectionSubjectMappingRepository
    {
        /// <summary>
        /// Repository interface for performing CRUD operations on SectionSubjectMappings.
        /// </summary>
       /// <summary>
          /// Retrieves SectionSubjectMappings optionally filtered by their unique identifier.
          /// </summary>
          /// <param name="id">Optional. The unique identifier of the SectionSubjectMappings to retrieve. If not provided, retrieves all SectionSubjectMappingss.</param>
          /// <returns>
          /// The task result contains a collection of SectionSubjectMappings if successful, or null if no SectionSubjectMappings match the provided identifier.
          /// </returns>
            Task<IEnumerable<SectionSubjectMapping>> GetSectionSubjectMapping(int? id);
            /// <summary>
            /// Inserts a new SectionSubjectMapping.
            /// </summary>
            /// <param name="roles">The SectionSubjectMapping to insert.</param>
            /// <returns>
            /// Not returns anything.
            /// </returns>
            Task<SectionSubjectMapping> InsertSectionSubjectMappingDetails(SectionSubjectMapping roles);
            /// <summary>
            /// Updates an existing SectionSubjectMapping.
            /// </summary>
            /// <param name="roles">The SectionSubjectMapping to update.</param>
            /// <returns>
            /// Not returns anything.
            /// </returns>
            Task UpdateSectionSubjectMappingDetails(SectionSubjectMapping roles);
            /// <summary>
            /// Deletes a SectionSubjectMapping by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the SectionSubjectMapping to delete.</param>
            /// <returns>
            /// The task result indicates whether the deletion was successful.
            /// </returns>
            Task<string> DeleteSectionSubjectMappingDetails(int id);
        Task<IEnumerable<SectionSubjectMapping>> GetFacultyListBySectionIdDetails(int sectionId);

    }
}
