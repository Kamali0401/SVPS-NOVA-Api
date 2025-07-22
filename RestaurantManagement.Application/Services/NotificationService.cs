using AutoMapper;
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
    /// Service class for performing CRUD operations on Notifications.
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _NotificationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="NotificationRepository">The repository for accessing NotificationDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public NotificationService(INotificationRepository NotificationRepository, IMapper mapper)
        {
            _NotificationRepository = NotificationRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<NotificationDto>> GetNotificationById(int? id)
        {
            var Notifications = await _NotificationRepository.GetNotificationById(id);

            var NotificationDetails = _mapper.Map<IEnumerable<NotificationDto>>(Notifications);
            return NotificationDetails;
        }

        public async Task<IEnumerable<NotificationDto>> GetAllNotification(int studentId, string role)
        {
            var Notifications = await _NotificationRepository.GetAllNotification(studentId, role);

            var NotificationDetails = _mapper.Map<IEnumerable<NotificationDto>>(Notifications);
            return NotificationDetails;
        }
        /// <inheritdoc/>
        /*public async Task<string> InsertNotificationDetails(NotificationDto NotificationingDto)
        {

            var Notification = _mapper.Map<Notification>(NotificationingDto);
            var insertedData = await _NotificationRepository.InsertNotificationDetails(Notification);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Notification insertion failed.");
            }
           // return _mapper.Map<NotificationDto>(insertedData);*/

        /*  return insertedData;

      }*/
      /*  public async Task<string> InsertNotificationDetails(NotificationDto NotificationDto)
        {
            var Notification = _mapper.Map<Notification>(NotificationDto);

            var feedbackList = new List<Notification> { Notification };

            var insertedResult = await _NotificationRepository.InsertNotificationDetails(feedbackList);

            if (string.IsNullOrEmpty(insertedResult))
            {
                throw new Exception("Student feedback insertion failed.");
            }

            return insertedResult;
        }*/
        /// <inheritdoc/>
      public async Task UpdateNotificationDetails(NotificationDto NotificationDto)
        {
            var Notification = _mapper.Map<Notification>(NotificationDto);
            await _NotificationRepository.UpdateNotificationDetails(Notification);
        }
        /// <inheritdoc/>
       /* public async Task<bool> DeleteNotificationDetails(int id)
        {
            return await _NotificationRepository.DeleteNotificationDetails(id);
        }*/
    }
}
