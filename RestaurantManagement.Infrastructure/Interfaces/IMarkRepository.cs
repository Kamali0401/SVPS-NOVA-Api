using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Interfaces
{
    public  interface IMarkRepository
    {
        /// <summary>
        /// Retrieves Roles optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the Mark to retrieve. If not provided, retrieves all Roles.</param>
        /// <returns>
        /// The task result contains a collection of Mark DTOs. if successful, or null if no Roles match the provided identifier.
        /// </returns>
        Task<IEnumerable<Mark>> GetStudentMarkById(int? id);
        Task<IEnumerable<Mark>> GetStudentMark();
        Task<IEnumerable<Mark>> GetStudentMarkByStudentId(int studentId);
        /// <summary>
        /// Inserts a new Mark.
        /// </summary>
        /// <param name="Mark">The DTO representing the Mark to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        ///   Task<HouseDto> InsertHouseDetails(HouseDto houseDto);
        Task<Mark> InsertMarkDetails(Mark Mark);

        /// <summary>
        /// Updates an existing Mark.
        /// </summary>
        /// <param name="Mark">The DTO representing the updated Mark.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
      //  Task UpdateMarkDetails(Mark Mark);
        /// <summary>
        /// Deletes a Mark by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Mark to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
      //  Task<bool> DeleteMarkDetails(int id);
        Task<string> DeleteMarkDetails(List<Mark> marks);
        Task<IEnumerable<Mark>> UpdateReadytosendEmail(bool ReadytosendEmail);
        Task<string> GetAllMarkReport(string Section, string subjects, string test);
       // Task<(MemoryStream memory, string path)> DownloadData(string filepath);
    }
}
