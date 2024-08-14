using Microsoft.EntityFrameworkCore;
using SistemCrud.Data;
using SistemCrud.DTOs;
using SistemCrud.Models;

namespace SistemCrud.Services
{
    public class ProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<IEnumerable<Project>> GetActiveProjectsAsync()
        {
            return await _context.Projects
                .Where(p => p.DeletedAt == null)
                .Include(p => p.Tarefas)
                .ToListAsync();
        }


        public async Task<IEnumerable<Project>> GetDeletedProjectsAsync()
        {
            return await _context.Projects
                .Where(p => p.DeletedAt != null)
                .ToListAsync();
        }

        public async Task<Project> CreateProjectAsync(ProjetoDTO projetoDTO)
        {
            var projetoExiste = await _context.Projects
                .FirstOrDefaultAsync(u => u.Name == projetoDTO.Name);

            if (projetoExiste != null)
            {
                throw new ArgumentException("Já existe um projeto com esse nome.");
            }

            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = projetoDTO.Name,
                Descricao = projetoDTO.Descricao,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task UpdateProjectAsync(Project project)
        {
            var existingProject = await _context.Projects.FindAsync(project.Id);
            if (existingProject == null) throw new ArgumentException("Projeto não encontrado");

            existingProject.Name = project.Name;
            existingProject.UpdatedAt = DateTime.UtcNow;

            _context.Projects.Update(existingProject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) throw new ArgumentException("Projeto não encontrado");

            project.DeletedAt = DateTime.UtcNow;
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
