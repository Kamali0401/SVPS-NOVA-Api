using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Api.Controllers;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using System.Data.SqlClient;
using System.IO.Compression;
using System.Net;
using System.Web;

namespace SonaNova.Api.Controllers
{ /// <summary>
  /// Controller for handling CRUD operations on AnnouncementDto.
  /// </summary>
    [Route("api/Announcement")]
    [ApiController]
    [Authorize]
    public class AnnouncementController : SonaNovaControllerBase
    {
       
        

            private readonly IAnnouncementService _AnnouncementService;
            /// <summary>
            /// Initializes a new instance of the <see cref="AnnouncementController"/> class.
            /// </summary>
            /// <param name="logger">The logger instance used for logging.</param>
            /// <param name="AnnouncementService">The AnnouncementDto service instance used for CRUD operations on AnnouncementDto.</param>
            public AnnouncementController(ILogger<AnnouncementController> logger, IAnnouncementService AnnouncementService) : base(logger)
            {
                _AnnouncementService = AnnouncementService;
            }

            /// <summary>
            /// Retrieves all AnnouncementDto.
            /// </summary>
            /// <returns>
            /// The response with a collection of AnnouncementDto DTOs if successful, or a problem 
            /// details object indicating the error if the operation fails.
            /// </returns>
            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<AnnouncementDto>))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> GetAllAnnouncement(int? id, bool isReadToSendData = false)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(GetAllAnnouncement), id, isReadToSendData);
                try
                {
                    var result = await _AnnouncementService.GetAllAnnouncement(id, isReadToSendData);

                    if (result == null)
                    {
                        return NoContent();
                    }
                    foreach (var assignment in result)
                    {
                        if (!string.IsNullOrWhiteSpace(assignment.FileNames))
                        {
                            var fileList = assignment.FileNames.Split('|').ToList();

                            // Remove last empty entry if FileName ends with a '|'
                            if (fileList.Count > 0 && string.IsNullOrWhiteSpace(fileList.Last()))
                            {
                                fileList.RemoveAt(fileList.Count - 1);
                            }

                            assignment.Files = fileList;
                        }
                    }
                    _logger.LogDebug(result.ToString());
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }


            /// <summary>
            /// Retrieves a AnnouncementDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the AnnouncementDto.</param>
            /// <returns>
            /// The response with the AnnouncementDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("Id/{id}")]
            [ProducesResponseType(200, Type = typeof(AnnouncementDto))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

            public async Task<IActionResult> GetAnnouncementById(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetAnnouncementById), id);
                if (id < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                try
                {
                    var AnnouncementDto = await _AnnouncementService.GetAnnouncement(id);
                    _logger.LogDebug(AnnouncementDto.ToString());
                    return AnnouncementDto.Count() == 1 ? Ok(AnnouncementDto) : StatusCode(StatusCodes.Status404NotFound);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            /// <summary>
            /// Inserts a new AnnouncementDto.
            /// </summary>
            /// <param name="AnnouncementDto">The DTO representing the AnnouncementDto to insert.</param>
            /// <returns>
            /// The response with the created AnnouncementDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpPost]
            [ProducesResponseType(201)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> InsertAnnouncement([FromBody] AnnouncementDto AnnouncementDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(InsertAnnouncement));
                try
                {
                    // Insert the AnnouncementDto and retrieve the data
                    var AnnouncementDetail = await _AnnouncementService.InsertAnnouncementDetails(AnnouncementDto);
                    _logger.LogDebug(AnnouncementDto.ToString());

                    return CreatedAtAction(nameof(GetAllAnnouncement), new { id = AnnouncementDto.Id }, AnnouncementDetail);
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "SQL Error occurred: {Message}", ex.Message);
                    return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
                    {
                        Title = "Database Error",
                        Detail = "An error occurred while processing your request. Please try again later.",
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

            /// <summary>
            /// Updates an existing AnnouncementDto.
            /// </summary>
            /// <param name="AnnouncementDto">The DTO representing the updated AnnouncementDto.</param>
            /// <returns>
            /// The response with no content if the update is successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
          /*  [HttpPut]
            [ProducesResponseType(204)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> UpdateAnnouncement([FromBody] AnnouncementDto AnnouncementDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(UpdateAnnouncement));
                var AnnouncementDetails = await _AnnouncementService.GetAnnouncement((int?)AnnouncementDto.Id);
                if (AnnouncementDetails == null)
                {
                    return NotFound();
                }

                try
                {
                    await _AnnouncementService.UpdateAnnouncementDetails(AnnouncementDto);
                    return NoContent();
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "SQL Error occurred: {Message}", ex.Message);
                    return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
                    {
                        Title = "Database Error",
                        Detail = "An error occurred while processing your request. Please try again later.",
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
            }*/

            /// <summary>
            /// Deletes a AnnouncementDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the AnnouncementDto to delete.</param>
            /// <returns>
            /// The response with no content if the deletion is successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpDelete("{id}")]
            [ProducesResponseType(204)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> DeleteAnnouncement(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteAnnouncement), id);
                var AnnouncementDto = await _AnnouncementService.GetAnnouncement(id);
                if (AnnouncementDto == null)
                {
                    return NotFound();
                }

                try
                {
                    await _AnnouncementService.DeleteAnnouncementDetails(id);
                    return NoContent();
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "SQL Error occurred: {Message}", ex.Message);
                    return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
                    {
                        Title = "Database Error",
                        Detail = "An error occurred while processing your request. Please try again later.",
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

        [HttpGet("downloadAnnouncementFiles")]
        public async Task<IActionResult> DownloadAnnouncementFiles(int id, bool? isReadToSendData = false)
        {
            var isReadToSend = isReadToSendData == null ? false : isReadToSendData;
            var result1 = await _AnnouncementService.GetAllAnnouncement(id, (bool)isReadToSend);
            var firstItem = result1.FirstOrDefault();

            if (firstItem == null)
            {
                return NotFound("No activity data found.");
            }

            var zipName = $"archive-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}.zip";
            var filePath = firstItem.Filepath;

            if (!Directory.Exists(filePath))
            {
                return NotFound("File directory not found.");
            }

            var files = Directory.GetFiles(filePath).ToList();

            MemoryStream compressedFileStream = new MemoryStream();

            using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, true))
            {

                files.ForEach(file =>
                {
                    //Create a zip entry for each attachment
                    var zipEntry = zipArchive.CreateEntry(Path.GetFileName(file));
                    byte[] bytes = System.IO.File.ReadAllBytes(file);
                    //Get the stream of the attachment
                    using (var originalFileStream = new MemoryStream(bytes))
                    using (var zipEntryStream = zipEntry.Open())
                    {
                        //Copy the attachment stream to the zip entry stream
                        originalFileStream.CopyTo(zipEntryStream);
                    }
                });


            }
            const string contentType = "application/zip";
            var result = new FileContentResult(compressedFileStream.ToArray(), contentType)
            {
                FileDownloadName = $"{zipName}.zip"
            };
            return result;

        }
        [HttpGet("Transulate")]
        public String translate(String EnglishTranslate, string From, string To)
        {
            var fromLanguage = From;
            var toLanguage = To;
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(EnglishTranslate)}";
            var webclient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webclient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4
                    , StringComparison.Ordinal) - 4);
                return result;
            }
            catch (Exception e1)
            {
                return "error";
            }

        }

    }
}
