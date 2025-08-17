using FullStackDeveloperTask.Api.Models;
using FullStackDeveloperTask.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullStackDeveloperTask.Api.Controllers
{
    [Route("api/forklift")]
    [ApiController]
    public class ForkliftController(IForkliftService forkliftService, ILogger<ForkliftController> log) : Controller
    {
        private static readonly string[] AllowedExtensions2 = [".json", ".csv"];

        /// <summary>
        /// Import Forklifts
        /// </summary>
        /// <param name="forklifs">File that contains the forklifts to import/param>
        /// <returns>Ok response</returns>
        /// <response code="200">Import Completed</response>
        /// <response code="400">Bad Request</response>
        [HttpPost("import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ImportForklifts(IFormFile forklifs)
        {
            try
            {
                if (forklifs is null || forklifs.Length == 0)
                {
                    return BadRequest("Not File submited");
                }

                var fileExtension = Path.GetExtension(forklifs.FileName);
                if (!AllowedExtensions2.Contains(fileExtension))
                {
                    return BadRequest("File format not supported");
                }

                await forkliftService.ImportForklifts(forklifs, fileExtension, HttpContext.RequestAborted);

                log.LogInformation("{MethodName}: Forklifts imported succesfully.", nameof(ImportForklifts));

                return Ok();
            }
            catch(NotSupportedException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get Forklifts
        /// </summary>
        /// <param name="skip">Amount of entries to skip.</param>
        /// <param name="take">Amount of entries to take.</param>
        /// <returns>List of Forklift</returns>
        /// <response code="200">Forklift List</response>
        /// <response code="500">Unhandled error</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetForkliftResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetForklifts([FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            try
            {
                var forklifts = await forkliftService.GetForklifts(skip, take, HttpContext.RequestAborted)
                .ConfigureAwait(false);

                log.LogInformation("{MethodName}: Forklifts returned successfully.", nameof(GetForklifts));

                return Ok(forklifts);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Execute Command
        /// </summary>
        /// <param name="command">Command to execute/param>
        /// <returns>Command execution</returns>
        /// <response code="200">Command executed Completed</response>
        /// <response code="400">Bad Request</response>
        [HttpPost("command")]
        [ProducesResponseType(typeof(CommandExecuteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ExecuteCommand(string command)
        {
            try
            {
                if (command is null || command.Length == 0)
                {
                    return BadRequest("No command submited");
                }

                var response = forkliftService.ExecuteCommand(command);

                log.LogInformation("{MethodName}: Command executed successfully.", nameof(ExecuteCommand));

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
