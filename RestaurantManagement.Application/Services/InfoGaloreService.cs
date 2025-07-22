using AutoMapper;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure.Interfaces;
using SonaNova.Application.Common;
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
{  /// <summary>
   /// Service class for performing CRUD operations on InfoGalores.
   /// </summary>
    public class InfoGaloreService :IInfoGaloreService
    {
      
        
            private readonly IInfoGaloreRepository _InfoGaloreRepository;
            private readonly IMapper _mapper;

            /// <summary>
            /// Initializes a new instance of the <see cref="InfoGaloreService"/> class.
            /// </summary>
            /// <param name="InfoGaloreRepository">The repository for accessing InfoGaloreDto data.</param>
            /// <param name="mapper">The AutoMapper instance for mapping between entity and DTO.</param>
            public InfoGaloreService(IInfoGaloreRepository InfoGaloreRepository, IMapper mapper)
            {
                _InfoGaloreRepository = InfoGaloreRepository;
                _mapper = mapper;
            }
        /// <inheritdoc/>
        public async Task<InfoGaloreDto> InsertInfoGalore(InfoGaloreDto dto)
        {
            var entity = _mapper.Map<InfoGalore>(dto);
            var inserted = await _InfoGaloreRepository.InsertInfoGaloreDetails(entity);
            return _mapper.Map<InfoGaloreDto>(inserted);
        }

        public async Task UpdateInfoGalore(int id, string target)
        {
            await _InfoGaloreRepository.UpdateInfoGaloreDetails(id, target);
        }

        public async Task<IEnumerable<InfoGaloreDto>> GetAllInfoGalore(string infoType, int? id)
        {
            var entities = await _InfoGaloreRepository.GetAllInfoGalore(infoType, id);
            return _mapper.Map<IEnumerable<InfoGaloreDto>>(entities);
        }
        public async Task<List<AttachmentModelDto>> GetAttachmentAsync(int id, string type)
        {
            // Step 1: Define the action method based on the file type
            string actionMethodName = string.Empty;
            if (type == "Assignment")
            {
                actionMethodName = $"Assignment\\Assignment-{id}";
            }
            if (type == "Announcement")
            {
                actionMethodName = $"Announcement\\Announcement-{id}";
            }
            else if (type == "Students")
            {
                actionMethodName = $"Students\\Students-{id}";
            }
            else if (type == "ContentLib")
            {
                actionMethodName = $"ContentLib\\ContentLib-{id}";
            }
            else if (type == "Events")
            {
                actionMethodName = $"Events\\Events-{id}";
            }
            else if (type == "PressReports")
            {
                actionMethodName = $"PressReports\\PressReports-{id}";
            }
            else if (type == "Faculty")
            {
                actionMethodName = $"Facultys\\Facultys-{id}";
            }
            else if (type == "InfoGalore")
            {
                actionMethodName = "InfoGalore";
            }
            else if (type == "LeaveRequest")
            {
                actionMethodName = $"LeaveRequest\\LeaveRequest-{id}";
            }

            // Step 2: Define the source and destination folders
            string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), actionMethodName);
            string destination = Path.Combine(Directory.GetCurrentDirectory(), "Attachments", type, $"{type}-{id}");

            // Step 3: Check if the source folder exists; if not, return an empty list
            if (!Directory.Exists(outputFolder))
            {
                return new List<AttachmentModelDto>(); // Return an empty list if folder does not exist
            }

            // Ensure the folder exists by deleting it first if it's already there
            if (Directory.Exists(destination))
            {
                Directory.Delete(destination, true);
            }
            Directory.CreateDirectory(destination);

            // Step 4: Get all files from the source folder
            string[] files = Directory.GetFiles(outputFolder);

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destination, fileName);
                File.Copy(file, destFile, true); // Overwrites if file exists
            }

            List<AttachmentModelDto> extractedFiles = new List<AttachmentModelDto>();

            // Step 5: Process extracted files
            foreach (var filePath in Directory.GetFiles(destination))
            {
                var attachment = new AttachmentModelDto
                {
                    FileName = Path.GetFileName(filePath),
                    FilePath = filePath
                };

                if (FileIsAnImageChecker.IsImageFile(filePath))
                {
                    attachment.BlobData = await File.ReadAllBytesAsync(filePath);
                }

                extractedFiles.Add(attachment);
            }

            return extractedFiles;
        }
    }
}
