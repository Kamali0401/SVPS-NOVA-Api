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
    /// Service class for performing CRUD operations on ContentLibings.
    /// </summary>
    public class ContentLibService : IContentLibService
    {
        private readonly IContentLibRepository _ContentLibRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLibService"/> class.
        /// </summary>
        /// <param name="ContentLibRepository">The repository for accessing ContentLibDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public ContentLibService(IContentLibRepository ContentLibRepository, IMapper mapper)
        {
            _ContentLibRepository = ContentLibRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<ContentLibDto>> GetContentLibDetails(int? id)
        {
            var ContentLibs = await _ContentLibRepository.GetContentLibDetails(id);

            var ContentLibDetails = _mapper.Map<IEnumerable<ContentLibDto>>(ContentLibs);
            return ContentLibDetails;
        }
        /// <inheritdoc/>
        public async Task<ContentLibDto> InsertContentLibDetails(ContentLibDto ContentLibingDto)
        {

            var ContentLib = _mapper.Map<ContentLib>(ContentLibingDto);
            var insertedData = await _ContentLibRepository.InsertContentLibDetails(ContentLib);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("ContentLib insertion failed.");
            }
            return _mapper.Map<ContentLibDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateContentLibDetails(ContentLibDto ContentLibingDto)
        {
            var ContentLib = _mapper.Map<ContentLib>(ContentLibingDto);
            await _ContentLibRepository.UpdateContentLibDetails(ContentLib);
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteContentLibDetails(int id)
        {
            return await _ContentLibRepository.DeleteContentLibDetails(id);
        }

        public async Task<IEnumerable<ContentLibDto>> GetAllContentLibByStudent(int student)
        {
            var ContentLibs = await _ContentLibRepository.GetContentLibDetails(student);

            var ContentLibDetails = _mapper.Map<IEnumerable<ContentLibDto>>(ContentLibs);
            return ContentLibDetails;
        }
    }
}
