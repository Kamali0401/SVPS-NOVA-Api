using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Services;
using System.Data.SqlClient;
using System.IO.Compression;
using System.Net;

namespace RestaurantManagement.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on FacultyDto.
    /// </summary>
    [Route("api/faculty")]
    [ApiController]
    public class FacultyController : SonaNovaControllerBase
    {
        private readonly IFacultyService _FacultyService;
        /// <summary>
        /// Initializes a new instance of the <see cref="FacultyController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="FacultyService">The FacultyDto service instance used for CRUD operations on FacultyDto.</param>
        public FacultyController(ILogger<FacultyController> logger, IFacultyService FacultyService) : base(logger)
        {
            _FacultyService = FacultyService;
        }

        /// <summary>
        /// Retrieves all FacultyDto.
        /// </summary>
        /// <returns>
        /// The response with a collection of FacultyDto DTOs if successful, or a problem 
        /// details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FacultyDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllFacultys()
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetAllFacultys));
            try
            {
                var result = await _FacultyService.GetFacultyDetails(null);

                if (result == null )
                {
                    return NoContent();
                }

                foreach (var faculty in result)
                {
                    if (!string.IsNullOrWhiteSpace(faculty.FileNames))
                    {
                        var filesList = faculty.FileNames.Split('|').ToList();


                        if (filesList.Count > 0 && string.IsNullOrWhiteSpace(filesList.Last()))
                        {
                            filesList.RemoveAt(filesList.Count - 1);
                        }

                        faculty.files = filesList;
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        /// <summary>
        /// Retrieves a FacultyDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the FacultyDto.</param>
        /// <returns>
        /// The response with the FacultyDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(FacultyDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetFacultyDetailsById(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetFacultyDetailsById), id);
            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var FacultyDto = await _FacultyService.GetFacultyDetails(id);
                return FacultyDto.Count() == 1 ? Ok(FacultyDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Inserts a new FacultyDto.
        /// </summary>
        /// <param name="FacultyDto">The DTO representing the FacultyDto to insert.</param>
        /// <returns>
        /// The response with the created FacultyDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> InsertFaculty([FromBody] FacultyDto FacultyDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertFaculty));
            try
            {
                // Insert the FacultyDto and retrieve the data
                var FacultyDetail = await _FacultyService.InsertFaculty(FacultyDto);


                return CreatedAtAction(nameof(GetAllFacultys), new { id = FacultyDto.Id }, FacultyDetail);
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
        /// Updates an existing FacultyDto.
        /// </summary>
        /// <param name="FacultyDto">The DTO representing the updated FacultyDto.</param>
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
        public async Task<IActionResult> UpdateFaculty([FromBody] FacultyDto FacultyDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(UpdateFaculty));
            var Faculty = await _FacultyService.GetFacultyDetails((int?)FacultyDto.Id);
            if (Faculty == null)
            {
                return NotFound();
            }

            try
            {
                await _FacultyService.UpdateFaculty(FacultyDto);
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
        /// Deletes a FacultyDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the FacultyDto to delete.</param>
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
        public async Task<IActionResult> DeleteFaculty(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteFaculty), id);
            var FacultyDto = await _FacultyService.GetFacultyDetails(id);
            if (FacultyDto == null)
            {
                return NotFound();
            }

            try
            {
                await _FacultyService.DeleteFaculty(id);
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
        /// Retrieves a studentDto by its unique identifier.
        /// </summary>
        /// <param name="studentname">The unique identifier of the studentDto.</param>
        /// <returns>
        /// The response with the studentDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("name/{facultyName}")]
        [ProducesResponseType(200, Type = typeof(FacultyDropdowndto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetFacultyByName(string facultyName)
        {
            _logger.LogInformation("{MethodName} method is called for the studentname: {studentname}", nameof(GetFacultyByName), facultyName);

            try
            {
                /*var studentDto = await _FacultyService.GetFacultyByName(facultyName);

                if (studentDto == null)
                    return StatusCode(StatusCodes.Status404NotFound, "Faculty details not found.");

                return studentDto.Id != 0
                    ? Ok(studentDto)
                    : StatusCode(StatusCodes.Status404NotFound, "Faculty ID not valid.");*/

                var facultyList = await _FacultyService.GetFacultyByName(facultyName);

                if (facultyList == null || !facultyList.Any())
                    return StatusCode(StatusCodes.Status404NotFound, "Faculty details not found.");

                return Ok(facultyList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("downloadfacultyFiles")]
        public async Task<IActionResult> DownloadFacultyFiles(int id)
        {
            // Get faculty details
            var result1 = await _FacultyService.GetFacultyDetails(id);
            var faculty = result1.FirstOrDefault(); // Fix for indexing error
            if (faculty == null)
            {
                return NotFound("No faculty details found for the given ID.");
            }

            // Get file path
            var filePath = faculty.FilePath; // Using the first item directly
            if (string.IsNullOrEmpty(filePath) || !Directory.Exists(filePath))
            {
                return NotFound("File path is invalid or does not exist.");
            }

            // Get list of files
            var files = Directory.GetFiles(filePath).ToList();
            if (files.Count == 0)
            {
                return NotFound("No files available for the given ID.");
            }

            // Prepare zip archive
            var zipName = $"archive-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}";
            MemoryStream compressedFileStream = new MemoryStream();

            using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, true))
            {
                files.ForEach(file =>
                {
                    var zipEntry = zipArchive.CreateEntry(Path.GetFileName(file));
                    byte[] bytes = System.IO.File.ReadAllBytes(file);
                    using (var originalFileStream = new MemoryStream(bytes))
                    using (var zipEntryStream = zipEntry.Open())
                    {
                        originalFileStream.CopyTo(zipEntryStream);
                    }
                });
            }

            compressedFileStream.Seek(0, SeekOrigin.Begin); // Reset stream position
            const string contentType = "application/zip";
            var result = new FileContentResult(compressedFileStream.ToArray(), contentType)
            {
                FileDownloadName = $"{zipName}.zip"
            };
            return result;
        }

    }

}
