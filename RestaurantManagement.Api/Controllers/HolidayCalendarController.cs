using RestaurantManagement.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on _holidayCalendarDto.
    /// </summary>
    [Route("api/holidayCalendar")]
    [ApiController]
    public class HolidayCalendarController : SonaNovaControllerBase
    {


        private readonly IHolidayCalendarService _holidayCalendarService;
        /// <summary>
        /// Initializes a new instance of the <see cref="_holidayCalendarController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="_holidayCalendarService">The _holidayCalendarDto service instance used for CRUD operations on _holidayCalendarDto.</param>
        public HolidayCalendarController(ILogger<HolidayCalendarController> logger, IHolidayCalendarService holidayCalendarService) : base(logger)
        {
            _holidayCalendarService = holidayCalendarService;
        }

        /// <summary>
        /// Retrieves all _holidayCalendarDto.
        /// </summary>
        /// <returns>
        /// The response with a collection of _holidayCalendarDto DTOs if successful, or a problem 
        /// details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HolidayCalendarDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetHolidayCalendar(int? Id = null)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetHolidayCalendar));
            try
            {
                var result = await _holidayCalendarService.GetHolidayCalendarDetails(Id);
                _logger.LogDebug(result.ToString());
                // Check if the result is null or empty
                if (result == null || !result.Any())
                {
                    _logger.LogWarning("No holiday calendar details found.");
                    return NoContent(); // Returns HTTP 204 No Content
                }

                // Process the files for each holiday calendar entry
                foreach (var item in result)
                {
                    if (!string.IsNullOrWhiteSpace(item.FileNames))
                    {
                        item.Files = item.FileNames.Split('|').ToList();

                        // Remove empty entries caused by trailing delimiters
                        item.Files = item.Files.Where(file => !string.IsNullOrWhiteSpace(file)).ToList();
                    }
                }

                _logger.LogInformation("Holiday calendar details retrieved successfully.");
                return Ok(result); // Returns HTTP 200 with the result
            
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Retrieves a HolidayCalendarDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the _holidayCalendarDto.</param>
        /// <returns>
        /// The response with the _holidayCalendarDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(HolidayCalendarDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetHolidayCalendarById(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetHolidayCalendarById), id);
           
            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var HolidayCalendarDto = await _holidayCalendarService.GetHolidayCalendarDetails(id);
                _logger.LogDebug(HolidayCalendarDto.ToString());
                return HolidayCalendarDto.Count() == 1 ? Ok(HolidayCalendarDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Inserts a new _holidayCalendarDto.
        /// </summary>
        /// <param name="_holidayCalendarDto">The DTO representing the _holidayCalendarDto to insert.</param>
        /// <returns>
        /// The response with the created _holidayCalendarDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> InsertHolidayCalendar([FromBody] HolidayCalendarDto HolidayCalendarDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertHolidayCalendar));
            try
            {
                // Insert the HolidayCalendarDto and retrieve the data
                var HolidayCalendarDetail = await _holidayCalendarService.InsertHolidayCalendarDetails(HolidayCalendarDto);

                _logger.LogDebug(HolidayCalendarDto.ToString());
                return CreatedAtAction(nameof(GetHolidayCalendar), new { id = HolidayCalendarDto.Id }, HolidayCalendarDetail);
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
        /// Updates an existing _holidayCalendarDto.
        /// </summary>
        /// <param name="_holidayCalendarDto">The DTO representing the updated _holidayCalendarDto.</param>
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
        public async Task<IActionResult> UpdateHolidayCalendar([FromBody] HolidayCalendarDto HolidayCalendarDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(UpdateHolidayCalendar));
            var HolidayCalendarDetails = await _holidayCalendarService.GetHolidayCalendarDetails((int?)HolidayCalendarDto.Id);
            if (HolidayCalendarDetails == null)
            {
                return NotFound();
            }

            try
            {
                await _holidayCalendarService.UpdateHolidayCalendarDetails(HolidayCalendarDto);
                _logger.LogDebug(HolidayCalendarDto.ToString());

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
        /// Deletes a _holidayCalendarDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the _holidayCalendarDto to delete.</param>
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
        public async Task<IActionResult> DeleteHolidayCalendar(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteHolidayCalendar), id);
            var HolidayCalendarDto = await _holidayCalendarService.GetHolidayCalendarDetails(id);
            if (HolidayCalendarDto == null)
            {
                return NotFound();
            }

            try
            {
                await _holidayCalendarService.DeleteHolidayCalendarDetails(id);
                _logger.LogDebug(HolidayCalendarDto.ToString());
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
