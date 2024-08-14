using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemCrud.Data;
using SistemCrud.DTOs;
using SistemCrud.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemCrud.Services
{
    public class UsuarioServices
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> BuscarPorUUIDUserNameESenha(string uuidUserName, string password)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UUIDUserName == uuidUserName);

            if (user != null && VerifyPassword(password, user.Password))
            {
                return user;
            }

            return null;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInputPassword = HashPassword(password);
            return hashedInputPassword == hashedPassword;
        }

        public async Task<List<UserReadDTO>> BuscarTodosUsuarios()
        {
            var users = await _dbContext.Users.ToListAsync();

            var userReadDTOs = users.Select(user => new UserReadDTO
            {
                Id = user.Id,
                UUIDUserName = user.UUIDUserName,
                Password = "********"
            }).ToList();

            return userReadDTOs;
        }

        public async Task<User> Adicionar(UserDTO userDTO)
        {
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UUIDUserName == userDTO.UUIDUserName);

            if (existingUser != null)
            {
                throw new Exception("Usuário com esse UUIDUserName já existe.");
            }

            var hashedPassword = HashPassword(userDTO.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                UUIDUserName = userDTO.UUIDUserName,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow,
            };

            var collaborator = new Collaborator
            {
                Id = Guid.NewGuid(),
                Name = userDTO.UUIDUserName,
                CreatedAt = DateTime.UtcNow,
                User = user
            };

            _dbContext.Users.Add(user);
            _dbContext.Collaborators.Add(collaborator);

            await _dbContext.SaveChangesAsync();

            return user;
        }

        private string GerarTokenJWT(User user)
        {
            string chaveSecreta = "e3c46810-b96e-40d9-a9eb-9ffa7b373e5b";
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", user.UUIDUserName),
                new Claim("nome", user.UUIDUserName)
            };

            var token = new JwtSecurityToken(
                issuer: "system_tasks",
                audience: "seus_usuarios",
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credencial
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserReadDTO> BuscarPorId(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserReadDTO
            {
                Id = user.Id,
                UUIDUserName = user.UUIDUserName,
                Password = "********"
            };
        }

        public async Task<bool> Apagar(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            var collaborator = await _dbContext.Collaborators
                .FirstOrDefaultAsync(c => c.UserId == id);

            if (collaborator != null)
            {
                _dbContext.Collaborators.Remove(collaborator);
            }

            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
