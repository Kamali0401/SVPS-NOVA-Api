using Microsoft.AspNetCore.Authorization;
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
{/// <summary>
 /// Controller for handling CRUD operations on ExamDto.
 /// </summary>
    [Route("api/exam")]
    [ApiController]
    [Authorize]
    public class ExamController : SonaNovaControllerBase
    { 
        


            private readonly IExamService _ExamService;
            /// <summary>
            /// Initializes a new instance of the <see cref="ExamController"/> class.
            /// </summary>
            /// <param name="logger">The logger instance used for logging.</param>
            /// <param name="ExamService">The ExamDto service instance used for CRUD operations on ExamDto.</param>
            public ExamController(ILogger<ExamController> logger, IExamService ExamService) : base(logger)
            {
                _ExamService = ExamService;
            }

            /// <summary>
            /// Retrieves all ExamDto.
            /// </summary>
            /// <returns>
            /// The response with a collection of ExamDto DTOs if successful, or a problem 
            /// details object indicating the error if the operation fails.
            /// </returns>
            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<ExamDto>))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> GetAllExam()
            {
                _logger.LogInformation("{MethodName} method is called", nameof(GetAllExam));
                try
                {
                    var result = await _ExamService.GetExam(null);
                    _logger.LogDebug(result.ToString());
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }


            /// <summary>
            /// Retrieves a ExamDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the ExamDto.</param>
            /// <returns>
            /// The response with the ExamDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("{id}")]
            [ProducesResponseType(200, Type = typeof(ExamDto))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

            public async Task<IActionResult> GetExamById(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetExamById), id);
                if (id < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                try
                {
                    var ExamDto = await _ExamService.GetExam(id);
                    _logger.LogDebug(ExamDto.ToString());
                    return ExamDto.Count() == 1 ? Ok(ExamDto) : StatusCode(StatusCodes.Status404NotFound);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            /// <summary>
            /// Inserts a new ExamDto.
            /// </summary>
            /// <param name="ExamDto">The DTO representing the ExamDto to insert.</param>
            /// <returns>
            /// The response with the created ExamDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpPost]
            [ProducesResponseType(201)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> InsertExam([FromBody] ExamDto ExamDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(InsertExam));
                try
                {
                    // Insert the ExamDto and retrieve the data
                    var ExamDetail = await _ExamService.InsertExamDetails(ExamDto);
                    _logger.LogDebug(ExamDto.ToString());

                    return CreatedAtAction(nameof(GetAllExam), new { id = ExamDto.Id }, ExamDetail);
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
            /// Updates an existing ExamDto.
            /// </summary>
            /// <param name="ExamDto">The DTO representing the updated ExamDto.</param>
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
            public async Task<IActionResult> UpdateExam([FromBody] ExamDto ExamDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(UpdateExam));
                var ExamDetails = await _ExamService.GetExam((int?)ExamDto.Id);
                if (ExamDetails == null)
                {
                    return NotFound();
                }

                try
                {
                    await _ExamService.UpdateExamDetails(ExamDto);
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
            /// Deletes a ExamDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the ExamDto to delete.</param>
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
            public async Task<IActionResult> DeleteExam(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteExam), id);
                var ExamDto = await _ExamService.GetExam(id);
                if (ExamDto == null)
                {
                    return NotFound();
                }

                try
                {
                var result = await _ExamService.DeleteExamDetails(id);
                return Ok(new { message = result });
                
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
