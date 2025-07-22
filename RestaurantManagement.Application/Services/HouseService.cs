using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Services
{
    /// <summary>
    /// Service class for performing CRUD operations on Houses.
    /// </summary>
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _HouseRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="HouseService"/> class.
        /// </summary>
        /// <param name="HouseRepository">The repository for accessing HouseDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public HouseService(IHouseRepository HouseRepository, IMapper mapper)
        {
            _HouseRepository = HouseRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<HouseDto>> GetHouse(int? id)
        {
            var Houses = await _HouseRepository.GetHouse(id);

            var HouseDetails = _mapper.Map<IEnumerable<HouseDto>>(Houses);
            return HouseDetails;
        }
        /// <inheritdoc/>
        public async Task<HouseDto> InsertHouseDetails(HouseDto houseingDto)
        {

            var House = _mapper.Map<House>(houseingDto);
            var insertedData = await _HouseRepository.InsertHouseDetails(House);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("House insertion failed.");
            }
            return _mapper.Map<HouseDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateHouseDetails(HouseDto HouseDto)
        {
            var House = _mapper.Map<House>(HouseDto);
            await _HouseRepository.UpdateHouseDetails(House);
        }
        /// <inheritdoc/>
        public async Task<string> DeleteHouseDetails(int id)
        {
            return await _HouseRepository.DeleteHouseDetails(id);
        }


    }
}
