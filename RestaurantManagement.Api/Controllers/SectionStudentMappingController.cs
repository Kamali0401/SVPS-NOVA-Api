using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantManagement.Api.Controllers;
using RestaurantManagement.Application.Dtos;
using SonaNova.Application.Dtos;
using SonaNova.Application.Interfaces;
using System.Data.SqlClient;
using System.Net;

namespace SonaNova.Api.Controllers
{ /// <summary>
  /// Controller for handling CRUD operations on SectionStudentMappingDto.
  /// </summary>
    [Route("api/sectionStudentMapping")]
    [ApiController]
    public class SectionStudentMappingController : SonaNovaControllerBase
    {
       
       

            private readonly ISectionStudentMappingService _SectionStudentMappingService;
            /// <summary>
            /// Initializes a new instance of the <see cref="SectionStudentMappingController"/> class.
            /// </summary>
            /// <param name="logger">The logger instance used for logging.</param>
            /// <param name="SectionStudentMappingService">The SectionStudentMappingDto service instance used for CRUD operations on SectionStudentMappingDto.</param>
            public SectionStudentMappingController(ILogger<SectionStudentMappingController> logger, ISectionStudentMappingService SectionStudentMappingService) : base(logger)
            {
                _SectionStudentMappingService = SectionStudentMappingService;
            }

        /// <summary>
        /// Retrieves all SectionStudentMappingDto.
        /// </summary>
        /// <returns>
        /// The response with a collection of SectionStudentMappingDto DTOs if successful, or a problem 
        /// details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SectionStudentMappingDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllSectionStudentMapping()
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetAllSectionStudentMapping));
            try
            {
                var result = await _SectionStudentMappingService.GetSectionStudentMapping(null);
                _logger.LogDebug(result.ToString());
                var groupByData = result.GroupBy(x => x.SectionId);
                var jsonData = JsonConvert.SerializeObject(groupByData);
                _logger.LogDebug(result.ToString());
                if (result == null)
                {
                    return NoContent();
                }
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Retrieves a SectionStudentMappingDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the SectionStudentMappingDto.</param>
        /// <returns>
        /// The response with the SectionStudentMappingDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(SectionStudentMappingDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetSectionStudentMappingById(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetSectionStudentMappingById), id);
            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var SectionStudentMappingDto = await _SectionStudentMappingService.GetSectionStudentMapping(id);
                _logger.LogDebug(SectionStudentMappingDto.ToString());
                return SectionStudentMappingDto.Count() == 1 ? Ok(SectionStudentMappingDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Inserts a new SectionStudentMappingDto.
        /// </summary>
        /// <param name="SectionStudentMappingDto">The DTO representing the SectionStudentMappingDto to insert.</param>
        /// <returns>
        /// The response with the created SectionStudentMappingDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
            [ProducesResponseType(201)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
            [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
            public async Task<IActionResult> InsertSectionStudentMapping([FromBody] List<SectionStudentMappingDto> SectionStudentMappingDto)
            {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertSectionStudentMapping));
            try
            {
               // var list = new List<SectionStudentMappingDto> { SectionStudentMappingDto }; // Wrap in list

                var result = await _SectionStudentMappingService.InsertSectionStudentMappingDetails(SectionStudentMappingDto);

                _logger.LogDebug(SectionStudentMappingDto.ToString());

                // return CreatedAtAction(nameof(GetAllSectionStudentMapping), new { id = SectionStudentMappingDto.Id }, result);
                return CreatedAtAction(nameof(GetAllSectionStudentMapping), null, result);
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
        /// Updates an existing SectionStudentMappingDto.
        /// </summary>
        /// <param name="SectionStudentMappingDto">The DTO representing the updated SectionStudentMappingDto.</param>
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
        public async Task<IActionResult> UpdateSectionStudentMapping([FromBody] List<SectionStudentMappingDto> SectionStudentMappingDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(UpdateSectionStudentMapping));
            /*var SectionStudentMappingDetails = await _SectionStudentMappingService.GetSectionStudentMapping((int?)SectionStudentMappingDto.Id);
            if (SectionStudentMappingDetails == null)
            {
                return NotFound();
            }

            try
            {
            //var list = new List<SectionStudentMappingDto> { SectionStudentMappingDto }; // ✅ wrap into list
            await _SectionStudentMappingService.UpdateSectionStudentMappingDetails(SectionStudentMappingDto);
            return NoContent();
        }*/

            try
            {
                foreach (var dto in SectionStudentMappingDto)
                {
                    var existing = await _SectionStudentMappingService.GetSectionStudentMapping(dto.Id);
                    if (existing == null)
                    {
                        return NotFound(new ProblemDetails
                        {
                            Title = "Not Found",
                            Detail = $"Mapping with ID {dto.Id} not found",
                            Status = (int)HttpStatusCode.NotFound
                        });
                    }
                }

                await _SectionStudentMappingService.UpdateSectionStudentMappingDetails(SectionStudentMappingDto);
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
        /// Deletes a SectionStudentMappingDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the SectionStudentMappingDto to delete.</param>
        /// <returns>
        /// The response with no content if the deletion is successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> DeleteSectionStudentMapping([FromQuery] int[] ids, [FromQuery] int batchId)
        {
            _logger.LogInformation("{MethodName} method is called for ids: {ids} and batchId: {batchId}", nameof(DeleteSectionStudentMapping), ids, batchId);

            try
            {
                var result = await _SectionStudentMappingService.DeleteSectionStudentMappingDetails(ids, batchId);

                if (result == null || result == 0)
                {
                    return NoContent(); // or NotFound(); if you prefer to indicate nothing was deleted
                }

                _logger.LogDebug("Deleted rows count: {count}", result);
                return Ok(new { DeletedCount = result });
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
        /// Retrieves a studentDto by its unique identifier.
        /// </summary>
        /// <param name="studentname">The unique identifier of the studentDto.</param>
        /// <returns>
        /// The response with the studentDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("mappedStudentByName")]
        [ProducesResponseType(200, Type = typeof(StudentDropdownModelDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetMappedStudentByName(string StudentName, int SectionId)
        {
            _logger.LogInformation("{MethodName} method is called for the studentname: {studentname}", nameof(GetMappedStudentByName), StudentName, SectionId);

            try
            {
                var studentDto = await _SectionStudentMappingService.GetMappedStudentByName(StudentName, SectionId);

                _logger.LogDebug("Result from service: {@Result}", studentDto);

                if (studentDto == null)
                {
                    return NoContent(); // or NotFound("Student not found.");
                }

                return Ok(studentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
