using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Repositories;

namespace RestaurantManagement.Application.Services
{ /// <summary>
  /// Service class for performing CRUD operations on Faculty.
  /// </summary>
    public class FacultyService : IFacultyService
    {
        private readonly IFacultyRepository _FacultyRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacultyService"/> class.
        /// </summary>
        /// <param name="FacultyRepository">The repository for accessing FacultyDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public FacultyService(IFacultyRepository FacultyRepository, IMapper mapper)
        {
            _FacultyRepository = FacultyRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<FacultyDto>> GetFacultyDetails(int? id)
        {
            var Faculty = await _FacultyRepository.GetFacultyDetails(id);

            var Facultydto = _mapper.Map<IEnumerable<FacultyDto>>(Faculty);
            return Facultydto;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<FacultyDropdowndto>> GetFacultyByName(string facultyName)
        {
            var students = await _FacultyRepository.GetFacultyByName(facultyName);

            var studentDetailDetails = _mapper.Map<IEnumerable<FacultyDropdowndto>>(students);
            return studentDetailDetails;
        }
        /// <inheritdoc/>
        public async Task<FacultyDto> InsertFaculty(FacultyDto FacultyDto)
        {

            var Faculty = _mapper.Map<Faculty>(FacultyDto);
            var insertedData = await _FacultyRepository.InsertFaculty(Faculty);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Faculty insertion failed.");
            }
            return _mapper.Map<FacultyDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateFaculty(FacultyDto FacultyDto)
        {
            var Faculty = _mapper.Map<Faculty>(FacultyDto);
            await _FacultyRepository.UpdateFaculty(Faculty);
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteFaculty(int id)
        {
            return await _FacultyRepository.DeleteFaculty(id);
        }
    }
}
