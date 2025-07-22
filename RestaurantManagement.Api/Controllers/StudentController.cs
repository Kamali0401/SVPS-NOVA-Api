using RestaurantManagement.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Application.Services;
using System.IO.Compression;
using SonaNova.Application.Dtos;

namespace RestaurantManagement.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on studentDto.
    /// </summary>
    [Route("api/student")]
    [ApiController]
    public class StudentController : SonaNovaControllerBase
    {


        private readonly IStudentService _studentService;
        /// <summary>
        /// Initializes a new instance of the <see cref="studentController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="studentService">The studentDto service instance used for CRUD operations on studentDto.</param>
        public StudentController(ILogger<StudentController> logger, IStudentService studentService) : base(logger)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Retrieves all studentDto.
        /// </summary>
        /// <returns>
        /// The response with a collection of studentDto DTOs if successful, or a problem 
        /// details object indicating the error if the operation fails.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> GetAllStudentDetails()
        {
            _logger.LogInformation("{MethodName} method is called", nameof(GetAllStudentDetails));
            try
            {
                var result = await _studentService.GetStudentDetails(null);
                _logger.LogDebug(result.ToString());
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


        /// <summary>
        /// Retrieves a studentDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the studentDto.</param>
        /// <returns>
        /// The response with the studentDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(StudentDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetStudentDetailsById(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetStudentDetailsById), id);
            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            /*try
            {
                var studentDto = await _studentService.GetStudentDetails(id);

                if (studentDto == null)
                    return StatusCode(StatusCodes.Status404NotFound, "Student details not found.");

                return studentDto.Id != 0
                    ? Ok(studentDto)
                    : StatusCode(StatusCodes.Status404NotFound, "Student ID not valid.");
            }*/
            
            try
            {
                var subjectDto = await _studentService.GetStudentDetails(id);
                return subjectDto.Count() == 1 ? Ok(subjectDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        /// <summary>
        /// Inserts a new studentDto.
        /// </summary>
        /// <param name="studentDto">The DTO representing the studentDto to insert.</param>
        /// <returns>
        /// The response with the created studentDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> InsertStudent([FromBody] StudentDto studentDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(InsertStudent));
            try
            {
                // Insert the studentDto and retrieve the data
                var student = await _studentService.InsertStudent(studentDto);


                return CreatedAtAction(nameof(GetAllStudentDetails), new { id = studentDto.Id }, student);
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
        /// Updates an existing studentDto.
        /// </summary>
        /// <param name="studentDto">The DTO representing the updated studentDto.</param>
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
        public async Task<IActionResult> UpdateStudent([FromBody] StudentDto studentDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(UpdateStudent));
            var students = await _studentService.GetStudentDetails((int?)studentDto.Id);
            if (students == null)
           {
                return NotFound();
            }

            try
            {
                await _studentService.UpdateStudent(studentDto);
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
        /// Deletes a studentDto by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the studentDto to delete.</param>
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
        public async Task<IActionResult> DeleteStudent(int id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(DeleteStudent), id);
            var studentDto = await _studentService.GetStudentDetails(id);
            if (studentDto == null)
            {
                return NotFound();
            }

            try
            {
                await _studentService.DeleteStudent(id);
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
        /// Retrieves a studentDto by its unique identifier.
        /// </summary>
        /// <param name="studentname">The unique identifier of the studentDto.</param>
        /// <returns>
        /// The response with the studentDto DTO if successful, or a problem details 
        /// object indicating the error if the operation fails.
        /// </returns>
        [HttpGet("name/{studentname}")]
        [ProducesResponseType(200, Type = typeof(StudentDropdownDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetStudentByName(string studentname)
        {
            _logger.LogInformation("{MethodName} method is called for the studentname: {studentname}", nameof(GetStudentByName), studentname);
           
            try
            {
                /*  var studentDto = await _studentService.GetStudentByName(studentname);

                  if (studentDto == null)
                      return StatusCode(StatusCodes.Status404NotFound, "Student details not found.");

                  return studentDto.Id != 0
                      ? Ok(studentDto)
                      : StatusCode(StatusCodes.Status404NotFound, "Student ID not valid.");*/
                var studentList = await _studentService.GetStudentByName(studentname);

                if (studentList == null || !studentList.Any())
                    return StatusCode(StatusCodes.Status404NotFound, "Faculty details not found.");
                return Ok(studentList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpGet("downloadFiles")]
        public async Task<IActionResult> DownloadStudentFiles(int id)
        {
            var result1 = await _studentService.GetStudentDetails(id);
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
        [HttpPut("StudentSemDate")]
        [ProducesResponseType(200, Type = typeof(StudentSemDateModelDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> updateStudentSemDateDetails([FromBody] StudentSemDateModelDto studentDto)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(updateStudentSemDateDetails));

            try
            {
                await _studentService.updateStudentSemDateDetails(studentDto);
                _logger.LogDebug(studentDto.ToString());

                return Ok(studentDto); // or NoContent();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error occurred: {Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
                {
                    Title = "Database Error",
                    Detail = "An error occurred while accessing the database. Please try again later.",
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
        [HttpGet("studentConfig")]
        [ProducesResponseType(200, Type = typeof(StudentSemDateModelDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]

        public async Task<IActionResult> GetAllStudentConfiguration(int? id)
        {
            _logger.LogInformation("{MethodName} method is called for the id: {id}", nameof(GetAllStudentConfiguration), id);
            if (id < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            /*try
            {
                var studentDto = await _studentService.GetStudentDetails(id);

                if (studentDto == null)
                    return StatusCode(StatusCodes.Status404NotFound, "Student details not found.");

                return studentDto.Id != 0
                    ? Ok(studentDto)
                    : StatusCode(StatusCodes.Status404NotFound, "Student ID not valid.");
            }*/

            try
            {
                var subjectDto = await _studentService.GetAllStudentConfiguration(id);
                return subjectDto.Count() == 1 ? Ok(subjectDto) : StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
