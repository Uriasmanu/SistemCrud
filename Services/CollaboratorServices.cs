using Microsoft.EntityFrameworkCore;
using SistemCrud.Data;
using SistemCrud.Models;

namespace SistemCrud.Services
{
    public class CollaboratorServices
    {
        private readonly ApplicationDbContext _dbContext;

        public CollaboratorServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Collaborator>> BuscarTodosCollaborators()
        {
            return await _dbContext.Collaborators.ToListAsync();
        }

    }
}
