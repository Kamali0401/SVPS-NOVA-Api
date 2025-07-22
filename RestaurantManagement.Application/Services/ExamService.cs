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
    /// Service class for performing CRUD operations on Exams.
    /// </summary>
    public class ExamService : IExamService
    {
        private readonly IExamRepository _ExamRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExamService"/> class.
        /// </summary>
        /// <param name="ExamRepository">The repository for accessing ExamDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public ExamService(IExamRepository ExamRepository, IMapper mapper)
        {
            _ExamRepository = ExamRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<ExamDto>> GetExam(int? id)
        {
            var Exams = await _ExamRepository.GetExam(id);

            var ExamDetails = _mapper.Map<IEnumerable<ExamDto>>(Exams);
            return ExamDetails;
        }
        /// <inheritdoc/>
        public async Task<ExamDto> InsertExamDetails(ExamDto ExamingDto)
        {

            var Exam = _mapper.Map<Exam>(ExamingDto);
            var insertedData = await _ExamRepository.InsertExamDetails(Exam);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Exam insertion failed.");
            }
            return _mapper.Map<ExamDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateExamDetails(ExamDto ExamDto)
        {
            var Exam = _mapper.Map<Exam>(ExamDto);
            await _ExamRepository.UpdateExamDetails(Exam);
        }
        /// <inheritdoc/>
        public async Task<string> DeleteExamDetails(int id)
        {
            return await _ExamRepository.DeleteExamDetails(id);
        }
    }
}
