using RestaurantManagement.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using SonaNova.Application.Dtos;

using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Services;
using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantManagement.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on AssignmentDto.
    /// </summary>
    [Route("api/assignment")]
    [ApiController]
    [Authorize]
    public class AssignmentController : SonaNovaControllerBase
    {


        private readonly IAssignmentService _AssignmentService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="AssignmentService">The AssignmentDto service instance used for CRUD operations on AssignmentDto.</param>
        public AssignmentController(ILogger<AssignmentController> logger, IAssignmentService AssignmentService) : base(logger)
        {
            _AssignmentService = AssignmentService;
        }

        /// <summary>
        /// Retrieves all AssignmentDto.
        /// </summary>
        /// <returns>
        /// The response with a collection of AssignmentDto DTOs if successful, or a problem 
        /// details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AssignmentDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllAssignment()
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetAllAssignment));
            try
            {
                var result = await _AssignmentService.GetAssignment(null);
                if (result == null)
                {
                    return NoContent();
                }
                foreach (var assignment in result)
                {
                    if (!string.IsNullOrWhiteSpace(assignment.FileName))
                    {
                        var fileList = assignment.FileName.Split('|').ToList();

                        // Remove last empty entry if FileName ends with a '|'
                        if (fileList.Count > 0 && string.IsNullOrWhiteSpace(fileList.Last()))
                        {
                            fileList.RemoveAt(fileList.Count - 1);
                        }

                        assignment.FileList = fileList;
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
        /// Retrieves a AssignmentDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AssignmentDto.</param>
        /// <returns>
        /// The response with the AssignmentDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AssignmentDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetAssignmentById(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetAssignmentById), id);
            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var AssignmentDto = await _AssignmentService.GetAssignment(id);
                _logger.LogDebug(AssignmentDto.ToString());
                return AssignmentDto.Count() == 1 ? Ok(AssignmentDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Inserts a new AssignmentDto.
        /// </summary>
        /// <param name="AssignmentDto">The DTO representing the AssignmentDto to insert.</param>
        /// <returns>
        /// The response with the created AssignmentDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> InsertAssignment([FromBody] AssignmentDto AssignmentDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertAssignment));
            try
            {
                // Insert the AssignmentDto and retrieve the data
                var AssignmentDetail = await _AssignmentService.InsertAssignmentDetails(AssignmentDto);
                _logger.LogDebug(AssignmentDto.ToString());

                return CreatedAtAction(nameof(GetAllAssignment), new { id = AssignmentDto.Id }, AssignmentDetail);
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
        /// Updates an existing AssignmentDto.
        /// </summary>
        /// <param name="AssignmentDto">The DTO representing the updated AssignmentDto.</param>
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
        public async Task<IActionResult> UpdateAssignment([FromBody] AssignmentDto AssignmentDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(UpdateAssignment));
            var AssignmentDetails = await _AssignmentService.GetAssignment((int?)AssignmentDto.Id);
            if (AssignmentDetails == null)
            {
                return NotFound();
            }

            try
            {
                await _AssignmentService.UpdateAssignmentDetails(AssignmentDto);
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
        /// Deletes a AssignmentDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AssignmentDto to delete.</param>
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
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteAssignment), id);
            var AssignmentDto = await _AssignmentService.GetAssignment(id);
            if (AssignmentDto == null)
            {
                return NotFound();
            }

            try
            {
                await _AssignmentService.DeleteAssignmentDetails(id);
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

        [HttpGet("{role}/{studentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AssignmentDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllAssignmentByStudent( string role,  int studentId)
        {
            _logger.LogInformation("{MethodName} method is called with Type: {Type}, DepartmentId: {DepartmentId}", nameof(GetAllAssignmentByStudent), role, studentId);

            try
            {
                var result = await _AssignmentService.GetAllAssignmentByStudent(role, studentId);

                if (result == null)
                {
                    return NoContent();
                }
                foreach (var assignment in result)
                {
                    if (!string.IsNullOrWhiteSpace(assignment.FileName))
                    {
                        var fileList = assignment.FileName.Split('|').ToList();

                        // Remove last empty entry if FileName ends with a '|'
                        if (fileList.Count > 0 && string.IsNullOrWhiteSpace(fileList.Last()))
                        {
                            fileList.RemoveAt(fileList.Count - 1);
                        }

                        assignment.FileList = fileList;
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


        [HttpGet("downloadassignmentFiles")]
        public async Task<IActionResult> DownloadAssignmentFiles(int id)
        {
            var result1 = await _AssignmentService.GetAssignment(id);
            var assignment = result1.FirstOrDefault(); // ✅ Fix: Get first item safely

            if (assignment == null)
            {
                return NotFound("Assignment not found.");
            }

            var filePath = assignment.FileName; // ✅ Use the selected item
            if (string.IsNullOrEmpty(filePath) || !Directory.Exists(filePath))
            {
                return NotFound("File path is invalid or does not exist.");
            }

            var files = Directory.GetFiles(filePath).ToList();
            if (files.Count == 0)
            {
                return NotFound("No files found in the assignment folder.");
            }
            var zipName = $"archive-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}";

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
