using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Api.Controllers;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;

namespace SonaNova.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on UploadAttachmentsDto.
    /// </summary>
    [Route("api/uploadAttachment")]
    [ApiController]
    [Authorize]
    public class UploadAttachmentsController : SonaNovaControllerBase
    {


        private readonly IUploadAttachmentsService _UploadAttachmentsService;
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadAttachmentsController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="UploadAttachmentsService">The UploadAttachmentsDto service instance used for CRUD operations on UploadAttachmentsDto.</param>
        public UploadAttachmentsController(ILogger<UploadAttachmentsController> logger, IUploadAttachmentsService UploadAttachmentsService) : base(logger)
        {
            _UploadAttachmentsService = UploadAttachmentsService;
        }
        [HttpPost("uploadAtivityAttachments")]
        public async Task<IActionResult> UploadFile([FromForm] UploadfileDto fileUploadModel)
        {
            if (fileUploadModel.FormFiles != null)
            {
                //subDirectory = subDirectory ?? string.Empty;
                var target = Path.Combine(Directory.GetCurrentDirectory().ToString(), fileUploadModel.ActivityName, fileUploadModel.ActivityName + "-" + fileUploadModel.Id);

                //Path.Combine(_appSettings.Settings.UploadFilePath.ToString(), fileUploadModel.ActivityName, fileUploadModel.ActivityName + "-" + fileUploadModel.Id);
                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }
                for (int i = 0; i < fileUploadModel.FormFiles.Count; i++)
                {
                    string path = Path.Combine(target, fileUploadModel.FormFiles[i].FileName);
                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        await fileUploadModel.FormFiles[i].CopyToAsync(stream);
                    }
                }
                string filenames = "";
                if (target != "")
                {
                    string[] filePaths = Directory.GetFiles(target);
                    foreach (var file in filePaths)
                    {
                        filenames = filenames + Path.GetFileName(file) + "|";

                    }

                }
                var result = await _UploadAttachmentsService.UpdateActivityFilepathdata(target, fileUploadModel.Id, filenames);
            }
            return Ok();
        }

      

        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(FileUploadDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UploadfacultystudentFile([FromForm] FileUploadDto fileUploadModel)
        {
            if (fileUploadModel.FormFiles != null)
            {
                var target = Path.Combine(Directory.GetCurrentDirectory(), fileUploadModel.TypeofUser, fileUploadModel.TypeofUser + "-" + fileUploadModel.Id);

                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }

                // Get existing files in the directory
                List<string> existingFiles = Directory.GetFiles(target).Select(Path.GetFileName).ToList();

                // Upload new files
                for (int i = 0; i < fileUploadModel.FormFiles.Count; i++)
                {
                    string path = Path.Combine(target, fileUploadModel.FormFiles[i].FileName);
                    // Check if file already exists to prevent duplication
                    if (!System.IO.File.Exists(path))
                    {
                        using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                        {
                            await fileUploadModel.FormFiles[i].CopyToAsync(stream);
                        }

                        // Add new file name to the list
                        existingFiles.Add(fileUploadModel.FormFiles[i].FileName);
                    }
                }

                // Join filenames with '|'
                string filenames = string.Join("|", existingFiles) + "|";

                // Update file path data in the database
                var result = await _UploadAttachmentsService.UpdateFilepathdata(target, fileUploadModel.Id, filenames, fileUploadModel.TypeofUser);
            }

            return Ok();
        }

    }
}
