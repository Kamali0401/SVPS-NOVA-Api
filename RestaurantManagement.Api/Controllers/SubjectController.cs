using RestaurantManagement.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Services;

namespace RestaurantManagement.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on subjectDto.
    /// </summary>
    [Route("api/subject")]
    [ApiController]
    public class SubjectController : SonaNovaControllerBase
    {


        private readonly ISubjectService _subjectCostService;
        /// <summary>
        /// Initializes a new instance of the <see cref="subjectController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="subjectService">The subjectDto service instance used for CRUD operations on subjectDto.</param>
        public SubjectController(ILogger<SubjectController> logger, ISubjectService subjectCostService) : base(logger)
        {
            _subjectCostService = subjectCostService;
        }

        /// <summary>
        /// Retrieves all subjectDto.
        /// </summary>
        /// <returns>
        /// The response with a collection of subjectDto DTOs if successful, or a problem 
        /// details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SubjectDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllSubject()
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetAllSubject));
            try
            {
                var result = await _subjectCostService.GetSubjectDetails(null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Retrieves a subjectDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the subjectDto.</param>
        /// <returns>
        /// The response with the subjectDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(SubjectDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetSubjectById(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetSubjectById), id);
            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var subjectDto = await _subjectCostService.GetSubjectDetails(id);
                return subjectDto.Count() == 1 ? Ok(subjectDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Inserts a new subjectDto.
        /// </summary>
        /// <param name="subjectDto">The DTO representing the subjectDto to insert.</param>
        /// <returns>
        /// The response with the created subjectDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> InsertSubject([FromBody] SubjectDto subjectDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertSubject));
            try
            {
                // Insert the subjectDto and retrieve the data
                var InventoryCostDetail = await _subjectCostService.InsertSubjectDetails(subjectDto);


                return CreatedAtAction(nameof(GetAllSubject), new { id = subjectDto.Id }, InventoryCostDetail);
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
        /// Updates an existing subjectDto.
        /// </summary>
        /// <param name="subjectDto">The DTO representing the updated subjectDto.</param>
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
        public async Task<IActionResult> UpdateSubject([FromBody] SubjectDto subjectDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(UpdateSubject));
            var subjectDetails = await _subjectCostService.GetSubjectDetails((int?)subjectDto.Id);
            if (subjectDetails == null)
            {
                return NotFound();
            }

            try
            {
                await _subjectCostService.UpdateSubjectDetails(subjectDto);
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
        /// Deletes a subjectDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the subjectDto to delete.</param>
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
        public async Task<IActionResult> DeleteSubject(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteSubject), id);
            var subjectDto = await _subjectCostService.GetSubjectDetails(id);
            if (subjectDto == null)
            {
                return NotFound();
            }

            try
            {
                // await _subjectCostService.DeleteSubjectDetails(id);
                // return NoContent();
                var result = await _subjectCostService.DeleteSubjectDetails(id);
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
    }
}
