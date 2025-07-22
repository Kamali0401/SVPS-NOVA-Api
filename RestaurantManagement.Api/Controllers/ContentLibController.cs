using RestaurantManagement.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Services;
using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantManagement.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on ContentLibingDto.
    /// </summary>
    [Route("api/ContentLib")]
    [ApiController]
    [Authorize]
    public class ContentLibController : SonaNovaControllerBase
    {


        private readonly IContentLibService _ContentLibService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLibController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="ContentLibService">The ContentLibingDto service instance used for CRUD operations on ContentLibingDto.</param>
        public ContentLibController(ILogger<ContentLibController> logger, IContentLibService ContentLibService) : base(logger)
        {
            _ContentLibService = ContentLibService;
        }

        /// <summary>
        /// Retrieves all ContentLibingDto.
        /// </summary>
        /// <returns>
        /// The response with a collection of ContentLibingDto DTOs if successful, or a problem 
        /// details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ContentLibDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllContentLibs()
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetAllContentLibs));
            try
            {
                var result = await _ContentLibService.GetContentLibDetails(null);
                if (result == null)
                {
                    return NoContent();
                }

                foreach (var student in result)
                {
                    if (!string.IsNullOrWhiteSpace(student.FileName))
                    {
                        var fileList = student.FileName.Split('|').ToList();

                        if (fileList.Count > 0 && string.IsNullOrWhiteSpace(fileList.Last()))
                        {
                            fileList.RemoveAt(fileList.Count - 1);
                        }

                        student.FileList = fileList;
                    }
                }
                return Ok(result);

                //return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Retrieves a ContentLibingDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ContentLibingDto.</param>
        /// <returns>
        /// The response with the ContentLibingDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ContentLibDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetContentLibById(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetContentLibById), id);
            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var ContentLibingDto = await _ContentLibService.GetContentLibDetails(id);
                return ContentLibingDto.Count() == 1 ? Ok(ContentLibingDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Inserts a new ContentLibingDto.
        /// </summary>
        /// <param name="ContentLibingDto">The DTO representing the ContentLibingDto to insert.</param>
        /// <returns>
        /// The response with the created ContentLibingDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> InsertContentLibing([FromBody] ContentLibDto ContentLibingDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertContentLibing));
            try
            {
                // Insert the ContentLibingDto and retrieve the data
                var ContentLibingDetail = await _ContentLibService.InsertContentLibDetails(ContentLibingDto);


                return CreatedAtAction(nameof(GetAllContentLibs), new { id = ContentLibingDto.Id }, ContentLibingDetail);
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
        /// Updates an existing ContentLibingDto.
        /// </summary>
        /// <param name="ContentLibingDto">The DTO representing the updated ContentLibingDto.</param>
        /// <returns>
        /// The response with no content if the update is successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> UpdateContentLib([FromBody] ContentLibDto ContentLibingDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(UpdateContentLib));
            var ContentLibingDetails = await _ContentLibService.GetContentLibDetails((int?)ContentLibingDto.Id);
           if (ContentLibingDetails == null)
           {
                return NotFound();
           }

            try
            {
                await _ContentLibService.UpdateContentLibDetails(ContentLibingDto);
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

        /// <summary>
        /// Deletes a ContentLibingDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ContentLibingDto to delete.</param>
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
        public async Task<IActionResult> DeleteContentLib(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteContentLib), id);
            var ContentLibingDto = await _ContentLibService.GetContentLibDetails(id);
            if (ContentLibingDto == null)
            {
                return NotFound();
            }

            try
            {
                await _ContentLibService.DeleteContentLibDetails(id);
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
        [HttpGet("StudentId/{student}")]
        [ProducesResponseType(200, Type = typeof(ContentLibDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetAllContentLibByStudent(int student)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetAllContentLibByStudent), student);
            if (student < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var ContentLibingDto = await _ContentLibService.GetAllContentLibByStudent(student);
                return ContentLibingDto.Count() == 1 ? Ok(ContentLibingDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet("downloadcontentlibFiles")]
        public async Task<IActionResult> DownloadContentLibFiles(int id)
        {
            var result1 = await _ContentLibService.GetContentLibDetails(id);
            var content = result1.FirstOrDefault(); // ✅ Fix here

            if (content == null)
            {
                return NotFound("No content library data found for the given ID.");
            }

            var filePath = content.FileName;
            if (string.IsNullOrEmpty(filePath) || !Directory.Exists(filePath))
            {
                return NotFound("File path is invalid or does not exist.");
            }

            var files = Directory.GetFiles(filePath).ToList();
            if (files.Count == 0)
            {
                return NotFound("No files found in the directory.");
            }

            var zipName = $"archive-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}";
           // MemoryStream compressedFileStream = new MemoryStream();

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
    }
}
