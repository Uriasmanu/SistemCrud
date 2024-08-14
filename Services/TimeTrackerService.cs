using Microsoft.EntityFrameworkCore;
using SistemCrud.Data;
using SistemCrud.DTOs;
using SistemCrud.Models;

namespace SistemCrud.Services
{
    public class TimeTrackerService
    {
        private readonly ApplicationDbContext _dbContext;

        public TimeTrackerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TimeTracker>> GetAllTrackingsAsync()
        {
            return await _dbContext.TimeTrackers.ToListAsync();
        }


        public async Task<bool> StartTrackingAsync(TimeTrackerStartDTO dto)
        {
            // Verificar se a tarefa existe
            var tarefa = await _dbContext.Tarefas.FindAsync(dto.TarefasId);
            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            // Verificar se o colaborador existe
            var colaborador = await _dbContext.Collaborators.FindAsync(dto.CollaboratorId);
            if (colaborador == null)
            {
                throw new Exception("Colaborador não encontrado.");
            }

            // Criar e adicionar o TimeTracker se as verificações passarem
            var timeTracker = new TimeTracker
            {
                Id = Guid.NewGuid(),
                StartTime = dto.StartTime,
                TarefasId = dto.TarefasId,
                CollaboratorId = dto.CollaboratorId,
                CreatedAt = dto.CreatedAt
            };

            _dbContext.TimeTrackers.Add(timeTracker);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> EndTrackingAsync(TimeTrackerEndDTO dto)
        {
            // Verificar se a tarefa existe
            var tarefa = await _dbContext.Tarefas.FindAsync(dto.TarefasId);
            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            // Verificar se o colaborador existe
            var colaborador = await _dbContext.Collaborators.FindAsync(dto.CollaboratorId);
            if (colaborador == null)
            {
                throw new Exception("Colaborador não encontrado.");
            }

            // Encontrar o TimeTracker existente para a mesma tarefa e colaborador
            var timeTracker = await _dbContext.TimeTrackers
                .Where(tt => tt.TarefasId == dto.TarefasId && tt.CollaboratorId == dto.CollaboratorId && !tt.EndTime.HasValue)
                .FirstOrDefaultAsync();

            if (timeTracker == null)
            {
                throw new Exception("Registro de tempo não encontrado ou já finalizado.");
            }

            // Atualizar o EndTime do TimeTracker encontrado
            timeTracker.EndTime = dto.EndTime;

            _dbContext.TimeTrackers.Update(timeTracker);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }


        public async Task<bool> DeleteTrackingAsync(Guid id)
        {
            // Encontrar o TimeTracker pelo ID
            var timeTracker = await _dbContext.TimeTrackers.FindAsync(id);
            if (timeTracker == null)
            {
                throw new Exception("Registro de tempo não encontrado.");
            }

            _dbContext.TimeTrackers.Remove(timeTracker);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }


    }
}
