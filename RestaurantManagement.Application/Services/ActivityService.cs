using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Services
{
    /// <summary>
    /// Service class for performing CRUD operations on Activitys.
    /// </summary>
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _ActivityRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityService"/> class.
        /// </summary>
        /// <param name="ActivityRepository">The repository for accessing ActivityDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public ActivityService(IActivityRepository ActivityRepository, IMapper mapper)
        {
            _ActivityRepository = ActivityRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<ActivityDto>> GetActivityData(int? id)
        {
            var Activitys = await _ActivityRepository.GetActivityData(id);

            var ActivityDetails = _mapper.Map<IEnumerable<ActivityDto>>(Activitys);
            return ActivityDetails;
        }
        /// <inheritdoc/>
        public async Task<ActivityDto> InsertActivityData(ActivityDto ActivityDto)
        {

            var Activity = _mapper.Map<Activity>(ActivityDto);
            var insertedData = await _ActivityRepository.InsertActivityData(Activity);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Activity insertion failed.");
            }
            return _mapper.Map<ActivityDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateActivityData(ActivityDto ActivityDto)
        {
            var Activity = _mapper.Map<Activity>(ActivityDto);
            await _ActivityRepository.UpdateActivityData(Activity);
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteActivityData(int id)
        {
            return await _ActivityRepository.DeleteActivityData(id);
        }

        public async Task<IEnumerable<ActivityDto>> GetAllActivityData(int Type, long? DepartmentId)
        {
            var Activitys = await _ActivityRepository.GetAllActivityData(Type, DepartmentId);

            var ActivityDetails = _mapper.Map<IEnumerable<ActivityDto>>(Activitys);
            return ActivityDetails;
        }
    }
}
