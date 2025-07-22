using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Api.Controllers;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Services;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using System.Data.SqlClient;
using System.Net;

namespace SonaNova.Api.Controllers
{ /// <summary>
  /// Controller for handling CRUD operations on MarkDto.
  /// </summary>
    [Route("api/mark")]
    [ApiController]
    public class MarkController : SonaNovaControllerBase
    {
       
        


            private readonly IMarkServices _MarkService;
            /// <summary>
            /// Initializes a new instance of the <see cref="MarkController"/> class.
            /// </summary>
            /// <param name="logger">The logger instance used for logging.</param>
            /// <param name="MarkService">The MarkDto service instance used for CRUD operations on MarkDto.</param>
            public MarkController(ILogger<MarkController> logger, IMarkServices MarkService) : base(logger)
            {
                _MarkService = MarkService;
            }

            /// <summary>
            /// Retrieves all MarkDto.
            /// </summary>
            /// <returns>
            /// The response with a collection of MarkDto DTOs if successful, or a problem 
            /// details object indicating the error if the operation fails.
            /// </returns>
            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<MarkDto>))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> GetStudentMark()
            {
                _logger.LogInformation("{MethodName} method is called", nameof(GetStudentMark));
                try
                {
                    var result = await _MarkService.GetStudentMark();
                    _logger.LogDebug(result.ToString());
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }


            /// <summary>
            /// Retrieves a MarkDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the MarkDto.</param>
            /// <returns>
            /// The response with the MarkDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("{id}")]
            [ProducesResponseType(200, Type = typeof(MarkDto))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

            public async Task<IActionResult> GetStudentMarkById(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetStudentMarkById), id);
                if (id < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                try
                {
                    var MarkDto = await _MarkService.GetStudentMarkById(id);
                    _logger.LogDebug(MarkDto.ToString());
                    return MarkDto.Count() == 1 ? Ok(MarkDto) : StatusCode(StatusCodes.Status404NotFound);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

        /// <summary>
        /// Retrieves a MarkDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the MarkDto.</param>
        /// <returns>
        /// The response with the MarkDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("markByStudentId/{studentId}")]
        [ProducesResponseType(200, Type = typeof(MarkDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetStudentMarkByStudentId(int studentId)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetStudentMarkByStudentId), studentId);
            if (studentId < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var MarkDto = await _MarkService.GetStudentMarkByStudentId(studentId);
                _logger.LogDebug(MarkDto.ToString());
                // return MarkDto.Count() == 1 ? Ok(MarkDto) : StatusCode(StatusCodes.Status404NotFound);
                return Ok(MarkDto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Inserts a new MarkDto.
        /// </summary>
        /// <param name="MarkDto">The DTO representing the MarkDto to insert.</param>
        /// <returns>
        /// The response with the created MarkDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
            [ProducesResponseType(201)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> InsertMark([FromBody] MarkDto MarkDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(InsertMark));
                try
                {
                    // Insert the MarkDto and retrieve the data
                    var MarkDetail = await _MarkService.InsertMarkDetails(MarkDto);
                    _logger.LogDebug(MarkDto.ToString());

                    return CreatedAtAction(nameof(GetStudentMark), new { id = MarkDto.Id }, MarkDetail);
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
            /// Updates an existing MarkDto.
            /// </summary>
            /// <param name="MarkDto">The DTO representing the updated MarkDto.</param>
            /// <returns>
            /// The response with no content if the update is successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpPost("ReadyToSendEmail")]
            [ProducesResponseType(200)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> UpdateMark(bool ReadytosendEmail)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(UpdateMark));
            try
            {
                var result = await _MarkService.UpdateReadytosendEmail(ReadytosendEmail);

                if (result == null)
                {
                    return NotFound("No records were updated.");
                }

                _logger.LogDebug("Update result: {Result}", result.ToString());
                return Ok(result);
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

            


        [HttpDelete("deleteMarks")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> DeleteMark([FromBody] List<MarkDto> marks)
        {
            _logger.LogInformation("{MethodName} method is called for deleting marks", nameof(DeleteMark));

            if (marks == null)
            {
                return NotFound();
            }

            try
            {
                var result = await _MarkService.DeleteMarkDetails(marks);

                if (result == null)
                {
                    return NotFound();
                }

                _logger.LogDebug("DeleteMark result: {Result}", result.ToString());
                return Ok(result);
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
        [HttpGet("studentmarkreport")]
        [ProducesResponseType(typeof(FileResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<FileResult> GetAllMarkReport(string Section, string subjects, string test)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetStudentMark), Section, subjects, test);
            try
            {
                var result = await _MarkService.GetAllMarkReport(Section, subjects, test);

                // Optionally log data
                //_logger.LogDebug(result.ToString());

                // Convert to JSON string or Excel format string, based on how you implement PrepareFileForDownload
                //var dataString = System.Text.Json.JsonSerializer.Serialize(result);

                return await PrepareFileForDownload(result, "Excel"); // or "CSV", etc.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching student marks.");
                throw; // Or return InternalServerError(ex) if custom error response is needed
            }
        }
        private async Task<FileResult> PrepareFileForDownload(string result, string type)
        {
            try
            {
                var downloadData = await _MarkService.DownloadData(result);
                if (downloadData.memory == null || string.IsNullOrEmpty(downloadData.path))
                {
                    return null;
                }

                downloadData.memory.Position = 0;
                string contentType = string.Empty;
                if (type == "Word") contentType = "application/vnd.ms-word";
                else if (type == "Excel") contentType = "application/vnd.ms-excel";
                else contentType = "application/pdf";

                // Reading the file bytes and converting to Base64 (if needed for some reason)
                byte[] fileBytes = System.IO.File.ReadAllBytes(result);
                string base64String = Convert.ToBase64String(fileBytes);

                // Returning the file as a downloadable result
                return File(downloadData.memory, contentType, Path.GetFileName(downloadData.path));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error preparing file for download: {ex.Message}");
                throw;
            }
        }

    }
}
