using Microsoft.AspNetCore.Mvc;
using SistemCrud.DTOs;
using SistemCrud.Services;

namespace SistemCrud.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioServices _usuarioServices;

        public UsuarioController(UsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Adicionar(UserDTO userDTO)
        {
            try
            {
                var user = await _usuarioServices.Adicionar(userDTO);
                return CreatedAtAction(nameof(Adicionar), new { id = user.Id }, new UserDTO
                {
                    UUIDUserName = user.UUIDUserName
                });
            }
            catch (Exception ex) when (ex.Message == "Usuário com esse UserName já existe.")
            {
                return Conflict(new { message = ex.Message }); // Retorna 409 Conflict
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", detail = ex.Message }); // Retorna 500 Internal Server Error
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UserReadDTO>>> BuscarUsuarios()
        {
            var users = await _usuarioServices.BuscarTodosUsuarios();
            var userReadDTOs = users.Select(u => new UserReadDTO
            {
                Id = u.Id,
                UUIDUserName = u.UUIDUserName

            }).ToList();

            return Ok(userReadDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> BuscarUsuarioPorId(Guid id)
        {
            var user = await _usuarioServices.BuscarPorId(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = new UserDTO
            {
                UUIDUserName = user.UUIDUserName

            };

            return Ok(userDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Apagar(Guid id)
        {
            var result = await _usuarioServices.Apagar(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
