using Microsoft.AspNetCore.Mvc;
using SistemCrud.DTOs;
using SistemCrud.Services;

namespace SistemCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeTrackerController : ControllerBase
    {
        private readonly TimeTrackerService _service;

        public TimeTrackerController(TimeTrackerService service)
        {
            _service = service;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartTracking([FromBody] TimeTrackerStartDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _service.StartTrackingAsync(dto);

            if (result)
            {
                return Ok("Tracking started successfully.");
            }

            return StatusCode(500, "An error occurred while starting tracking.");
        }
    }
}
