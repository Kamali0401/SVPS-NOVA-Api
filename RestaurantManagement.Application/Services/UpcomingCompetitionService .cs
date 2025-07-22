using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Interfaces;
using SonaNova.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Services
{
    /// <summary>
    /// Service class for performing CRUD operations on UpcomingCompetitions.
    /// </summary>
    public class UpcomingCompetitionService : IUpcomingCompetitionService
    {
        private readonly IUpcomingCompetitionRepository _UpcomingCompetitionRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpcomingCompetitionService"/> class.
        /// </summary>
        /// <param name="UpcomingCompetitionRepository">The repository for accessing UpcomingCompetitionDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public UpcomingCompetitionService(IUpcomingCompetitionRepository UpcomingCompetitionRepository, IMapper mapper)
        {
            _UpcomingCompetitionRepository = UpcomingCompetitionRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<UpcomingCompetitionDto>> GetUpcomingCompetition(string role, int? id)
        {
            var UpcomingCompetitions = await _UpcomingCompetitionRepository.GetUpcomingCompetition(role, id);

            var UpcomingCompetitionDetails = _mapper.Map<IEnumerable<UpcomingCompetitionDto>>(UpcomingCompetitions);
            return UpcomingCompetitionDetails;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<UpcomingCompetitionDto>> GetUpcomingCompetitionbyId(int? id)
        {
            var UpcomingCompetitions = await _UpcomingCompetitionRepository.GetUpcomingCompetitionbyId(id);

            var UpcomingCompetitionDetails = _mapper.Map<IEnumerable<UpcomingCompetitionDto>>(UpcomingCompetitions);
            return UpcomingCompetitionDetails;
        }
        /// <inheritdoc/>
        public async Task<UpcomingCompetitionDto> InsertUpcomingCompetition(UpcomingCompetitionDto UpcomingCompetitionDto)
        {

            var UpcomingCompetition = _mapper.Map<UpcomingCompetition>(UpcomingCompetitionDto);
            var insertedData = await _UpcomingCompetitionRepository.InsertUpcomingCompetition(UpcomingCompetition);
            if (insertedData == null)
            {
                // Handle the case where the insertion was not successful
                throw new Exception("UpcomingCompetition insertion failed.");
            }
            return _mapper.Map<UpcomingCompetitionDto>(insertedData);

        }
        /// <inheritdoc/>
        public async Task UpdateUpcomingCompetition(UpcomingCompetitionDto UpcomingCompetitionDto)
        {
            var UpcomingCompetition = _mapper.Map<UpcomingCompetition>(UpcomingCompetitionDto);
            await _UpcomingCompetitionRepository.UpdateUpcomingCompetition(UpcomingCompetition);
        }
        /// <inheritdoc/>
        public async Task<bool> DeleteUpcomingCompetitionDetails(int id)
        {
            return await _UpcomingCompetitionRepository.DeleteUpcomingCompetitionDetails(id);
        }

        public async Task<IEnumerable<UpcomingCompetitionDto>> UpdateInterestedCompetition(int studentId, int competitionI)
        {
            var UpcomingCompetitions = await _UpcomingCompetitionRepository.UpdateInterestedCompetition( studentId,  competitionI);

            var UpcomingCompetitionDetails = _mapper.Map<IEnumerable<UpcomingCompetitionDto>>(UpcomingCompetitions);
            return UpcomingCompetitionDetails;
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
        /// <inheritdoc/>
        public async Task<string> GetInterestedStudentList(int competitionId)
        {
            var UpcomingCompetitions = await _UpcomingCompetitionRepository.GetInterestedStudentList(competitionId);

           // var UpcomingCompetitionDetails = _mapper.Map<IEnumerable<UpcomingCompetitionDto>>(UpcomingCompetitions);
            return UpcomingCompetitions;
        }
    }
}
