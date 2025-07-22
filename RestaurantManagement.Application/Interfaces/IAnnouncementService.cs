using DocumentFormat.OpenXml.Office2010.Excel;
using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Interfaces
{
    public  interface IAnnouncementService
    {
        /// <summary>
        /// Retrieves Roles optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the AnnouncementDto to retrieve. If not provided, retrieves all Roles.</param>
        /// <returns>
        /// The task result contains a collection of AnnouncementDto DTOs. if successful, or null if no Roles match the provided identifier.
        /// </returns>
        Task<IEnumerable<AnnouncementDto>> GetAnnouncement(int? id);
        Task<IEnumerable<AnnouncementDto>> GetAllAnnouncement(int? id, bool isReadToSendData);
        /// <summary>
        /// Inserts a new AnnouncementDto.
        /// </summary>
        /// <param name="AnnouncementDto">The DTO representing the AnnouncementDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<AnnouncementDto> InsertAnnouncementDetails(AnnouncementDto AnnouncementDto);

        /// <summary>
        /// Updates an existing AnnouncementDto.
        /// </summary>
        /// <param name="AnnouncementDto">The DTO representing the updated AnnouncementDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
      //  Task UpdateAnnouncementDetails(AnnouncementDto AnnouncementDto);
        /// <summary>
        /// Deletes a AnnouncementDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AnnouncementDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteAnnouncementDetails(int id);
    }
}
