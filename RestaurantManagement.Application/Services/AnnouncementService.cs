using AutoMapper;
using RestaurantManagement.Application.Dtos;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using SonaNova.Domain.Entities;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Services
{
    /// <summary>
    /// Service class for performing CRUD operations on Announcements.
    /// </summary>
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _AnnouncementRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnouncementService"/> class.
        /// </summary>
        /// <param name="AnnouncementRepository">The repository for accessing Announcement data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public AnnouncementService(IAnnouncementRepository AnnouncementRepository, IMapper mapper)
        {
            _AnnouncementRepository = AnnouncementRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<AnnouncementDto>> GetAnnouncement(int? id)
        {
            var Announcements = await _AnnouncementRepository.GetAnnouncement(id);

            var AnnouncementDetails = _mapper.Map<IEnumerable<AnnouncementDto>>(Announcements);
            return AnnouncementDetails;
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAnnouncement(int? id, bool isReadToSendData)
        {
            var Announcements = await _AnnouncementRepository.GetAllAnnouncement(id, isReadToSendData );

            var AnnouncementDetails = _mapper.Map<IEnumerable<AnnouncementDto>>(Announcements);
            return AnnouncementDetails;
        }
        /// <inheritdoc/>
        /*public async Task<string> InsertAnnouncementDetails(Announcement AnnouncementingDto)
        {

            var Announcement = _mapper.Map<Announcement>(AnnouncementingDto);
            var insertedData = await _AnnouncementRepository.InsertAnnouncementDetails(Announcement);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Announcement insertion failed.");
            }
           // return _mapper.Map<Announcement>(insertedData);*/

        /*  return insertedData;

      }*/
        public async Task<AnnouncementDto> InsertAnnouncementDetails(AnnouncementDto AnnouncementingDto)
        {

            var Announcement = _mapper.Map<Announcement>(AnnouncementingDto);
            var insertedData = await _AnnouncementRepository.InsertAnnouncementDetails(Announcement);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Announcement insertion failed.");
            }
            return _mapper.Map<AnnouncementDto>(insertedData);

        }
        /// <inheritdoc/>
       /* public async Task UpdateAnnouncementDetails(Announcement Announcement)
        {
            var Announcement = _mapper.Map<Announcement>(Announcement);
            await _AnnouncementRepository.UpdateAnnouncementDetails(Announcement);
        }*/
        /// <inheritdoc/>
        public async Task<bool> DeleteAnnouncementDetails(int id)
        {
            return await _AnnouncementRepository.DeleteAnnouncementDetails(id);
        }
    }
}
