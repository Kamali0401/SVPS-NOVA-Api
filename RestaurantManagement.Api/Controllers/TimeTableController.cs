using RestaurantManagement.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using RestaurantManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantManagement.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on TimeTableDto.
    /// </summary>
    [Route("api/timeTable")]
    [ApiController]
    [Authorize]
    public class TimeTableController : SonaNovaControllerBase
    {


        private readonly ITimeTableService _TimeTableService;
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeTableController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="TimeTableService">The TimeTableDto service instance used for CRUD operations on TimeTableDto.</param>
        public TimeTableController(ILogger<TimeTableController> logger, ITimeTableService TimeTableService) : base(logger)
        {
            _TimeTableService = TimeTableService;
        }

        /// <summary>
        /// Retrieves all TimeTableDto.
        /// </summary>
        /// <returns>
        /// The response with a collection of TimeTableDto DTOs if successful, or a problem 
        /// details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TimeTableDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllTimeTables()
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetAllTimeTables));
            try
            {
                var result = await _TimeTableService.GetTimeTableDetails(null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Retrieves a TimeTableDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the TimeTableDto.</param>
        /// <returns>
        /// The response with the TimeTableDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TimeTableDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetTimeTableById(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetTimeTableById), id);
            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var TimeTableDto = await _TimeTableService.GetTimeTableDetails(id);
                return TimeTableDto.Count() == 1 ? Ok(TimeTableDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Inserts a new TimeTableDto.
        /// </summary>
        /// <param name="TimeTableDto">The DTO representing the TimeTableDto to insert.</param>
        /// <returns>
        /// The response with the created TimeTableDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> InsertTimeTable([FromBody] TimeTableDto TimeTableDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertTimeTable));
            try
            {
                // Insert the TimeTableDto and retrieve the data
                var TimeTableDetail = await _TimeTableService.InsertTimeTableDetails(TimeTableDto);


                return CreatedAtAction(nameof(GetAllTimeTables), new { id = TimeTableDto.Id }, TimeTableDetail);
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
        /// Updates an existing TimeTableDto.
        /// </summary>
        /// <param name="TimeTableDto">The DTO representing the updated TimeTableDto.</param>
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
        public async Task<IActionResult> UpdateTimeTable([FromBody] TimeTableDto TimeTableDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(UpdateTimeTable));
            var TimeTableDetails = await _TimeTableService.GetTimeTableDetails((int?)TimeTableDto.Id);
            if (TimeTableDetails == null)
            {
                return NotFound();
            }

            try
            {
                await _TimeTableService.UpdateTimeTableDetails(TimeTableDto);
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
        /// Deletes a TimeTableDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the TimeTableDto to delete.</param>
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
        public async Task<IActionResult> DeleteTimeTable(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteTimeTable), id);
            var TimeTableDto = await _TimeTableService.GetTimeTableDetails(id);
            if (TimeTableDto == null)
            {
                return NotFound();
            }

            try
            {
                await _TimeTableService.DeleteTimeTableDetails(id);
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
        /// Retrieves a TimeTableDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the TimeTableDto.</param>
        /// <returns>
        /// The response with the TimeTableDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("{sectionId}/{role}")]
        [ProducesResponseType(200, Type = typeof(TimeTableDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetTimeTableBySectionId(int sectionId, string role)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetTimeTableBySectionId), sectionId,role);
            if (sectionId < 1 || string.IsNullOrWhiteSpace(role))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var TimeTableDto = await _TimeTableService.GetTimeTableBySectionId(sectionId,role);
                return Ok(TimeTableDto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
