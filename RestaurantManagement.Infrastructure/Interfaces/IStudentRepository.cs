using System;
using System.Collections.Generic;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SonaNova.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on studentDetails.
    /// </summary>
    public interface IStudentRepository
    { /// <summary>
      /// Retrieves studentDetails optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the studentDetails to retrieve. If not provided, retrieves all studentDetailss.</param>
      /// <returns>
      /// The task result contains a collection of studentDetails if successful, or null if no studentDetailss match the provided identifier.
      /// </returns>
        Task<IEnumerable<Students>> GetStudentDetails(int? id);
        /// <summary>
        /// Inserts a new studentDetails.
        /// </summary>
        /// <param name="studentDetails">The studentDetails to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<Students> InsertStudent(Students studentDetails);
        /// <summary>
        /// Updates an existing studentDetails.
        /// </summary>
        /// <param name="studentDetails">The studentDetails to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateStudent(Students studentDetails);
        /// <summary>
        /// Deletes a studentDetails by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the studentDetails to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteStudent(int id);
        Task<IEnumerable<StudentDropdown>> GetStudentByName(string studentname);
        Task updateStudentSemDateDetails(StudentSemDateModel studentDto);
        Task<List<StudentSemDateModel>> GetAllStudentConfiguration(int? id);
    }
}
