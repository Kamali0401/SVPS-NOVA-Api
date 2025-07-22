using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Constants;
using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Services
{
    /// <summary>
    /// Service class for performing CRUD operations on Assignments.
    /// </summary>
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _AssignmentRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentService"/> class.
        /// </summary>
        /// <param name="AssignmentRepository">The repository for accessing AssignmentDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public AssignmentService(IAssignmentRepository AssignmentRepository, IMapper mapper)
        {
            _AssignmentRepository = AssignmentRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<AssignmentDto>> GetAssignment(int? id)
        {
            var Assignments = await _AssignmentRepository.GetAssignment(id);

            var AssignmentDetails = _mapper.Map<IEnumerable<AssignmentDto>>(Assignments);
            return AssignmentDetails;
        }
        /// <inheritdoc/>
        public async Task<AssignmentDto> InsertAssignmentDetails(AssignmentDto AssignmentingDto)
        {

            var Assignment = _mapper.Map<Assignment>(AssignmentingDto);
            var insertedData = await _AssignmentRepository.InsertAssignmentDetails(Assignment);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Assignment insertion failed.");
            }
            return _mapper.Map<AssignmentDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateAssignmentDetails(AssignmentDto AssignmentDto)
        {
            var Assignment = _mapper.Map<Assignment>(AssignmentDto);
            await _AssignmentRepository.UpdateAssignmentDetails(Assignment);
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteAssignmentDetails(int id)
        {
            return await _AssignmentRepository.DeleteAssignmentDetails(id);
        }
        public async Task<IEnumerable<AssignmentDto>> GetAllAssignmentByStudent(string role, int studentId)
        {
            var Activitys = await _AssignmentRepository.GetAllAssignmentByStudent(role, studentId);

            var ActivityDetails = _mapper.Map<IEnumerable<AssignmentDto>>(Activitys);
            return ActivityDetails;
        }

    }
}
