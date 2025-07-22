using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Api.Controllers;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using System.Data.SqlClient;
using System.Net;

namespace SonaNova.Api.Controllers
{/// <summary>
 /// Controller for handling CRUD operations on NotificationDto.
 /// </summary>
    [Route("api/notification")]
    [ApiController]
    [Authorize]
    public class NotificationController : SonaNovaControllerBase
    {
       

            private readonly INotificationService _NotificationService;
            /// <summary>
            /// Initializes a new instance of the <see cref="NotificationController"/> class.
            /// </summary>
            /// <param name="logger">The logger instance used for logging.</param>
            /// <param name="NotificationService">The NotificationDto service instance used for CRUD operations on NotificationDto.</param>
            public NotificationController(ILogger<NotificationController> logger, INotificationService NotificationService) : base(logger)
            {
                _NotificationService = NotificationService;
            }

            /// <summary>
            /// Retrieves all NotificationDto.
            /// </summary>
            /// <returns>
            /// The response with a collection of NotificationDto DTOs if successful, or a problem 
            /// details object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("{studentId}/{role}")]
            [ProducesResponseType(200, Type = typeof(IEnumerable<NotificationDto>))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> GetAllNotification(int studentId, string role)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(GetAllNotification),studentId, role);
                try
                {
                    var result = await _NotificationService.GetAllNotification(studentId, role);


                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }


            /// <summary>
            /// Retrieves a NotificationDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the NotificationDto.</param>
            /// <returns>
            /// The response with the NotificationDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("Id/{id}")]
            [ProducesResponseType(200, Type = typeof(NotificationDto))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

            public async Task<IActionResult> GetNotificationById(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetNotificationById), id);
                if (id < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                try
                {
                    var NotificationDto = await _NotificationService.GetNotificationById(id);
                    _logger.LogDebug(NotificationDto.ToString());
                    return NotificationDto.Count() == 1 ? Ok(NotificationDto) : StatusCode(StatusCodes.Status404NotFound);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            /// <summary>
            /// Inserts a new NotificationDto.
            /// </summary>
            /// <param name="NotificationDto">The DTO representing the NotificationDto to insert.</param>
            /// <returns>
            /// The response with the created NotificationDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
           /* [HttpPost]
            [ProducesResponseType(201)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> InsertNotification([FromBody] NotificationDto NotificationDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(InsertNotification));
                try
                {
                    // Insert the NotificationDto and retrieve the data
                    var NotificationDetail = await _NotificationService.InsertNotificationDetails(NotificationDto);
                    _logger.LogDebug(NotificationDto.ToString());

                    return CreatedAtAction(nameof(GetAllNotification), new { id = NotificationDto.Id }, NotificationDetail);
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
            /// Updates an existing NotificationDto.
            /// </summary>
            /// <param name="NotificationDto">The DTO representing the updated NotificationDto.</param>
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
            public async Task<IActionResult> UpdateNotification([FromBody] NotificationDto NotificationDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(UpdateNotification));
                var NotificationDetails = await _NotificationService.GetNotificationById((int?)NotificationDto.Id);
                if (NotificationDetails == null)
                {
                    return NotFound();
                }

                try
                {
                    await _NotificationService.UpdateNotificationDetails(NotificationDto);
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
            /// Deletes a NotificationDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the NotificationDto to delete.</param>
            /// <returns>
            /// The response with no content if the deletion is successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
          /*  [HttpDelete("{id}")]
            [ProducesResponseType(204)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> DeleteNotification(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteNotification), id);
                var NotificationDto = await _NotificationService.GetNotification(id);
                if (NotificationDto == null)
                {
                    return NotFound();
                }

                try
                {
                    await _NotificationService.DeleteNotificationDetails(id);
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


        
    }
}
