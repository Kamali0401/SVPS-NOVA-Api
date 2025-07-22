using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Api.Controllers;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using System.Data.SqlClient;
using System.Net;

namespace SonaNova.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on LeaveDto.
    /// </summary>
    [Route("api/leaveApproval")]
    [ApiController]
    [Authorize]
    public class LeaveController : SonaNovaControllerBase
    {
        


            private readonly ILeaveService _LeaveService;
            /// <summary>
            /// Initializes a new instance of the <see cref="LeaveController"/> class.
            /// </summary>
            /// <param name="logger">The logger instance used for logging.</param>
            /// <param name="LeaveService">The LeaveDto service instance used for CRUD operations on LeaveDto.</param>
            public LeaveController(ILogger<LeaveController> logger, ILeaveService LeaveService) : base(logger)
            {
                _LeaveService = LeaveService;
            }

            /// <summary>
            /// Retrieves all LeaveDto.
            /// </summary>
            /// <returns>
            /// The response with a collection of LeaveDto DTOs if successful, or a problem 
            /// details object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("Byrole/{role}")]
            [ProducesResponseType(200, Type = typeof(IEnumerable<LeaveDto>))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> GetAllLeave(string role, int? id)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(GetAllLeave));
            try
            {
                var result = await _LeaveService.GetLeave(role, id);
                if (result == null)
                {
                    return NoContent();
                }

                foreach (var student in result)
                {
                    if (!string.IsNullOrWhiteSpace(student.Filename))
                    {
                        var fileList = student.Filename.Split('|').ToList();

                        if (fileList.Count > 0 && string.IsNullOrWhiteSpace(fileList.Last()))
                        {
                            fileList.RemoveAt(fileList.Count - 1);
                        }

                        student.FileList = fileList;
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
            /// Retrieves a LeaveDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the LeaveDto.</param>
            /// <returns>
            /// The response with the LeaveDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("{id}")]
            [ProducesResponseType(200, Type = typeof(LeaveDto))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

            public async Task<IActionResult> GetLeaveById(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetLeaveById), id);
                if (id < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                try
                {
                    var LeaveDto = await _LeaveService.GetLeaveById(id);
                    _logger.LogDebug(LeaveDto.ToString());
                    return LeaveDto.Count() == 1 ? Ok(LeaveDto) : StatusCode(StatusCodes.Status404NotFound);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            /// <summary>
            /// Inserts a new LeaveDto.
            /// </summary>
            /// <param name="LeaveDto">The DTO representing the LeaveDto to insert.</param>
            /// <returns>
            /// The response with the created LeaveDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpPost]
            [ProducesResponseType(201)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> InsertLeave([FromBody] LeaveDto LeaveDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(InsertLeave));
                try
                {
                    // Insert the LeaveDto and retrieve the data
                    var LeaveDetail = await _LeaveService.InsertLeaveDetails(LeaveDto);
                    _logger.LogDebug(LeaveDto.ToString());

                    return CreatedAtAction(nameof(GetLeaveById), new { id = LeaveDto.Id }, LeaveDetail);
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
            /// Updates an existing LeaveDto.
            /// </summary>
            /// <param name="LeaveDto">The DTO representing the updated LeaveDto.</param>
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
            public async Task<IActionResult> UpdateLeave([FromBody] LeaveDto LeaveDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(UpdateLeave));
                var LeaveDetails = await _LeaveService.GetLeaveById((int?)LeaveDto.Id);
                if (LeaveDetails == null)
                {
                    return NotFound();
                }

                try
                {
                    await _LeaveService.UpdateLeaveDetails(LeaveDto);
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
            /// Deletes a LeaveDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the LeaveDto to delete.</param>
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
            public async Task<IActionResult> DeleteLeave(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteLeave), id);
                var LeaveDto = await _LeaveService.GetLeaveById(id);
                if (LeaveDto == null)
                {
                    return NotFound();
                }

                try
                {
                    await _LeaveService.DeleteLeaveDetails(id);
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
