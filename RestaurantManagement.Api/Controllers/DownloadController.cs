using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Api.Controllers;
using RestaurantManagement.Application.Common;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using SonaNova.Application.Services;
using System.IO.Compression;
using System.Net;

namespace SonaNova.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on DownloadDto.
    /// </summary>
    [Route("api/downloads")]
    [ApiController]
    public class DownloadController : SonaNovaControllerBase
    {


        //private readonly IDownlaodService _DownloadService;
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="DownloadService">The DownloadDto service instance used for CRUD operations on DownloadDto.</param>
        public DownloadController(ILogger<DownloadController> logger) : base(logger)
        {
           // _DownloadService = DownloadService;
        }
       /* [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var message = await _DownloadService.GetStatus();
            return Ok(new { message });
        }*/

        [HttpGet("downloadTemplate")]
        [EnableCors]
        public async Task<IActionResult> DownloadTemplate()
        {
            var zipName = $"archive-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}.zip";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Template");
            var files = Directory.GetFiles(filePath).ToList();

            await using var compressedFileStream = new MemoryStream();
            using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    var zipEntry = zipArchive.CreateEntry(Path.GetFileName(file));

                    // Use async read
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(file);

                    await using var originalFileStream = new MemoryStream(bytes);
                    await using var zipEntryStream = zipEntry.Open();

                    await originalFileStream.CopyToAsync(zipEntryStream);
                }
            }

            compressedFileStream.Position = 0; // Reset stream position before returning
            const string contentType = "application/zip";

            return File(compressedFileStream.ToArray(), contentType, zipName);
        }
        [HttpGet("downloadAttachments")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> DownloadAttachment(int id, string type, string filename)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(DownloadAttachment));
            try
            {
                // Construct file path

                string filePath = (type != "InfoGalore") ? Path.Combine(Directory.GetCurrentDirectory(), "Attachments", type, $"{type}-{id}", filename) :
                    Path.Combine(Directory.GetCurrentDirectory(), "Attachments", type, $"{type}-", filename);
                _logger.LogInformation("File path: {FilePath}", filePath);

                // Check if the file exists
                if (!System.IO.File.Exists(filePath))
                {
                    _logger.LogWarning("File not found: {FilePath}", filePath);
                    return NotFound("File not found.");
                }

                // Prepare memory stream
                MemoryStream memory = new MemoryStream();

                await using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                // Get content type
                string contentType;
                try
                {
                    contentType = FindContentType.GetContentType(filePath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to determine content type");
                    return StatusCode(500, "Failed to determine content type.");
                }

                // Reset memory stream position
                memory.Position = 0;

                return File(memory, contentType, Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while downloading the attachment");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        

    }
}
