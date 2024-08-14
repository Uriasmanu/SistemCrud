using Microsoft.AspNetCore.Mvc;
using SistemCrud.DTOs;
using SistemCrud.Services;
using System;
using System.Threading.Tasks;

namespace SistemCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeTrackerController : ControllerBase
    {
        private readonly TimeTrackerService _timeTrackerService;

        public TimeTrackerController(TimeTrackerService timeTrackerService)
        {
            _timeTrackerService = timeTrackerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrackings()
        {
            try
            {
                var trackings = await _timeTrackerService.GetAllTrackingsAsync();

                if (trackings != null && trackings.Any())
                {
                    return Ok(trackings);
                }
                else
                {
                    return NotFound("Nenhum registro de tempo encontrado.");
                }
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("End time must be greater than or equal to start time"))
            {
                return BadRequest("Há um problema com os registros de tempo. Verifique se todos os tempos de término são posteriores aos tempos de início.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartTracking([FromBody] TimeTrackerStartDTO dto)
        {
            try
            {
                var result = await _timeTrackerService.StartTrackingAsync(dto);
                if (result)
                {
                    return Ok(new { message = "Time tracking started successfully." });
                }
                return BadRequest(new { message = "Failed to start time tracking." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("end")]
        public async Task<IActionResult> EndTracking([FromBody] TimeTrackerEndDTO dto)
        {
            try
            {
                var result = await _timeTrackerService.EndTrackingAsync(dto);
                if (result)
                {
                    return Ok(new { message = "Time tracking ended successfully." });
                }
                return BadRequest(new { message = "Failed to end time tracking." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTracking(Guid id)
        {
            try
            {
                var result = await _timeTrackerService.DeleteTrackingAsync(id);
                if (result)
                {
                    return Ok(new { message = "Time tracking deleted successfully." });
                }
                return BadRequest(new { message = "Failed to delete time tracking." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
