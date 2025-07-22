using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.Infrastructure.Repositories;
using SonaNova.Application.Dtos;
using SonaNova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Services
{
    /// <summary>
    /// Service class for performing CRUD operations on students.
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentService"/> class.
        /// </summary>
        /// <param name="studentDetailRepository">The repository for accessing studentDetailDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public StudentService(IStudentRepository studentDetailRepository, IMapper mapper)
        {
            _studentRepository = studentDetailRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<StudentDto>> GetStudentDetails(int? id)
        {
            var students = await _studentRepository.GetStudentDetails(id);

            var studentDetailDetails = _mapper.Map<IEnumerable<StudentDto>>(students);
            return studentDetailDetails;
        }
        /// <inheritdoc/>
        public async Task<StudentDto> InsertStudent(StudentDto studentDto)
        {

            var HolidayCalendar = _mapper.Map<Students>(studentDto);
            var insertedData = await _studentRepository.InsertStudent(HolidayCalendar);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Student insertion failed.");
            }
            return _mapper.Map<StudentDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateStudent(StudentDto studentDto)
        {
            var HolidayCalendar = _mapper.Map<Students>(studentDto);
            await _studentRepository.UpdateStudent(HolidayCalendar);
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteStudent(int id)
        {
            return await _studentRepository.DeleteStudent(id);
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<StudentDropdownDto>> GetStudentByName(string studentname)
        {
            var students = await _studentRepository.GetStudentByName(studentname);

            var studentDetailDetails = _mapper.Map < IEnumerable<StudentDropdownDto>>(students);
            return studentDetailDetails;
        }
        public async Task<List<StudentSemDateModelDto>> GetAllStudentConfiguration(int? id)
        {
            var students = await _studentRepository.GetAllStudentConfiguration(id);

            var studentDetailDetails = _mapper.Map<List<StudentSemDateModelDto>>(students);
            return studentDetailDetails;
        }

        public async Task updateStudentSemDateDetails(StudentSemDateModelDto studentDto)
        {
            var HolidayCalendar = _mapper.Map<StudentSemDateModel>(studentDto);
            await _studentRepository.updateStudentSemDateDetails(HolidayCalendar);
        }
    }
}
