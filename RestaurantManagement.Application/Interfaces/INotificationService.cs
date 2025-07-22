using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public  interface INotificationService
    {
        /// <summary>
        /// Retrieves Roles optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the NotificationDto to retrieve. If not provided, retrieves all Roles.</param>
        /// <returns>
        /// The task result contains a collection of NotificationDto DTOs. if successful, or null if no Roles match the provided identifier.
        /// </returns>
        Task<IEnumerable<NotificationDto>> GetNotificationById(int? id);
        Task<IEnumerable<NotificationDto>> GetAllNotification(int studentId, string role);
        /// <summary>
        /// Inserts a new NotificationDto.
        /// </summary>
        /// <param name="NotificationDto">The DTO representing the NotificationDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task UpdateNotificationDetails(NotificationDto NotificationDto);

        /// <summary>
        /// Updates an existing NotificationDto.
        /// </summary>
        /// <param name="NotificationDto">The DTO representing the updated NotificationDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
      //  Task UpdateNotificationDetails(NotificationDto NotificationDto);
        /// <summary>
        /// Deletes a NotificationDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the NotificationDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
       // Task<bool> DeleteNotificationDetails(int id);
    }
}
