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
    /// Service class for performing CRUD operations on subjects.
    /// </summary>
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectService"/> class.
        /// </summary>
        /// <param name="SubjectRepository">The repository for accessing SubjectDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<SubjectDto>> GetSubjectDetails(int? id)
        {
            var Subjects = await _subjectRepository.GetSubjectDetails(id);

            var SubjectDetails = _mapper.Map<IEnumerable<SubjectDto>>(Subjects);
            return SubjectDetails;
        }
        /// <inheritdoc/>
        public async Task<SubjectDto> InsertSubjectDetails(SubjectDto subjectDto)
        {

            var Subject = _mapper.Map<Subject>(subjectDto);
            var insertedData = await _subjectRepository.InsertSubjectDetails(Subject);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Subject insertion failed.");
            }
            return _mapper.Map<SubjectDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateSubjectDetails(SubjectDto subjectDto)
        {
            var Subject = _mapper.Map<Subject>(subjectDto);
            await _subjectRepository.UpdateSubjectDetails(Subject);
        }
        /// <inheritdoc/>
        public async Task<string> DeleteSubjectDetails(int id)
        {
            return await _subjectRepository.DeleteSubjectDetails(id);
        }
        
        
    }
}
