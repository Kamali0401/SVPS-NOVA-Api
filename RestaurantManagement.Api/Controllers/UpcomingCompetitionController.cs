using RestaurantManagement.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using RestaurantManagement.Application.Interfaces;
using SonaNova.Application.Dtos;
using RestaurantManagement.Application.Services;
using RestaurantManagement.Domain.Entities;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace RestaurantManagement.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on UpcomingCompetitionDto.
    /// </summary>
    [Route("api/upcomingCompetition")]
    [ApiController]
    public class UpcomingCompetitionController : SonaNovaControllerBase
    {


        private readonly IUpcomingCompetitionService _UpcomingCompetitionService;
        /// <summary>
        /// Initializes a new instance of the <see cref="UpcomingCompetitionController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="UpcomingCompetitionService">The UpcomingCompetitionDto service instance used for CRUD operations on UpcomingCompetitionDto.</param>
        public UpcomingCompetitionController(ILogger<UpcomingCompetitionController> logger, IUpcomingCompetitionService UpcomingCompetition) : base(logger)
        {
            _UpcomingCompetitionService = UpcomingCompetition;
        }

        /// <summary>
        /// Retrieves all UpcomingCompetitionDto.
        /// </summary>
        /// <returns>
        /// The response with a collection of UpcomingCompetitionDto DTOs if successful, or a problem 
        /// details object indicating the error if the operation fails.
        /// </returns>
        /*[HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UpcomingCompetitionDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllUpcomingCompetition()
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetAllUpcomingCompetition));
            try
            {
                var result = await _UpcomingCompetitionService.GetUpcomingCompetition(null);
                _logger.LogDebug(result.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        */

        /// <summary>
        /// Retrieves a UpcomingCompetitionDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the UpcomingCompetitionDto.</param>
        /// <returns>
        /// The response with the UpcomingCompetitionDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UpcomingCompetitionDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetUpcomingCompetitionById(int? id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetUpcomingCompetitionById), id);
            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var UpcomingCompetitionDto = await _UpcomingCompetitionService.GetUpcomingCompetitionbyId(id);
                _logger.LogDebug(UpcomingCompetitionDto.ToString());
                return UpcomingCompetitionDto.Count() == 1 ? Ok(UpcomingCompetitionDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Retrieves all upcoming competitions based on the user's role and optional ID.
        /// </summary>
        /// <param name="role">The role of the user (e.g., Student, Faculty).</param>
        /// <param name="id">Optional user identifier to filter competitions.</param>
        /// <returns>
        /// The response with a collection of UpcomingCompetition DTOs if successful, 
        /// or a problem details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("byrole/{role}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UpcomingCompetition>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllUpcomingCompetition(string role, int? id)
        {
            _logger.LogInformation("{MethodName} method is called with Role: {role}, Id: {id}", nameof(GetAllUpcomingCompetition), role, id);

            try
            {
                var result = await _UpcomingCompetitionService.GetUpcomingCompetition(role, id);

                /*if (result == null || !result.Any())
                {
                    return NoContent();
                }

                foreach (var comp in result)
                {
                    if (!string.IsNullOrWhiteSpace(comp.FileNames))
                    {
                        var files = comp.FileNames.Split('|').ToList();

                        if (files.Count > 0 && string.IsNullOrWhiteSpace(files.Last()))
                        {
                            files.RemoveAt(files.Count - 1);
                        }

                        comp.Files = files;
                    }
                }*/
                if (result == null)
                {
                    return NoContent();
                }

                foreach (var Student in result)
                {
                    if (!string.IsNullOrWhiteSpace(Student.FileNames))
                    {
                        var filesList = Student.FileNames.Split('|').ToList();


                        if (filesList.Count > 0 && string.IsNullOrWhiteSpace(filesList.Last()))
                        {
                            filesList.RemoveAt(filesList.Count - 1);
                        }

                        Student.Files = filesList;
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
        /// Inserts a new UpcomingCompetitionDto.
        /// </summary>
        /// <param name="UpcomingCompetitionDto">The DTO representing the UpcomingCompetitionDto to insert.</param>
        /// <returns>
        /// The response with the created UpcomingCompetitionDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> InsertUpcomingCompetition([FromBody] UpcomingCompetitionDto UpcomingCompetitionDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertUpcomingCompetition));
            try
            {
                // Insert the UpcomingCompetitionDto and retrieve the data
                var UpcomingCompetitionDetail = await _UpcomingCompetitionService.InsertUpcomingCompetition(UpcomingCompetitionDto);

                _logger.LogDebug(UpcomingCompetitionDetail.ToString());
                //  return CreatedAtAction(nameof(GetAllUpcomingCompetition), new { id = UpcomingCompetitionDto.Id }, UpcomingCompetitionDetail);
                return CreatedAtAction(
         nameof(GetUpcomingCompetitionById), // <-- Make sure this method exists
         new { id = UpcomingCompetitionDetail.Id },
         UpcomingCompetitionDetail
     );
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
        /// Updates an existing UpcomingCompetitionDto.
        /// </summary>
        /// <param name="UpcomingCompetitionDto">The DTO representing the updated UpcomingCompetitionDto.</param>
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
        public async Task<IActionResult> UpdateUpcomingCompetition([FromBody] UpcomingCompetitionDto UpcomingCompetitionDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(UpdateUpcomingCompetition));
            var inventoryDetails = await _UpcomingCompetitionService.GetUpcomingCompetitionbyId((int?)UpcomingCompetitionDto.Id);
            //var existingRecord = await _UpcomingCompetitionService.GetUpcomingCompetitionById(UpcomingCompetitionDto.Id);
            if (inventoryDetails == null)
            {
                return NotFound();
            }

            try
            {
                await _UpcomingCompetitionService.UpdateUpcomingCompetition(UpcomingCompetitionDto);
                _logger.LogDebug("UpcomingCompetition with Id: {Id} updated successfully.", UpcomingCompetitionDto.Id);
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
        /// Deletes a UpcomingCompetitionDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the UpcomingCompetitionDto to delete.</param>
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
        public async Task<IActionResult> DeleteUpcomingCompetition(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteUpcomingCompetition), id);
            var UpcomingCompetitionDto = await _UpcomingCompetitionService.GetUpcomingCompetitionbyId(id);
            if (UpcomingCompetitionDto == null)
            {
                return NotFound();
            }

            try
            {
                await _UpcomingCompetitionService.DeleteUpcomingCompetitionDetails(id);
                _logger.LogDebug("UpcomingCompetition with Id: {Id} deleted successfully.", id);
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
        [HttpPut("interested/{studentId}/{competitionId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UpcomingCompetition>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> UpdateInterestedCompetition(int studentId, int competitionId)
        {
            _logger.LogInformation("{MethodName} called with studentId: {studentId}, competitionId: {competitionId}",
                nameof(UpdateInterestedCompetition), studentId, competitionId);

            try
            {
                var result = await _UpcomingCompetitionService.UpdateInterestedCompetition(studentId, competitionId);

                if (result == null || !result.Any())
                {
                    return NoContent();
                }

                _logger.LogDebug("Student interest updated successfully.");
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
        [HttpGet("CompetitionList")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<FileResult> GetInterestedStudentList(int competitionId)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetInterestedStudentList), competitionId);
            try
            {
                var result = await _UpcomingCompetitionService.GetInterestedStudentList(competitionId);
                //_logger.LogDebug(result.ToString());
                return await PrepareFileForDownload(result.ToString(), "Excel");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        private async Task<FileResult> PrepareFileForDownload(string result, string type)
        {
            try
            {
                var downloadData = await _UpcomingCompetitionService.DownloadData(result);
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
