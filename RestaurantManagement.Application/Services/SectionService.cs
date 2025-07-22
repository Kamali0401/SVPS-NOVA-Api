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
    /// Service class for performing CRUD operations on SectionDto.
    /// </summary>
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _SectionDtoRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionService"/> class.
        /// </summary>
        /// <param name="SectionDtoRepository">The repository for accessing SectionDtoDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public SectionService(ISectionRepository SectionDtoRepository, IMapper mapper)
        {
            _SectionDtoRepository = SectionDtoRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<SectionDto>> GetSectionDetails(int? id)
        {
            var SectionDtos = await _SectionDtoRepository.GetSectionDetails(id);

            var SectionDtoDetails = _mapper.Map<IEnumerable<SectionDto>>(SectionDtos);
            return SectionDtoDetails;
        }
        /// <inheritdoc/>
        public async Task<SectionDto> InsertSectionDetails(SectionDto SectionDtoDto)
        {

            var SectionDto = _mapper.Map<SectionMaster>(SectionDtoDto);
            var insertedData = await _SectionDtoRepository.InsertSectionDetails(SectionDto);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("SectionDto insertion failed.");
            }
            return _mapper.Map<SectionDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateSectionDetails(SectionDto SectionDtoDto)
        {
            var SectionDto = _mapper.Map<SectionMaster>(SectionDtoDto);
            await _SectionDtoRepository.UpdateSectionDetails(SectionDto);
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteSectionDetails(int id)
        {
            return await _SectionDtoRepository.DeleteSectionDetails(id);
        }
    }
}
