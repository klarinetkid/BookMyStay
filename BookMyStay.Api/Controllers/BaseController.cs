using Microsoft.AspNetCore.Mvc;

namespace BookMyStay.Api.Controllers
{
    public class BaseController : Controller
    {
        internal IActionResult ModelValidationErrorResult()
        {
            ErrorResponse response = new ErrorResponse()
            {
                ValidationErrors = ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)).ToArray()
            };
            return BadRequest(response);
        }
    }

    public class ErrorResponse
    {
        public string[]? ValidationErrors { get; set; }
    }
}
