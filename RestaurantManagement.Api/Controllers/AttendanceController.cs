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
{/// <summary>
 /// Controller for handling CRUD operations on AttendanceDto.
 /// </summary>
    [Route("api/attendance")]
    [ApiController]
    [Authorize]
    public class AttendanceController : SonaNovaControllerBase
    {
       

            private readonly IAttendanceService _AttendanceCostService;
            /// <summary>
            /// Initializes a new instance of the <see cref="AttendanceController"/> class.
            /// </summary>
            /// <param name="logger">The logger instance used for logging.</param>
            /// <param name="AttendanceService">The AttendanceDto service instance used for CRUD operations on AttendanceDto.</param>
            public AttendanceController(ILogger<AttendanceController> logger, IAttendanceService AttendanceCostService) : base(logger)
            {
                _AttendanceCostService = AttendanceCostService;
            }

            /// <summary>
            /// Retrieves all AttendanceDto.
            /// </summary>
            /// <returns>
            /// The response with a collection of AttendanceDto DTOs if successful, or a problem 
            /// details object indicating the error if the operation fails.
            /// </returns>
            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<AttendanceDto>))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> GetAllAttendance(DateTime? AttendanceDate, int sectionId, string Hoursday)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(GetAllAttendance), AttendanceDate, sectionId, Hoursday);
                try
                {
                    var result = await _AttendanceCostService.GetAttendanceDetails(AttendanceDate, sectionId, Hoursday);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }


            /// <summary>
            /// Retrieves a AttendanceDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the AttendanceDto.</param>
            /// <returns>
            /// The response with the AttendanceDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("{id}")]
            [ProducesResponseType(200, Type = typeof(AttendanceDto))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

            public async Task<IActionResult> GetAttendanceById(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetAttendanceById), id);
                if (id < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                try
                {
                    var AttendanceDto = await _AttendanceCostService.GetAttendanceById(id);
                    return AttendanceDto.Count() == 1 ? Ok(AttendanceDto) : StatusCode(StatusCodes.Status404NotFound);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
        /// <summary>
        /// Retrieves a AttendanceDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AttendanceDto.</param>
        /// <returns>
        /// The response with the AttendanceDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("student/{studentId}")]
        [ProducesResponseType(200, Type = typeof(StudentAttendanceModelDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetAttendanceByStudentId(int studentId, int month, int year)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetAttendanceByStudentId), studentId, month, year);
            if (studentId < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var AttendanceDto = await _AttendanceCostService.GetAttendanceByStudentId(studentId, month, year);
                return AttendanceDto.Count() == 1 ? Ok(AttendanceDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Inserts a new AttendanceDto.
        /// </summary>
        /// <param name="AttendanceDto">The DTO representing the AttendanceDto to insert.</param>
        /// <returns>
        /// The response with the created AttendanceDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
            [ProducesResponseType(201)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> InsertAttendance([FromBody] List<AttendanceDto> AttendanceDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(InsertAttendance));
                try
                {
                    // Insert the AttendanceDto and retrieve the data
                    var InventoryCostDetail = await _AttendanceCostService.InsertAttendanceDetails(AttendanceDto);


                // return CreatedAtAction(nameof(GetAllAttendance), new { id = AttendanceDto.Id }, InventoryCostDetail);
                return Created(nameof(GetAllAttendance), InventoryCostDetail);
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
            /// Updates an existing AttendanceDto.
            /// </summary>
            /// <param name="AttendanceDto">The DTO representing the updated AttendanceDto.</param>
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
            public async Task<IActionResult> UpdateAttendance([FromBody] AttendanceDto AttendanceDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(UpdateAttendance));
                var AttendanceDetails = await _AttendanceCostService.GetAttendanceById((int?)AttendanceDto.Id);
                if (AttendanceDetails == null)
                {
                    return NotFound();
                }

                try
                {
                    await _AttendanceCostService.UpdateAttendanceDetails(AttendanceDto);
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
        /// Deletes a AttendanceDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AttendanceDto to delete.</param>
        /// <returns>
        /// The response with no content if the deletion is successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpDelete()]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> DeleteAttendance([FromBody] AttendanceDto attendanceDto)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteAttendance), attendanceDto.Id);

            if (attendanceDto?.Id == null || attendanceDto.Id == 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Invalid ID",
                    Detail = "Attendance ID must be a valid non-zero value.",
                    Status = (int)HttpStatusCode.BadRequest
                });
            }

            var attendanceDetails = await _AttendanceCostService.GetAttendanceById(attendanceDto.Id);
            if (attendanceDetails == null)
            {
                return NotFound();
            }

            try
            {
                await _AttendanceCostService.DeleteAttendanceDetails(new List<AttendanceDto> { attendanceDto });
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
