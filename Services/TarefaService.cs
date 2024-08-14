using Microsoft.EntityFrameworkCore;
using SistemCrud.Data;
using SistemCrud.DTOs;
using SistemCrud.Models;

namespace SistemCrud.Services
{
    public class TarefaService
    {
        private readonly ApplicationDbContext _context;

        public TarefaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tarefas> GetTarefaByIdAsync(Guid id)
        {
            return await _context.Tarefas
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TarefaDTO>> GetAllTarefasAsync()
        {
            return await _context.Tarefas
                .Include(t => t.Project)
                .Select(t => new TarefaDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Descritiva = t.Descritiva,
                    ProjectId = t.ProjectId,
                    CreatedAt = t.CreatedAt,
                    Status = t.Status,
                    TimeTrackers = t.TimeTrackers,
                })
                .ToListAsync();
        }

        public async Task<Tarefas> CreateTarefaAsync(TarefaDTO tarefaDTO)
        {
            // Verifica se já existe uma tarefa com o mesmo nome
            var tarefaExiste = await _context.Tarefas
                .FirstOrDefaultAsync(t => t.Name == tarefaDTO.Name);

            if (tarefaExiste != null)
            {
                throw new ArgumentException("Já existe uma tarefa com esse nome.");
            }

            // Verifica se o projeto associado existe
            var projetoExiste = await _context.Projects
                .FindAsync(tarefaDTO.ProjectId);

            if (projetoExiste == null)
            {
                throw new ArgumentException("Projeto associado não encontrado.");
            }

            // Cria uma nova tarefa
            var novaTarefa = new Tarefas
            {
                Id = Guid.NewGuid(),
                Name = tarefaDTO.Name,
                Descritiva = tarefaDTO.Descritiva,
                ProjectId = tarefaDTO.ProjectId,
                CreatedAt = DateTime.UtcNow
            };

            // Adiciona e salva a nova tarefa no contexto
            _context.Tarefas.Add(novaTarefa);
            await _context.SaveChangesAsync();

            return novaTarefa;
        }


        public async Task UpdateTarefaAsync(Tarefas tarefa, TaskStatus newStatus)
        {
            var existingTarefa = await _context.Tarefas.FindAsync(tarefa.Id);
            if (existingTarefa == null) throw new ArgumentException("Tarefa não encontrada");

            existingTarefa.Name = tarefa.Name;
            existingTarefa.Descritiva = tarefa.Descritiva;
            existingTarefa.ProjectId = tarefa.ProjectId;
            existingTarefa.Status = newStatus;
            existingTarefa.UpdatedAt = DateTime.UtcNow;

            _context.Tarefas.Update(existingTarefa);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteTarefaAsync(Guid id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) throw new ArgumentException("Tarefa não encontrada");

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}
