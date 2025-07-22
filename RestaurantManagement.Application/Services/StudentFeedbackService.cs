using AutoMapper;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using SonaNova.Domain.Entities;
using SonaNova.Infrastructure.Interfaces;
using SonaNova.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Services
{
    /// <summary>
    /// Service class for performing CRUD operations on StudentFeedbacks.
    /// </summary>
    public class StudentFeedbackService : IStudentFeedbackService
    {
        private readonly IStudentFeedbackRepository _StudentFeedbackRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentFeedbackService"/> class.
        /// </summary>
        /// <param name="StudentFeedbackRepository">The repository for accessing StudentFeedbackDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public StudentFeedbackService(IStudentFeedbackRepository StudentFeedbackRepository, IMapper mapper)
        {
            _StudentFeedbackRepository = StudentFeedbackRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<StudentFeedbackDto>> GetStudentFeedback(int? id)
        {
            var StudentFeedbacks = await _StudentFeedbackRepository.GetStudentFeedback(id);

            var StudentFeedbackDetails = _mapper.Map<IEnumerable<StudentFeedbackDto>>(StudentFeedbacks);
            return StudentFeedbackDetails;
        }

        public async Task<IEnumerable<StudentFeedbackDto>> GetAllStudentFeedback(string role, int? id)
        {
            var StudentFeedbacks = await _StudentFeedbackRepository.GetAllStudentFeedback(role, id);

            var StudentFeedbackDetails = _mapper.Map<IEnumerable<StudentFeedbackDto>>(StudentFeedbacks);
            return StudentFeedbackDetails;
        }
        /// <inheritdoc/>
        /*public async Task<string> InsertStudentFeedbackDetails(StudentFeedbackDto StudentFeedbackingDto)
        {

            var StudentFeedback = _mapper.Map<StudentFeedback>(StudentFeedbackingDto);
            var insertedData = await _StudentFeedbackRepository.InsertStudentFeedbackDetails(StudentFeedback);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("StudentFeedback insertion failed.");
            }
           // return _mapper.Map<StudentFeedbackDto>(insertedData);*/

        /*  return insertedData;

      }*/
        public async Task<string> InsertStudentFeedbackDetails(List<StudentFeedbackDto> studentFeedbackDto)
        {
            /*var studentFeedback = _mapper.Map<StudentFeedback>(studentFeedbackDto);

            var feedbackList = new List<StudentFeedback> { studentFeedback };

            var insertedResult = await _StudentFeedbackRepository.InsertStudentFeedbackDetails(feedbackList);

            if (string.IsNullOrEmpty(insertedResult))
            {
                throw new Exception("Student feedback insertion failed.");
            }

            return insertedResult;*/
            var feedbackList = _mapper.Map<List<StudentFeedback>>(studentFeedbackDto); // ✅ Correct mapping

            var insertedResult = await _StudentFeedbackRepository.InsertStudentFeedbackDetails(feedbackList);

            if (string.IsNullOrEmpty(insertedResult))
            {
                throw new Exception("Student feedback insertion failed.");
            }

            return insertedResult;
        }




        /// <inheritdoc/>
        /* public async Task UpdateStudentFeedbackDetails(StudentFeedbackDto StudentFeedbackDto)
         {
             var StudentFeedback = _mapper.Map<StudentFeedback>(StudentFeedbackDto);
             await _StudentFeedbackRepository.UpdateStudentFeedbackDetails(StudentFeedback);
         }*/
        /// <inheritdoc/>
        public async Task<List<StudentFeedbackDto>> DeleteStudentFeedbackDetails(string id)
        {
            //return await _StudentFeedbackRepository.DeleteStudentFeedbackDetails(id);
            var entityList = await _StudentFeedbackRepository.DeleteStudentFeedbackDetails(id);
            var dtoList = _mapper.Map<List<StudentFeedbackDto>>(entityList);
            return dtoList;
        }
    }
}
