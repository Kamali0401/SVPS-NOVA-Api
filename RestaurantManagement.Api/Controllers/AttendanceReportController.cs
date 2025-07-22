using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Api.Controllers;
using SonaNova.Application.Interfaces;
using System.Data.SqlClient;
using System.Net;

namespace SonaNova.Api.Controllers
{/// <summary>
 /// Controller for handling CRUD operations on AttendanceDto.
 /// </summary>
  [Route("api/attendancereports")]
    [ApiController]
    [Authorize]
    public class AttendanceReportController : SonaNovaControllerBase
    {
        
       
       


            private readonly IAttendanceReport _AttendanceCostService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AttendanceReportController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="AttendanceService">The AttendanceDto service instance used for CRUD operations on AttendanceDto.</param>
        public AttendanceReportController(ILogger<AttendanceReportController> logger, IAttendanceReport AttendanceCostService) : base(logger)
            {
                _AttendanceCostService = AttendanceCostService;
            }

        [HttpGet("monthwiseReport")]
        [ProducesResponseType(typeof(FileResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> generateAttendanceMonthwisereport(int startMonth, int startYear, int endMonth, int endYear, int sectionId, string grade, string section)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(generateAttendanceMonthwisereport));
            try
            {
                var result = await _AttendanceCostService.generateMonthlyAttendancereport(startMonth, startYear, endMonth, endYear, sectionId, grade, section);
                _logger.LogDebug(result.ToString());

                var fileResult = await PrepareFileForDownload(result.ToString(), "Excel");
                return fileResult;
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
        [HttpGet("ExcelList")]
        [ProducesResponseType(typeof(FileResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> generateExcelList(string role)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(generateExcelList));
            try
            {
                var result = await _AttendanceCostService.generateExcelList(role);
                _logger.LogDebug(result.ToString());

                var fileResult = await PrepareFileForDownload(result.ToString(), "Excel");
                return fileResult;
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
        [HttpGet("DailyAttendanceReport")]
        [ProducesResponseType(typeof(FileResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> generateDailyAttendancereport(int month, int year, int sectionId, string grade, string section)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(generateDailyAttendancereport));
            try
            {
                var result = await _AttendanceCostService.generateDailyAttendancereport(month, year, sectionId, grade, section);
                _logger.LogDebug(result.ToString());

                var fileResult = await PrepareFileForDownload(result.ToString(), "Excel");
                return fileResult;
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

        [HttpGet("CumulativeReport")]
        [ProducesResponseType(typeof(FileResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> generateAttendanceCumulativereport(int startYear, int endYear, int sectionId)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(generateAttendanceCumulativereport));
            try
            {
                var result = await _AttendanceCostService.generateAttendanceCumulativereport(startYear, endYear, sectionId);
                _logger.LogDebug(result.ToString());

                var fileResult = await PrepareFileForDownload(result.ToString(), "Excel");
                return fileResult;
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
        private async Task<FileResult> PrepareFileForDownload(string result, string type)
        {
            try
            {
                var downloadData = await _AttendanceCostService.DownloadData(result);
                if (downloadData.memory == null || string.IsNullOrEmpty(downloadData.path))
                {
                    return null;
                }

                downloadData.memory.Position = 0;
                string contentType = string.Empty;
                if (type == "Word") contentType = "application/vnd.ms-word";
                else if (type == "Excel") contentType = "application/vnd.ms-excel";
                else contentType = "application/pdf";

                // Reading the file bytes and converting to Base64 (if needed for some reason)
                byte[] fileBytes = System.IO.File.ReadAllBytes(result);
                string base64String = Convert.ToBase64String(fileBytes);

                // Returning the file as a downloadable result
                return File(downloadData.memory, contentType, Path.GetFileName(downloadData.path));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error preparing file for download: {ex.Message}");
                throw;
            }
        }

    }


}
