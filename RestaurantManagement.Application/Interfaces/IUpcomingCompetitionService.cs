using RestaurantManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Service interface for performing CRUD operations on Roles.
    /// </summary>
    public interface IUpcomingCompetitionService
    {/// <summary>
     /// Retrieves Roles optionally filtered by their unique identifier.
     /// </summary>
     /// <param name="id">Optional. The unique identifier of the UpcomingCompetitionDto to retrieve. If not provided, retrieves all Roles.</param>
     /// <returns>
     /// The task result contains a collection of UpcomingCompetitionDto DTOs. if successful, or null if no Roles match the provided identifier.
     /// </returns>
        Task<IEnumerable<UpcomingCompetitionDto>> GetUpcomingCompetition(string role,int? id);
        Task<IEnumerable<UpcomingCompetitionDto>> GetUpcomingCompetitionbyId(int? id);

        Task<IEnumerable<UpcomingCompetitionDto>> UpdateInterestedCompetition(int studentId, int competitionId);

        /// <summary>
        /// Inserts a new UpcomingCompetitionDto.
        /// </summary>
        /// <param name="UpcomingCompetitionDto">The DTO representing the UpcomingCompetitionDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<UpcomingCompetitionDto> InsertUpcomingCompetition(UpcomingCompetitionDto UpcomingCompetitionDto);

        /// <summary>
        /// Updates an existing UpcomingCompetitionDto.
        /// </summary>
        /// <param name="UpcomingCompetitionDto">The DTO representing the updated UpcomingCompetitionDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateUpcomingCompetition(UpcomingCompetitionDto UpcomingCompetitionDto);
        /// <summary>
        /// Deletes a UpcomingCompetitionDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the UpcomingCompetitionDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteUpcomingCompetitionDetails(int id);
        Task<(MemoryStream memory, string path)> DownloadData(string filepath);
        Task<string> GetInterestedStudentList(int competitionId);
    }
}