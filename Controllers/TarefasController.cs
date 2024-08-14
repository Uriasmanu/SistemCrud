using Microsoft.AspNetCore.Mvc;
using SistemCrud.DTOs;
using SistemCrud.Models;
using SistemCrud.Services;

namespace SistemCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _tarefaService;

        public TarefaController(TarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetAllTarefas()
        {
            try
            {
                var tarefas = await _tarefaService.GetAllTarefasAsync();

                if (tarefas == null || !tarefas.Any())
                {
                    return NotFound(new { mensagem = "Nenhuma tarefa encontrada." });
                }

                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um erro ao tentar obter as tarefas.", erro = ex.Message });
            }
        }



        // GET: api/Tarefa/
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefas>> GetTarefaById(Guid id)
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        // POST: api/Tarefa
        [HttpPost]
        public async Task<ActionResult<Tarefas>> CreateTarefa(TarefaDTO tarefaDTO)
        {
            try
            {
                var createdTarefa = await _tarefaService.CreateTarefaAsync(tarefaDTO);


                var response = new
                {

                    Name = createdTarefa.Name,
                    Descritiva = createdTarefa.Descritiva,
                    ProjectId = createdTarefa.ProjectId,
                    CreatedAt = createdTarefa.CreatedAt,

                };

                return CreatedAtAction(nameof(GetTarefaById), new { id = createdTarefa.Id }, response);
            }
            catch (ArgumentException ex)
            {
                // Retorna uma resposta com uma mensagem de erro clara
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // PUT: api/Tarefa/
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarefa(Guid id, [FromBody] TarefaDTO tarefaDto)
        {
            if (id == Guid.Empty || tarefaDto == null)
            {
                return BadRequest("Invalid request data.");
            }

            try
            {

                var existingTarefa = await _tarefaService.GetTarefaByIdAsync(id);
                if (existingTarefa == null)
                {
                    return NotFound("Tarefa não encontrada.");
                }


                var newStatus = tarefaDto.Status;

                var tarefa = new Tarefas
                {
                    Id = id,
                    Name = tarefaDto.Name,
                    Descritiva = tarefaDto.Descritiva,
                    ProjectId = tarefaDto.ProjectId

                };

                await _tarefaService.UpdateTarefaAsync(tarefa, newStatus);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error.");
            }

            return NoContent();
        }

        // DELETE: api/Tarefa/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(Guid id)
        {
            try
            {
                await _tarefaService.DeleteTarefaAsync(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
