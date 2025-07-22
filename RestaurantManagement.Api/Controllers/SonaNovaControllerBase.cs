using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.Api.Controllers
{
    public class SonaNovaControllerBase : ControllerBase
    {
        protected readonly ILogger _logger;
        // Get the Role claim value
        protected string Role => User.FindFirst("Role")?.Value;

        // Get the UserId claim value
        protected int UserId => Convert.ToInt32(User.FindFirst("UserId")?.Value);

        public SonaNovaControllerBase(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult InternalServerError(Exception ex)
        {
            _logger.LogError("Error invoking service: {message}", ex.Message);
            _logger.LogError("Error invoking service stack trace: {StackTrace}", ex.StackTrace);
            _logger.LogError("Error invoking service inner exception: {InnerException}", ex.InnerException);
            var errorResponse = new { message = ex.Message };

            return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }

        protected IActionResult BadRequestError(Exception ex)
        {
            _logger.LogError("Error executing the request: {message}", ex.Message);
            var errorResponse = new { message = ex.Message };
            return StatusCode(StatusCodes.Status400BadRequest, errorResponse);
        }

        protected IActionResult BadRequestError(string errorMessage)
        {
            var errorResponse = new { message = errorMessage };
            return StatusCode(StatusCodes.Status400BadRequest, errorResponse);
        }

        protected IActionResult SuccessMessage(string message)
        {
            return StatusCode(StatusCodes.Status200OK, message);
        }
    }
}
