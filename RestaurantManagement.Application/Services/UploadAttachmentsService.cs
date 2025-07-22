using AutoMapper;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.Infrastructure.Repositories;
using SonaNova.Application.Interfaces;
using SonaNova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonaNova.Application.Services
{
    public class UploadAttachmentsService : IUploadAttachmentsService
    {
        private readonly IUploadAttachmentsRepository _UploadAttachmentsRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadAttachmentsnService"/> class.
        /// </summary>
        /// <param name="UploadAttachmentsnRepository">The repository for accessing UploadAttachmentsnDto data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
        public UploadAttachmentsService(IUploadAttachmentsRepository UploadAttachmentsRepository, IMapper mapper)
        {
            _UploadAttachmentsRepository = UploadAttachmentsRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public virtual async Task<string> UpdateActivityFilepathdata(string target, int id, string files)
        {
            try
            {
                return await _UploadAttachmentsRepository.UpdateActivityFilepathdata(target, id, files);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<string> UpdateFilepathdata(string target, int id, string files, string TypeofUser)
        {
            try
            {
                return await _UploadAttachmentsRepository.UpdateFilepathdata(target, id, files, TypeofUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
