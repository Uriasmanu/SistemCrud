using Microsoft.AspNetCore.Mvc;
using SistemCrud.Models;
using System.Collections.Generic;
using System.Linq;

namespace SistemCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // Lista em memória para armazenar os usuários temporariamente
        private static List<User> users = new List<User>();

        // Endpoint para listar todos os usuários
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(users);
        }

        // Endpoint para adicionar um novo usuário
        [HttpPost]
        public ActionResult AddUser([FromBody] User newUser)
        {
            users.Add(newUser);
            return Ok("Usuário adicionado com sucesso!");
        }

        // Endpoint para deletar um usuário pelo nome
        [HttpDelete("{nome}")]
        public ActionResult DeleteUser(string nome)
        {
            var user = users.FirstOrDefault(u => u.Nome == nome);
            if (user != null)
            {
                users.Remove(user);
                return Ok("Usuário deletado com sucesso!");
            }
            return NotFound("Usuário não encontrado.");
        }
    }
}
