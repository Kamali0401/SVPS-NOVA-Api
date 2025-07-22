using RestaurantManagement.Application.Dtos;
using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public interface IExamService
    {
        // <summary>
        /// Retrieves Roles optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the ExamDto to retrieve. If not provided, retrieves all Roles.</param>
        /// <returns>
        /// The task result contains a collection of ExamDto DTOs. if successful, or null if no Roles match the provided identifier.
        /// </returns>
        Task<IEnumerable<ExamDto>> GetExam(int? id);
        /// <summary>
        /// Inserts a new ExamDto.
        /// </summary>
        /// <param name="ExamDto">The DTO representing the ExamDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<ExamDto> InsertExamDetails(ExamDto ExamDto);

        /// <summary>
        /// Updates an existing ExamDto.
        /// </summary>
        /// <param name="ExamDto">The DTO representing the updated ExamDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateExamDetails(ExamDto ExamDto);
        /// <summary>
        /// Deletes a ExamDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ExamDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<string> DeleteExamDetails(int id);
    }
}
