using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Api.Controllers;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using System.Data.SqlClient;
using System.Net;

namespace SonaNova.Api.Controllers
{ /// <summary>
  /// Controller for handling CRUD operations on SectionSubjectMappingDto.
  /// </summary>
    [Route("api/sectionSubjectMapping")]
    [ApiController]
    public class SectionSubjectMappingController : SonaNovaControllerBase
    {
       
       
            private readonly ISectionSubjectMappingService _SectionSubjectMappingService;
            /// <summary>
            /// Initializes a new instance of the <see cref="SectionSubjectMappingController"/> class.
            /// </summary>
            /// <param name="logger">The logger instance used for logging.</param>
            /// <param name="SectionSubjectMappingService">The SectionSubjectMappingDto service instance used for CRUD operations on SectionSubjectMappingDto.</param>
            public SectionSubjectMappingController(ILogger<SectionSubjectMappingController> logger, ISectionSubjectMappingService SectionSubjectMappingService) : base(logger)
            {
                _SectionSubjectMappingService = SectionSubjectMappingService;
            }

            /// <summary>
            /// Retrieves all SectionSubjectMappingDto.
            /// </summary>
            /// <returns>
            /// The response with a collection of SectionSubjectMappingDto DTOs if successful, or a problem 
            /// details object indicating the error if the operation fails.
            /// </returns>
            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<SectionSubjectMappingDto>))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> GetAllSectionSubjectMapping()
            {
                _logger.LogInformation("{MethodName} method is called", nameof(GetAllSectionSubjectMapping));
                try
                {
                    var result = await _SectionSubjectMappingService.GetSectionSubjectMapping(null);
                    _logger.LogDebug(result.ToString());
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }


            /// <summary>
            /// Retrieves a SectionSubjectMappingDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the SectionSubjectMappingDto.</param>
            /// <returns>
            /// The response with the SectionSubjectMappingDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpGet("{id}")]
            [ProducesResponseType(200, Type = typeof(SectionSubjectMappingDto))]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

            public async Task<IActionResult> GetSectionSubjectMappingById(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetSectionSubjectMappingById), id);
                if (id < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                try
                {
                    var SectionSubjectMappingDto = await _SectionSubjectMappingService.GetSectionSubjectMapping(id);
                    _logger.LogDebug(SectionSubjectMappingDto.ToString());
                    return SectionSubjectMappingDto.Count() == 1 ? Ok(SectionSubjectMappingDto) : StatusCode(StatusCodes.Status404NotFound);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            /// <summary>
            /// Inserts a new SectionSubjectMappingDto.
            /// </summary>
            /// <param name="SectionSubjectMappingDto">The DTO representing the SectionSubjectMappingDto to insert.</param>
            /// <returns>
            /// The response with the created SectionSubjectMappingDto DTO if successful, or a problem details 
            /// object indicating the error if the operation fails.
            /// </returns>
            [HttpPost]
            [ProducesResponseType(201)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> InsertSectionSubjectMapping([FromBody] SectionSubjectMappingDto SectionSubjectMappingDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(InsertSectionSubjectMapping));
                try
                {
                    // Insert the SectionSubjectMappingDto and retrieve the data
                    var SectionSubjectMappingDetail = await _SectionSubjectMappingService.InsertSectionSubjectMappingDetails(SectionSubjectMappingDto);
                    _logger.LogDebug(SectionSubjectMappingDto.ToString());

                    return CreatedAtAction(nameof(GetAllSectionSubjectMapping), new { id = SectionSubjectMappingDto.Id }, SectionSubjectMappingDetail);
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
            /// Updates an existing SectionSubjectMappingDto.
            /// </summary>
            /// <param name="SectionSubjectMappingDto">The DTO representing the updated SectionSubjectMappingDto.</param>
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
            public async Task<IActionResult> UpdateSectionSubjectMapping([FromBody] SectionSubjectMappingDto SectionSubjectMappingDto)
            {
                _logger.LogInformation("{MethodName} method is called", nameof(UpdateSectionSubjectMapping));
                var SectionSubjectMappingDetails = await _SectionSubjectMappingService.GetSectionSubjectMapping((int?)SectionSubjectMappingDto.Id);
                if (SectionSubjectMappingDetails == null)
                {
                    return NotFound();
                }

                try
                {
                    await _SectionSubjectMappingService.UpdateSectionSubjectMappingDetails(SectionSubjectMappingDto);
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
            /// Deletes a SectionSubjectMappingDto by its unique identifier.
            /// </summary>
            /// <param name="id">The unique identifier of the SectionSubjectMappingDto to delete.</param>
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
            public async Task<IActionResult> DeleteSectionSubjectMapping(int id)
            {
                _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteSectionSubjectMapping), id);
                var SectionSubjectMappingDto = await _SectionSubjectMappingService.GetSectionSubjectMapping(id);
                if (SectionSubjectMappingDto == null)
                {
                    return NotFound();
                }

                try
                {
                    await _SectionSubjectMappingService.DeleteSectionSubjectMappingDetails(id);
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

        [HttpGet("FacultyListBySection/{sectionId}")]
        [ProducesResponseType(200, Type = typeof(SectionSubjectMappingDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetFacultyListBySectionIdDetails(int sectionId)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {sectionId}", nameof(GetFacultyListBySectionIdDetails), sectionId);
            if (sectionId < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var SectionSubjectMappingDto = await _SectionSubjectMappingService.GetFacultyListBySectionIdDetails(sectionId);
                _logger.LogDebug(SectionSubjectMappingDto.ToString());
                return SectionSubjectMappingDto.Count() == 1 ? Ok(SectionSubjectMappingDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
