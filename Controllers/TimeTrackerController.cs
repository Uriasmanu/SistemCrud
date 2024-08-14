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

            try
            {
                var result = await _service.StartTrackingAsync(dto);

                if (result)
                {
                    return Ok("Tracking started successfully.");
                }
                else
                {
                    // Consider providing more details if available
                    return StatusCode(500, "Failed to start tracking. Please try again later.");
                }
            }
            catch (Exception ex) when (ex.Message.Contains("Tarefa não encontrada"))
            {
                // Mensagem de erro específica para tarefas não encontradas
                return NotFound("Tarefa não encontrada. Verifique o ID da tarefa.");
            }
            catch (Exception ex) when (ex.Message.Contains("Colaborador não encontrado"))
            {
                // Mensagem de erro específica para colaboradores não encontrados
                return NotFound("Colaborador não encontrado. Verifique o ID do colaborador.");
            }
            catch (Exception ex)
            {
                // Mensagem genérica para outros tipos de erro
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
