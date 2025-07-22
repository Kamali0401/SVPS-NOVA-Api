using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Api.Controllers;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using System.Data.SqlClient;
using System.Net;

namespace SonaNova.Api.Controllers
{/// <summary>
 /// Controller for handling CRUD operations on StudentFeedbackDto.
 /// </summary>
    [Route("api/studentFeedback")]
    [ApiController]
    [Authorize]
    public class StudentFeedbackController : SonaNovaControllerBase
    {
       

            private readonly IStudentFeedbackService _StudentFeedbackService;
            /// <summary>
            /// Initializes a new instance of the <see cref="StudentFeedbackController"/> class.
            /// </summary>
            /// <param name="logger">The logger instance used for logging.</param>
            /// <param name="StudentFeedbackService">The StudentFeedbackDto service instance used for CRUD operations on StudentFeedbackDto.</param>
            public StudentFeedbackController(ILogger<StudentFeedbackController> logger, IStudentFeedbackService StudentFeedbackService) : base(logger)
            {
                _StudentFeedbackService = StudentFeedbackService;
            }

            /// <summary>
            /// Retrieves all StudentFeedbackDto.
            /// </summary>
            /// <returns>
            /// The response with a collection of StudentFeedbackDto DTOs if successful, or a problem 
            /// details object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("{role}")]
            [ProducesResponseType(200, Type = typeof(IEnumerable<StudentFeedbackDto>))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> GetAllStudentFeedback(string role, int ?id)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(GetAllStudentFeedback), role);
                try
                {
                    var result = await _StudentFeedbackService.GetAllStudentFeedback(role,id);

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
            /// Retrieves a StudentFeedbackDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the StudentFeedbackDto.</param>
            /// <returns>
            /// The response with the StudentFeedbackDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("Id/{id}")]
            [ProducesResponseType(200, Type = typeof(StudentFeedbackDto))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

            public async Task<IActionResult> GetStudentFeedbackById(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetStudentFeedbackById), id);
                if (id < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                try
                {
                    var StudentFeedbackDto = await _StudentFeedbackService.GetStudentFeedback(id);
                    _logger.LogDebug(StudentFeedbackDto.ToString());
                    return StudentFeedbackDto.Count() == 1 ? Ok(StudentFeedbackDto) : StatusCode(StatusCodes.Status404NotFound);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            /// <summary>
            /// Inserts a new StudentFeedbackDto.
            /// </summary>
            /// <param name="StudentFeedbackDto">The DTO representing the StudentFeedbackDto to insert.</param>
            /// <returns>
            /// The response with the created StudentFeedbackDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpPost]
            [ProducesResponseType(201, Type = typeof(List<StudentFeedbackDto>))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> InsertStudentFeedback([FromBody] List<StudentFeedbackDto> StudentFeedbackDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(InsertStudentFeedback));
                try
                {
                    // Insert the StudentFeedbackDto and retrieve the data
                    var StudentFeedbackDetail = await _StudentFeedbackService.InsertStudentFeedbackDetails(StudentFeedbackDto);
                    _logger.LogDebug(StudentFeedbackDto.ToString());

                //return CreatedAtAction(nameof(GetAllStudentFeedback), new { id = StudentFeedbackDto.Id }, StudentFeedbackDetail);
                return Ok(StudentFeedbackDetail);
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
            /// Updates an existing StudentFeedbackDto.
            /// </summary>
            /// <param name="StudentFeedbackDto">The DTO representing the updated StudentFeedbackDto.</param>
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
            public async Task<IActionResult> UpdateStudentFeedback([FromBody] StudentFeedbackDto StudentFeedbackDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(UpdateStudentFeedback));
                var StudentFeedbackDetails = await _StudentFeedbackService.GetStudentFeedback((int?)StudentFeedbackDto.Id);
                if (StudentFeedbackDetails == null)
                {
                    return NotFound();
                }

                try
                {
                    await _StudentFeedbackService.UpdateStudentFeedbackDetails(StudentFeedbackDto);
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
            /// Deletes a StudentFeedbackDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the StudentFeedbackDto to delete.</param>
            /// <returns>
            /// The response with no content if the deletion is successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpDelete("{id}")]
            [ProducesResponseType(204, Type = typeof(StudentFeedbackDto))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> DeleteStudentFeedback(string id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteStudentFeedback), id);
            //var StudentFeedbackDto = await _StudentFeedbackService.GetStudentFeedback(id);
            //if (StudentFeedbackDto == null)
            //{
            //    return NotFound();
            //}
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            try
                {
                    await _StudentFeedbackService.DeleteStudentFeedbackDetails(id);
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
