using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace PrintWayyMovieTheater.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult Try(Func<IActionResult> action)
        {
            try
            {
                return action();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
