using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestaurantManagement.Api.Controllers;
using RestaurantManagement.Application.Dtos;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Services;
using SonaNova.Application.Interfaces;
using SonaNova.Application.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SonaNova.Api.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on ChangePasswordDto.
    /// </summary>
    [Route("api/changePassword")]
    [ApiController]
    public class ChangePasswordController : SonaNovaControllerBase
    {

        private readonly IChangePasswordService _ChangePasswordService;
        private readonly IStudentService _studentService;

        /// <summary>
        /// Initializes a new instance of the controller.
        /// </summary>
        public ChangePasswordController(
            ILogger<ChangePasswordController> logger,
            IChangePasswordService changePasswordService,
            IStudentService studentService) : base(logger)
        {
            _ChangePasswordService = changePasswordService;
            _studentService = studentService;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(FacultyDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetVerifyPassword(string UserName, string Password)
        {
            // FacultyModel facultyDetails = JsonConvert.DeserializeObject<FacultyModel>(faculty);
            var result = await _ChangePasswordService.GetVerifyPassword(UserName, Password);



            _logger.LogDebug(result.ToString());
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
        [HttpPost("VerifyPassword")]
        [ProducesResponseType(200, Type = typeof(FacultyDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateVerifyPassword(string UserName, string NewPassword, string OldPassword, long FacultyId)
        {
            //   _logger.LogDebug($" at product sub categories {{@this}} in Get method." +
            //$"\r\n product subcategories", ToString());
            var result = await _ChangePasswordService.UpdateVerifyPassword(UserName, NewPassword, OldPassword, FacultyId);
            _logger.LogDebug(result.ToString());
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost("reset-password")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> PasswordReset([FromQuery] string userName, [FromQuery] string password)
        {
            _logger.LogInformation("{MethodName} method is called", nameof(PasswordReset));

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return BadRequest();
            }

            try
            {
                var result = await _ChangePasswordService.PasswordReset(userName, password);
                _logger.LogDebug(result.ToString());

                return result == 0
                    ? NotFound(new ProblemDetails { Title = "Reset Failed", Detail = "No user found or reset failed.", Status = 404 })
                    : Ok(result);
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
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(TokenDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Login([FromBody][Required] LoginUserDto user)
        {
            if (!string.IsNullOrEmpty(user.Role.ToString()))
            {
                if (((user.Role.ToString() == "Parent") || (user.Role.ToString() == "Student")) && (string.IsNullOrEmpty(user.AdmissionNo)))
                {
                    return BadRequestError("Mobile number cannot be empty.");
                }
                if ((user.Role.ToString() == "Teacher" || (user.Role.ToString() == "Admin") || (user.Role.ToString() == "Principal")) && (string.IsNullOrEmpty(user.Username)))
                {
                    return BadRequestError("Username cannot be empty.");
                }
                if (((user.Role.ToString() == "Teacher") || (user.Role.ToString() == "Admin") || (user.Role.ToString() == "Principal")) && (string.IsNullOrEmpty(user.Password)))
                {
                    return BadRequestError("Password cannot be empty.");
                }
            }

            try
            {
                var token = await _ChangePasswordService.LoginAsync(user);
                if (token != null)
                {
                    return Ok(token);
                }
                else
                {
                    return Unauthorized(new { message = "You are not allowed to login." });
                }
            }
            catch (Exception ex)
            {
                return BadRequestError(ex);
            }

        }

        [AllowAnonymous]
        [HttpGet("GetLoginList")]
        [ProducesResponseType(200, Type = typeof(StudentMobileList))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetLoginList(string mobileNo)
        {

            if ((string.IsNullOrEmpty(mobileNo)))
            {
                return BadRequestError("Mobile number cannot be empty.");
            }


            try
            {
                var students = await _studentService.GetStudentDetails(null);
                var student = students.Select(x =>
                    (x.Father_MobileNumber?.Equals(mobileNo) ?? false) ||
                    (x.Mother_MobileNumber?.Equals(mobileNo) ?? false));
                if (student != null)
                {
                    List<StudentMobileList> matchingAdmissionNumbers = students
                            .Where(s => s.Father_MobileNumber == mobileNo || s.Mother_MobileNumber == mobileNo)
                            .Select(s => new StudentMobileList
                            {
                                AdmissionNo = s.AdmissionNumber,
                                StudentName = s.StudentName
                            })
                            .ToList();

                    return Ok(matchingAdmissionNumbers);
                }
                else
                {
                    throw new Exception("The mobile number does not match.");
                }

            }
            catch (Exception ex)
            {
                return BadRequestError(ex);
            }

        }

    }
}
