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

        [HttpGet]
        public async Task<IActionResult> GetAllTrackings()
        {
            try
            {
                var trackings = await _service.GetAllTrackingsAsync();

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
            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                var result = await _service.StartTrackingAsync(dto);

                if (result)
                {
                    return Ok("Rastreamento iniciado com sucesso.");
                }
                else
                {
                    return StatusCode(500, "Falha ao iniciar o rastreamento. Por favor, tente novamente mais tarde.");
                }
            }
            catch (Exception ex) when (ex.Message.Contains("Tarefa não encontrada"))
            {
                return NotFound("Tarefa não encontrada. Verifique o ID da tarefa.");
            }
            catch (Exception ex) when (ex.Message.Contains("Colaborador não encontrado"))
            {
                return NotFound("Colaborador não encontrado. Verifique o ID do colaborador.");
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message ?? "Sem detalhes adicionais.";
                return StatusCode(500, $"Ocorreu um erro inesperado: {ex.Message}. Detalhes adicionais: {innerExceptionMessage}");
            }

        }

        [HttpPost("end")]
        public async Task<IActionResult> EndTracking([FromBody] TimeTrackerEndDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                var result = await _service.EndTrackingAsync(dto);

                if (result)
                {
                    return Ok("Rastreamento finalizado com sucesso.");
                }
                else
                {
                    return StatusCode(500, "Falha ao finalizar o rastreamento. Por favor, tente novamente mais tarde.");
                }
            }
            catch (Exception ex) when (ex.Message.Contains("Tarefa não encontrada"))
            {
                return NotFound("Tarefa não encontrada. Verifique o ID da tarefa.");
            }
            catch (Exception ex) when (ex.Message.Contains("Colaborador não encontrado"))
            {
                return NotFound("Colaborador não encontrado. Verifique o ID do colaborador.");
            }
            catch (Exception ex) when (ex.Message.Contains("Registro de tempo não encontrado ou já finalizado"))
            {
                return NotFound("Registro de tempo não encontrado ou já finalizado. Verifique se o rastreamento foi iniciado e não está finalizado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTracking(Guid id)
        {
            try
            {
                var result = await _service.DeleteTrackingAsync(id);

                if (result)
                {
                    return Ok("Rastreamento deletado com sucesso.");
                }
                else
                {
                    return StatusCode(500, "Falha ao deletar o rastreamento. Por favor, tente novamente mais tarde.");
                }
            }
            catch (Exception ex) when (ex.Message.Contains("Registro de tempo não encontrado"))
            {
                return NotFound("Registro de tempo não encontrado. Verifique o ID fornecido.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro inesperado: {ex.Message}");
            }
        }
    }
}
