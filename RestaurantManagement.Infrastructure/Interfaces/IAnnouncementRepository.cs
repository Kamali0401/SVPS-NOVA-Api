using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Infrastructure.Interfaces
{
    /// <summary>
    /// Repository interface for performing CRUD operations on Announcements.
    /// </summary>
    public interface IAnnouncementRepository
    { /// <summary>
      /// Retrieves Announcements optionally filtered by their unique identifier.
      /// </summary>
      /// <param name="id">Optional. The unique identifier of the Announcements to retrieve. If not provided, retrieves all Announcementss.</param>
      /// <returns>
      /// The task result contains a collection of Announcements if successful, or null if no Announcements match the provided identifier.
      /// </returns>
        Task<IEnumerable<Announcement>> GetAnnouncement(int? id);
        Task<IEnumerable<Announcement>> GetAllAnnouncement(int? id, bool isReadToSendData);
        /// <summary>
        /// Inserts a new Announcement.
        /// </summary>
        /// <param name="roles">The Announcement to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<Announcement> InsertAnnouncementDetails(Announcement roles);
        /// <summary>
        /// Updates an existing Announcement.
        /// </summary>
        /// <param name="roles">The Announcement to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
       // Task UpdateAnnouncementDetails(Announcement roles);
        /// <summary>
        /// Deletes a Announcement by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Announcement to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteAnnouncementDetails(int id);
    }
}
