using RestaurantManagement.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using RestaurantManagement.Application.Interfaces;
using SonaNova.Application.Dtos;
using RestaurantManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantManagement.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on houseactivityDto.
    /// </summary>
    [Route("api/houseActivity")]
    [ApiController]
    [Authorize]
    public class HouseActivityController : SonaNovaControllerBase
    {


        private readonly IHouseActivityService _houseactivityService;
        /// <summary>
        /// Initializes a new instance of the <see cref="houseactivityController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="houseactivityService">The houseActivityDto service instance used for CRUD operations on houseActivityDto.</param>
        public HouseActivityController(ILogger<HouseActivityController> logger, IHouseActivityService houseActivity) : base(logger)
        {
            _houseactivityService = houseActivity;
        }

        /// <summary>
        /// Retrieves all houseactivityDto.
        /// </summary>
        /// <returns>
        /// The response with a collection of houseactivityDto DTOs if successful, or a problem 
        /// details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HouseActivityDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllHouseActivity()
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetAllHouseActivity));
            try
            {
                var result = await _houseactivityService.GetHouseActivity(null);
                _logger.LogDebug(result.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Retrieves a houseactivityDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the houseactivityDto.</param>
        /// <returns>
        /// The response with the houseactivityDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(HouseActivityDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetHouseActivityById(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetHouseActivityById), id);
            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var houseActivityDto = await _houseactivityService.GetHouseActivity(id);
                _logger.LogDebug(houseActivityDto.ToString());
                return houseActivityDto.Count() == 1 ? Ok(houseActivityDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Inserts a new houseActivityDto.
        /// </summary>
        /// <param name="houseActivityDto">The DTO representing the houseActivityDto to insert.</param>
        /// <returns>
        /// The response with the created houseActivityDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> InsertHouseActivity([FromBody] HouseActivityDto houseActivityDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertHouseActivity));
            try
            {
                // Insert the houseActivityDto and retrieve the data
                var HouseActivityDetail = await _houseactivityService.InsertHouseActivity(houseActivityDto);

                _logger.LogDebug(HouseActivityDetail.ToString());
                return CreatedAtAction(nameof(GetAllHouseActivity), new { id = houseActivityDto.Id }, HouseActivityDetail);
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
        /// Updates an existing houseActivityDto.
        /// </summary>
        /// <param name="houseActivityDto">The DTO representing the updated houseActivityDto.</param>
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
        public async Task<IActionResult> UpdateHouseActivity([FromBody] HouseActivityDto houseActivityDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(UpdateHouseActivity));
            var inventoryDetails = await _houseactivityService.GetHouseActivity((int?)houseActivityDto.Id);
            if (inventoryDetails == null)
            {
                return NotFound();
            }

            try
            {
                await _houseactivityService.UpdateHouseActivity(houseActivityDto);
                _logger.LogDebug("HouseActivity with Id: {Id} updated successfully.", houseActivityDto.Id);
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
        /// Deletes a houseActivityDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the houseActivityDto to delete.</param>
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
        public async Task<IActionResult> DeleteHouseActivity(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteHouseActivity), id);
            var houseActivityDto = await _houseactivityService.GetHouseActivity(id);
            if (houseActivityDto == null)
            {
                return NotFound();
            }

            try
            {
               // await _houseactivityService.DeleteHouseActivityDetails(id);
                
               // return NoContent();

                var result = await _houseactivityService.DeleteHouseActivityDetails(id);
                _logger.LogDebug("HouseActivity with Id: {Id} deleted successfully.", id);
                return Ok(new { message = result }); // Will return { "message": "Success" }
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


        [HttpGet("HousePoints")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HousePointModelDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetHousePoint()
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetHousePoint));
            try
            {
                var result = await _houseactivityService.GetHousePointDetails();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
       
    }
}
