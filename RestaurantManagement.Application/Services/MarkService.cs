using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.Infrastructure.Repositories;
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
    /// Service class for performing CRUD operations on Marks.
    /// </summary>
    public class MarkServices : IMarkServices
    {
        private readonly IMarkRepository _MarkRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkService"/> class.
        /// </summary>
        /// <param name="MarkRepository">The repository for accessing MarkDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public MarkServices(IMarkRepository MarkRepository, IMapper mapper)
        {
            _MarkRepository = MarkRepository;
            _mapper = mapper;
        }
       
        public async Task<IEnumerable<MarkDto>> GetStudentMark()
        {
            var Marks = await _MarkRepository.GetStudentMark();

        var MarkDetails = _mapper.Map<IEnumerable<MarkDto>>(Marks);
            return MarkDetails;
        }
    public async Task<IEnumerable<MarkDto>> GetStudentMarkById(int? id)
        {
            var Marks = await _MarkRepository.GetStudentMarkById(id);

            var MarkDetails = _mapper.Map<IEnumerable<MarkDto>>(Marks);
            return MarkDetails;
        }
        /// <inheritdoc/>
        public async Task<MarkDto> InsertMarkDetails(MarkDto MarkingDto)
        {

            var Mark = _mapper.Map<Mark>(MarkingDto);
            var insertedData = await _MarkRepository.InsertMarkDetails(Mark);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("Mark insertion failed.");
            }
            return _mapper.Map<MarkDto>(insertedData);

        }
        public async Task<IEnumerable<MarkDto>> GetStudentMarkByStudentId(int studentId)
        {
            var Marks = await _MarkRepository.GetStudentMarkByStudentId(studentId);

            var MarkDetails = _mapper.Map<IEnumerable<MarkDto>>(Marks);
            return MarkDetails;
        }
        /// <inheritdoc/>
        public async Task<string> DeleteMarkDetails(List<MarkDto> marks)
        {
            var mappedMarks = _mapper.Map<List<Mark>>(marks);
            var deleted = await _MarkRepository.DeleteMarkDetails(mappedMarks);

            if (string.IsNullOrEmpty(deleted))
            {
                throw new Exception("Mark deletion failed.");
            }
            return deleted;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<MarkDto>> UpdateReadytosendEmail(bool ReadytosendEmail)
        {
            var updatedMarks = await _MarkRepository.UpdateReadytosendEmail(ReadytosendEmail);
            return _mapper.Map<IEnumerable<MarkDto>>(updatedMarks);
        }

        public async Task<(MemoryStream memory, string path)> DownloadData(string filepath)
        {
            var Path = filepath;
            var memorys = new MemoryStream();
            using (var stream = new FileStream(Path, FileMode.Open))
            {
                await stream.CopyToAsync(memorys);
            }
            return (memory: memorys, path: Path);
        }
        public virtual async Task<string> GetAllMarkReport(string Section, string subjects, string test)
        {
            return await _MarkRepository.GetAllMarkReport(Section, subjects, test);
        }

    }
}
