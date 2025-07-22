using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Api.Controllers;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using System.Data.SqlClient;
using System.Net;

namespace SonaNova.Api.Controllers
{/// <summary>
 /// Controller for handling CRUD operations on AcademicCalendarDto.
 /// </summary>
    [Route("api/academicCalendar")]
    [ApiController]
    public class AcademicCalendarController : SonaNovaControllerBase
    { 
       

            private readonly IAcademicCalendarService _AcademicCalendarService;
            /// <summary>
            /// Initializes a new instance of the <see cref="AcademicCalendarController"/> class.
            /// </summary>
            /// <param name="logger">The logger instance used for logging.</param>
            /// <param name="AcademicCalendarService">The AcademicCalendarDto service instance used for CRUD operations on AcademicCalendarDto.</param>
            public AcademicCalendarController(ILogger<AcademicCalendarController> logger, IAcademicCalendarService AcademicCalendarService) : base(logger)
            {
                _AcademicCalendarService = AcademicCalendarService;
            }

            /// <summary>
            /// Retrieves all AcademicCalendarDto.
            /// </summary>
            /// <returns>
            /// The response with a collection of AcademicCalendarDto DTOs if successful, or a problem 
            /// details object indicating the error if the operation fails.
            /// </returns>
            [HttpGet()]
            [ProducesResponseType(200, Type = typeof(IEnumerable<AcademicCalendarDto>))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> GetAllAcademicCalendar([FromQuery] string? role = null)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(GetAllAcademicCalendar),role);
                try
                {
                    var result = await _AcademicCalendarService.GetAllAcademicCalendar(role);
                    _logger.LogDebug(result.ToString());
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }


            /// <summary>
            /// Retrieves a AcademicCalendarDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the AcademicCalendarDto.</param>
            /// <returns>
            /// The response with the AcademicCalendarDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("Id/{id}")]
            [ProducesResponseType(200, Type = typeof(AcademicCalendarDto))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

            public async Task<IActionResult> GetAcademicCalendarById(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetAcademicCalendarById), id);
                if (id < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                try
                {
                    var AcademicCalendarDto = await _AcademicCalendarService.GetAcademicCalendar(id);
                    _logger.LogDebug(AcademicCalendarDto.ToString());
                    return AcademicCalendarDto.Count() == 1 ? Ok(AcademicCalendarDto) : StatusCode(StatusCodes.Status404NotFound);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            /// <summary>
            /// Inserts a new AcademicCalendarDto.
            /// </summary>
            /// <param name="AcademicCalendarDto">The DTO representing the AcademicCalendarDto to insert.</param>
            /// <returns>
            /// The response with the created AcademicCalendarDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpPost]
            [ProducesResponseType(201)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> InsertAcademicCalendar([FromBody] AcademicCalendarDto AcademicCalendarDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(InsertAcademicCalendar));
                try
                {
                    // Insert the AcademicCalendarDto and retrieve the data
                    var AcademicCalendarDetail = await _AcademicCalendarService.InsertAcademicCalendarDetails(AcademicCalendarDto);
                    _logger.LogDebug(AcademicCalendarDto.ToString());

                    return CreatedAtAction(nameof(GetAllAcademicCalendar), new { id = AcademicCalendarDto.Id }, AcademicCalendarDetail);
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
            /// Updates an existing AcademicCalendarDto.
            /// </summary>
            /// <param name="AcademicCalendarDto">The DTO representing the updated AcademicCalendarDto.</param>
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
            public async Task<IActionResult> UpdateAcademicCalendar([FromBody] AcademicCalendarDto AcademicCalendarDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(UpdateAcademicCalendar));
                var AcademicCalendarDetails = await _AcademicCalendarService.GetAcademicCalendar((int?)AcademicCalendarDto.Id);
                if (AcademicCalendarDetails == null)
                {
                    return NotFound();
                }

                try
                {
                    await _AcademicCalendarService.UpdateAcademicCalendarDetails(AcademicCalendarDto);
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
            /// Deletes a AcademicCalendarDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the AcademicCalendarDto to delete.</param>
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
            public async Task<IActionResult> DeleteAcademicCalendar(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteAcademicCalendar), id);
                var AcademicCalendarDto = await _AcademicCalendarService.GetAcademicCalendar(id);
                if (AcademicCalendarDto == null)
                {
                    return NotFound();
                }

                try
                {
                    await _AcademicCalendarService.DeleteAcademicCalendarDetails(id);
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
        
    }
}
