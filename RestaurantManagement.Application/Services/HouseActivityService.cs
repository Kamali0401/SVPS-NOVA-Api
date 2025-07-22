using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Interfaces;
using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Services
{
    /// <summary>
    /// Service class for performing CRUD operations on HouseActivitys.
    /// </summary>
    public class HouseActivityService : IHouseActivityService
    {
        private readonly IHouseActivityRepository _houseActivityRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="HouseActivityService"/> class.
        /// </summary>
        /// <param name="HouseActivityRepository">The repository for accessing HouseActivityDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public HouseActivityService(IHouseActivityRepository houseActivityRepository, IMapper mapper)
        {
            _houseActivityRepository = houseActivityRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<HouseActivityDto>> GetHouseActivity(int? id)
        {
            var HouseActivitys = await _houseActivityRepository.GetHouseActivity(id);

            var HouseActivityDetails = _mapper.Map<IEnumerable<HouseActivityDto>>(HouseActivitys);
            return HouseActivityDetails;
        }
        /// <inheritdoc/>
        public async Task<HouseActivityDto> InsertHouseActivity(HouseActivityDto houseActivityDto)
        {

            var HouseActivity = _mapper.Map<HouseActivity>(houseActivityDto);
            var insertedData = await _houseActivityRepository.InsertHouseActivity(HouseActivity);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("HouseActivity insertion failed.");
            }
            return _mapper.Map<HouseActivityDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateHouseActivity(HouseActivityDto houseActivityDto)
        {
            var HouseActivity = _mapper.Map<HouseActivity>(houseActivityDto);
            await _houseActivityRepository.UpdateHouseActivity(HouseActivity);
        }
        /// <inheritdoc/>
        public async Task<string> DeleteHouseActivityDetails(int id)
        {
            return await _houseActivityRepository.DeleteHouseActivityDetails(id);
        }

        public async Task<IEnumerable<HousePointModelDto>> GetHousePointDetails()
        {
            var HouseActivitys = await _houseActivityRepository.GetHousePointDetails();

            var HouseActivityDetails = _mapper.Map<IEnumerable<HousePointModelDto>>(HouseActivitys);
            return HouseActivityDetails;
        }
    }
}
