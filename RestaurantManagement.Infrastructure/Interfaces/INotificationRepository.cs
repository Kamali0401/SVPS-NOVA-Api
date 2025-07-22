using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Interfaces
{
    public  interface INotificationRepository
    {
        /// <summary>
        /// Repository interface for performing CRUD operations on Notifications.
        /// </summary>
       
         /// <summary>
          /// Retrieves Notifications optionally filtered by their unique identifier.
          /// </summary>
          /// <param name="id">Optional. The unique identifier of the Notifications to retrieve. If not provided, retrieves all Notificationss.</param>
          /// <returns>
          /// The task result contains a collection of Notifications if successful, or null if no Notifications match the provided identifier.
          /// </returns>
            Task<IEnumerable<Notification>> GetNotificationById(int? id);
            Task<IEnumerable<Notification>> GetAllNotification(int studentId, string role);
            /// <summary>
            /// Inserts a new Notification.
            /// </summary>
            /// <param name="roles">The Notification to insert.</param>
            /// <returns>
            /// Not returns anything.
            /// </returns>
            Task UpdateNotificationDetails(Notification roles);
            /// <summary>
            /// Updates an existing Notification.
            /// </summary>
            /// <param name="roles">The Notification to update.</param>
            /// <returns>
            /// Not returns anything.
            /// </returns>
            // Task UpdateNotificationDetails(Notification roles);
            /// <summary>
            /// Deletes a Notification by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the Notification to delete.</param>
            /// <returns>
            /// The task result indicates whether the deletion was successful.
          
        }
}
