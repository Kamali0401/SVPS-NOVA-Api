using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantManagement.Api.Controllers;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Services;
using SonaNova.Application.Common;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.Net;

namespace SonaNova.Api.Controllers
{ /// <summary>
  /// Controller for handling CRUD operations on InfoGaloreDto.
  /// </summary>
    [Route("api/infogalore")]
    [ApiController]
    public class InfoGaloreController : SonaNovaControllerBase
    {
       
        


            private readonly IInfoGaloreService _InfoGaloreCostService;
            /// <summary>
            /// Initializes a new instance of the <see cref="InfoGaloreController"/> class.
            /// </summary>
            /// <param name="logger">The logger instance used for logging.</param>
            /// <param name="InfoGaloreService">The InfoGaloreDto service instance used for CRUD operations on InfoGaloreDto.</param>
            public InfoGaloreController(ILogger<InfoGaloreController> logger, IInfoGaloreService InfoGaloreCostService) : base(logger)
            {
                _InfoGaloreCostService = InfoGaloreCostService;
            }

        /// <summary>
        /// Retrieves all InfoGaloreDto.
        /// </summary>
        /// <returns>
        /// The response with a collection of InfoGaloreDto DTOs if successful, or a problem 
        /// details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllInfoGalore(string infoType, int? id)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetAllInfoGalore));

            try
            {
                var result = await _InfoGaloreCostService.GetAllInfoGalore(infoType, id);

                _logger.LogDebug(JsonConvert.SerializeObject(result));

                if (result == null || !result.Any())
                {
                    return NotFound(new ProblemDetails
                    {
                        Title = "No Content",
                        Detail = "No information found for the provided parameters.",
                        Status = (int)HttpStatusCode.NotFound
                    });
                }

                string destination = Path.Combine(Directory.GetCurrentDirectory(), "Attachments", "InfoGalore", $"InfoGalore-{id}");
                Directory.CreateDirectory(destination);

                var extractedFiles = new ConcurrentBag<InfoAttachmentModelDto>();
                var processedFiles = new ConcurrentDictionary<string, bool>();

                await Parallel.ForEachAsync(result, async (entry, _) =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(entry.InfoFilePath) && Directory.Exists(entry.InfoFilePath))
                        {
                            var files = Directory.GetFiles(entry.InfoFilePath);

                            foreach (var file in files)
                            {
                                var fileName = Path.GetFileName(file);
                                var destFile = Path.Combine(destination, fileName);

                                if (!processedFiles.ContainsKey(destFile))
                                {
                                    processedFiles[destFile] = true;

                                    if (!System.IO.File.Exists(destFile))
                                    {
                                        System.IO.File.Copy(file, destFile, true);
                                    }

                                    var attachment = new InfoAttachmentModelDto
                                    {
                                        FileName = fileName,
                                        FilePath = destFile,
                                        InfoType = entry.InfoType
                                    };

                                    if (FileIsAnImageChecker.IsImageFile(file))
                                    {
                                        attachment.BlobData = await System.IO.File.ReadAllBytesAsync(file);
                                    }

                                    extractedFiles.Add(attachment);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error processing files for entry {entry.InfoFilePath}: {ex.Message}");
                    }
                });

                return Ok(extractedFiles);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred: {Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
                {
                    Title = "Database Error",
                    Detail = "An error occurred while accessing the database. Please try again later.",
                    Status = (int)HttpStatusCode.InternalServerError
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred. Please try again later.",
                    Status = (int)HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> InsertInfoGalore([FromForm] InfoGaloreDto infoGaloreModel)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertInfoGalore));

            try
            {
                var result = await _InfoGaloreCostService.InsertInfoGalore(infoGaloreModel);
                if (result == null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Title = "Insert Failed",
                        Detail = "The insert operation did not return any records.",
                        Status = (int)HttpStatusCode.NotFound
                    });
                }

                if (infoGaloreModel.InfoFile != null)
                {
                    var target = Path.Combine(Directory.GetCurrentDirectory(), "InfoGalore", "InfoGalore-" + result.Id);

                    if (!Directory.Exists(target))
                    {
                        Directory.CreateDirectory(target);
                    }

                    foreach (var file in infoGaloreModel.InfoFile)
                    {
                        string filePath = Path.Combine(target, file.FileName);
                        using (Stream stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }

                     await _InfoGaloreCostService.UpdateInfoGalore(result.Id, target);
                    
                }

                return Ok();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred: {Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
                {
                    Title = "Database Error",
                    Detail = "An error occurred while accessing the database. Please try again later.",
                    Status = (int)HttpStatusCode.InternalServerError
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred. Please try again later.",
                    Status = (int)HttpStatusCode.InternalServerError
                });
            }
        }
        [HttpGet("viewAttachments")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AttachmentModelDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAttachment(int id, string type)
        {

            _logger.LogInformation("{MethodName} method is called", nameof(GetAttachment));
            try
            {

                var attachmentData = await _InfoGaloreCostService.GetAttachmentAsync(id, type);
                return Ok(attachmentData);
            }
            catch (InvalidDataException)
            {
                return Ok("Success: No valid file found.");
            }

        }
    }
}
