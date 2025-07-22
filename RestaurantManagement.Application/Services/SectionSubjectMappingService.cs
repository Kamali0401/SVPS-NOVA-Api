using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure.Interfaces;
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
    /// Service class for performing CRUD operations on SectionSubjectMappings.
    /// </summary>
    public class SectionSubjectMappingService : ISectionSubjectMappingService
    {
        private readonly ISectionSubjectMappingRepository _SectionSubjectMappingRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionSubjectMappingService"/> class.
        /// </summary>
        /// <param name="SectionSubjectMappingRepository">The repository for accessing SectionSubjectMappingDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public SectionSubjectMappingService(ISectionSubjectMappingRepository SectionSubjectMappingRepository, IMapper mapper)
        {
            _SectionSubjectMappingRepository = SectionSubjectMappingRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<SectionSubjectMappingDto>> GetSectionSubjectMapping(int? id)
        {
            var SectionSubjectMappings = await _SectionSubjectMappingRepository.GetSectionSubjectMapping(id);

            var sectionSubjectMappingDetails = _mapper.Map<IEnumerable<SectionSubjectMappingDto>>(SectionSubjectMappings);
            return sectionSubjectMappingDetails;
        }
        /// <inheritdoc/>
        public async Task<SectionSubjectMappingDto> InsertSectionSubjectMappingDetails(SectionSubjectMappingDto SectionSubjectMappingingDto)
        {

            var SectionSubjectMapping = _mapper.Map<SectionSubjectMapping>(SectionSubjectMappingingDto);
            var insertedData = await _SectionSubjectMappingRepository.InsertSectionSubjectMappingDetails(SectionSubjectMapping);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("SectionSubjectMapping insertion failed.");
            }
            return _mapper.Map<SectionSubjectMappingDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateSectionSubjectMappingDetails(SectionSubjectMappingDto SectionSubjectMappingDto)
        {
            var SectionSubjectMapping = _mapper.Map<SectionSubjectMapping>(SectionSubjectMappingDto);
            await _SectionSubjectMappingRepository.UpdateSectionSubjectMappingDetails(SectionSubjectMapping);
        }
        /// <inheritdoc/>
        public async Task<string> DeleteSectionSubjectMappingDetails(int id)
        {
            return await _SectionSubjectMappingRepository.DeleteSectionSubjectMappingDetails(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SectionSubjectMappingDto>> GetFacultyListBySectionIdDetails(int sectionId)
        {
            var SectionSubjectMappings = await _SectionSubjectMappingRepository.GetFacultyListBySectionIdDetails(sectionId);

            var sectionSubjectMappingDetails = _mapper.Map<IEnumerable<SectionSubjectMappingDto>>(SectionSubjectMappings);
            return sectionSubjectMappingDetails;
        }
    }
 }
