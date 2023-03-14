using Microsoft.AspNetCore.Mvc;

namespace News.API.Controllers
{
    public class ErrorService : ControllerBase
    {
        public IActionResult Error()
        {
            var errorMessage = "An error occurred";

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = errorMessage });
        }
    }
}
