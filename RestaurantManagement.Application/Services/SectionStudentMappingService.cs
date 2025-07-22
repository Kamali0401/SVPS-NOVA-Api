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
    public  class SectionStudentMappingService : ISectionStudentMappingService
    {
        private readonly ISectionStudentMappingRepository _SectionStudentMappingRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionStudentMappingService"/> class.
        /// </summary>
        /// <param name="SectionStudentMappingRepository">The repository for accessing SectionStudentMappingDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public SectionStudentMappingService(ISectionStudentMappingRepository SectionStudentMappingRepository, IMapper mapper)
        {
            _SectionStudentMappingRepository = SectionStudentMappingRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<SectionStudentMappingDto>> GetSectionStudentMapping(int? id)
        {
            var SectionStudentMappings = await _SectionStudentMappingRepository.GetSectionStudentMapping(id);

            var SectionStudentMappingDetails = _mapper.Map<IEnumerable<SectionStudentMappingDto>>(SectionStudentMappings);
            return SectionStudentMappingDetails;
        }


        /// <inheritdoc/>
        /*public async Task<string> InsertSectionStudentMappingDetails(SectionStudentMappingDto SectionStudentMappingingDto)
        {

            var SectionStudentMapping = _mapper.Map<SectionStudentMapping>(SectionStudentMappingingDto);
            var insertedData = await _SectionStudentMappingRepository.InsertSectionStudentMappingDetails(SectionStudentMapping);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("SectionStudentMapping insertion failed.");
            }
           // return _mapper.Map<SectionStudentMappingDto>(insertedData);*/

        /*  return insertedData;

      }*/
        public async Task<int> InsertSectionStudentMappingDetails(List<SectionStudentMappingDto> sectionStudentMappingDtos)
        {
            var sectionStudentMappings = _mapper.Map<List<SectionStudentMapping>>(sectionStudentMappingDtos);

            var insertedResult = await _SectionStudentMappingRepository.InsertSectionStudentMappingDetails(sectionStudentMappings);

           /* if (insertedResult <= 0)
            {
                throw new Exception("Section-Student mapping insertion failed.");
            }*/

            return insertedResult;
        }

        /// <inheritdoc/>
        public async Task<int> UpdateSectionStudentMappingDetails(List<SectionStudentMappingDto> sectionStudentMappingDtos)
        {
            var sectionStudentMappings = _mapper.Map<List<SectionStudentMapping>>(sectionStudentMappingDtos);

            return await _SectionStudentMappingRepository.UpdateSectionStudentMappingDetails(sectionStudentMappings);
        }
        /// <inheritdoc/>
        public async Task<int> DeleteSectionStudentMappingDetails(int[] ids, int batchId)
        {
            return await _SectionStudentMappingRepository.DeleteSectionStudentMappingDetails(ids, batchId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<StudentDropdownModelDto>> GetMappedStudentByName(string StudentName, int SectionId)
        {
            var SectionStudentMappings = await _SectionStudentMappingRepository.GetMappedStudentByName(StudentName, SectionId);

            var SectionStudentMappingDetails = _mapper.Map<IEnumerable<StudentDropdownModelDto>>(SectionStudentMappings);
            return SectionStudentMappingDetails;
        }

        /// <inheritdoc/>
        
    }
}
