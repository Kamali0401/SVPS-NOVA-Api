using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public  interface IMarkServices
    {
        /// <summary>
        /// Retrieves Roles optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the MarkDto to retrieve. If not provided, retrieves all Roles.</param>
        /// <returns>
        /// The task result contains a collection of MarkDto DTOs. if successful, or null if no Roles match the provided identifier.
        /// </returns>
        Task<IEnumerable<MarkDto>> GetStudentMark();
        Task<IEnumerable<MarkDto>> GetStudentMarkById(int? id);
        Task<IEnumerable<MarkDto>> GetStudentMarkByStudentId(int studentId);
        /// <summary>
        /// Inserts a new MarkDto.
        /// </summary>
        /// <param name="MarkDto">The DTO representing the MarkDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        ///   Task<HouseDto> InsertHouseDetails(HouseDto houseDto);
        Task<MarkDto> InsertMarkDetails(MarkDto MarkDto);

        /// <summary>
        /// Updates an existing MarkDto.
        /// </summary>
        /// <param name="MarkDto">The DTO representing the updated MarkDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        //  Task UpdateMarkDetails(MarkDto MarkDto);
        /// <summary>
        /// Deletes a MarkDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the MarkDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        //  Task<bool> DeleteMarkDetails(int id);
        Task<string> DeleteMarkDetails(List<MarkDto> marks);
        Task<IEnumerable<MarkDto>> UpdateReadytosendEmail(bool ReadytosendEmail);
        Task<string> GetAllMarkReport(string Section, string subjects, string test);
        Task<(MemoryStream memory, string path)> DownloadData(string filepath);
    }
}
