using RestaurantManagement.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Services;
using System.IO.Compression;

namespace RestaurantManagement.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on ActivityDto.
    /// </summary>
    [Route("api/activity")]
    [ApiController]
    public class ActivityController : SonaNovaControllerBase
    {


        private readonly IActivityService _ActivityService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="ActivityService">The ActivityDto service instance used for CRUD operations on ActivityDto.</param>
        public ActivityController(ILogger<ActivityController> logger, IActivityService ActivityService) : base(logger)
        {
            _ActivityService = ActivityService;
        }



        /// <summary>
        /// Retrieves all activity records based on type and optionally by department.
        /// </summary>
        /// <param name="Type">The activity type identifier.</param>
        /// <param name="DepartmentId">Optional department ID to filter the activities.</param>
        /// <returns>
        /// The response with a collection of ActivityDto if successful,
        /// or a problem details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ActivityDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllActivities([FromQuery] int Type, [FromQuery] long? DepartmentId)
        {
            _logger.LogInformation("{MethodName} method is called with Type: {Type}, DepartmentId: {DepartmentId}", nameof(GetAllActivities), Type, DepartmentId);

            try
            {
                var result = await _ActivityService.GetAllActivityData(Type, DepartmentId);

                if (result == null)
                {
                    return NoContent();
                }

                foreach (var Student in result)
                {
                    if (!string.IsNullOrWhiteSpace(Student.FileNames))
                    {
                        var filesList = Student.FileNames.Split('|').ToList();


                        if (filesList.Count > 0 && string.IsNullOrWhiteSpace(filesList.Last()))
                        {
                            filesList.RemoveAt(filesList.Count - 1);
                        }

                        Student.Files = filesList;
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


        // <summary>
        /// Retrieves a single activity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the activity.</param>
        /// <returns>
        /// The response with the ActivityDto if found, or a problem details object if not found or on error.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ActivityDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetActivityById(int? id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetActivityById), id);

            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            try
            {
                var result = await _ActivityService.GetActivityData(id);

                if (result == null)
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Inserts a new ActivityDto.
        /// </summary>
        /// <param name="ActivityDto">The DTO representing the ActivityDto to insert.</param>
        /// <returns>
        /// The response with the created ActivityDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> InsertActivityData([FromBody] ActivityDto ActivityDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertActivityData));
            try
            {
                // Insert the ActivityDto and retrieve the data
                var ActivityDetail = await _ActivityService.InsertActivityData(ActivityDto);


                return CreatedAtAction(nameof(GetAllActivities), new { id = ActivityDto.Id }, ActivityDetail);

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
        /// Updates an existing ActivityDto.
        /// </summary>
        /// <param name="ActivityDto">The DTO representing the updated ActivityDto.</param>
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
        public async Task<IActionResult> UpdateActivityData([FromBody] ActivityDto ActivityDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(UpdateActivityData));
            var ActivityDetails = await _ActivityService.GetActivityData((int?)ActivityDto.Id);
            if (ActivityDetails == null)
            {
                return NotFound();
            }

            try
            {
                await _ActivityService.UpdateActivityData(ActivityDto);
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
        /// Deletes a ActivityDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ActivityDto to delete.</param>
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
        public async Task<IActionResult> DeleteActivityData(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteActivityData), id);
            var ActivityDto = await _ActivityService.GetActivityData(id);
            if (ActivityDto == null)
            {
                return NotFound();
            }

            try
            {
                await _ActivityService.DeleteActivityData(id);
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
        [HttpGet("downlaodactivityfiles")]
        public async Task<IActionResult> DownloadFiles(int id)
        {
            var result1 = await _ActivityService.GetActivityData(id);
            var firstItem = result1.FirstOrDefault();

            if (firstItem == null)
            {
                return NotFound("No activity data found.");
            }

            var zipName = $"archive-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}.zip";
            var filePath = firstItem.FilePath;

            if (!Directory.Exists(filePath))
            {
                return NotFound("File directory not found.");
            }

            var files = Directory.GetFiles(filePath).ToList();

            MemoryStream compressedFileStream = new MemoryStream();

            using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, true))
            {

                files.ForEach(file =>
                {
                    //Create a zip entry for each attachment
                    var zipEntry = zipArchive.CreateEntry(Path.GetFileName(file));
                    byte[] bytes = System.IO.File.ReadAllBytes(file);
                    //Get the stream of the attachment
                    using (var originalFileStream = new MemoryStream(bytes))
                    using (var zipEntryStream = zipEntry.Open())
                    {
                        //Copy the attachment stream to the zip entry stream
                        originalFileStream.CopyTo(zipEntryStream);
                    }
                });


            }
            const string contentType = "application/zip";
            HttpContext.Response.ContentType = contentType;
            var result = new FileContentResult(compressedFileStream.ToArray(), contentType)
            {
                FileDownloadName = $"{zipName}.zip"
            };
            return result;

        }
    }
}
