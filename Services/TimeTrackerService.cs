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

            // Verificar se o colaborador já está rastreando uma tarefa
            var currentTracking = await _dbContext.TimeTrackers
                .Where(tt => tt.CollaboratorId == dto.CollaboratorId && tt.EndTime == null)
                .FirstOrDefaultAsync();

            if (currentTracking != null)
            {
                // Se já houver um rastreamento em andamento, encerre-o
                currentTracking.EndTime = DateTime.UtcNow;
                _dbContext.TimeTrackers.Update(currentTracking);
                await _dbContext.SaveChangesAsync();
            }

            // Verificar se a nova tarefa excede o limite de 24 horas
            var newTracking = new TimeTracker
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc), // Garantir que StartTime esteja em UTC
                EndTime = null, // Inicialmente null
                TarefasId = dto.TarefasId,
                CollaboratorId = dto.CollaboratorId,
                CreatedAt = DateTime.SpecifyKind(dto.CreatedAt, DateTimeKind.Utc) // Garantir que CreatedAt esteja em UTC
            };

            // Adicionar o novo rastreamento
            _dbContext.TimeTrackers.Add(newTracking);

            // Verificar a duração após a inserção
            if (newTracking.GetDuration().HasValue && newTracking.GetDuration().Value.TotalHours > 24)
            {
                throw new InvalidOperationException("A duração não pode exceder 24 horas.");
            }

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
                .Where(tt => tt.TarefasId == dto.TarefasId && tt.CollaboratorId == dto.CollaboratorId && tt.EndTime == null)
                .FirstOrDefaultAsync();

            if (timeTracker == null)
            {
                throw new Exception("Registro de tempo não encontrado ou já finalizado.");
            }

            // Atualizar o EndTime do TimeTracker encontrado
            timeTracker.EndTime = DateTime.SpecifyKind(dto.EndTime, DateTimeKind.Utc); // Garantir que EndTime esteja em UTC

            // Verificar a duração após a atualização
            if (timeTracker.GetDuration().HasValue && timeTracker.GetDuration().Value.TotalHours > 24)
            {
                throw new InvalidOperationException("A duração não pode exceder 24 horas.");
            }

            _dbContext.TimeTrackers.Update(timeTracker);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateTrackingAsync(Guid id, TimeTrackerUpdateDTO dto)
        {
            // Encontrar o TimeTracker pelo ID
            var timeTracker = await _dbContext.TimeTrackers.FindAsync(id);
            if (timeTracker == null)
            {
                throw new Exception("Registro de tempo não encontrado.");
            }

            // Verificar se o colaborador existe
            var colaborador = await _dbContext.Collaborators.FindAsync(dto.CollaboratorId);
            if (colaborador == null)
            {
                throw new Exception("Colaborador não encontrado.");
            }

            // Verificar se a tarefa existe
            var tarefa = await _dbContext.Tarefas.FindAsync(dto.TarefasId);
            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada.");
            }

            // Atualizar os campos de data de início e fim
            timeTracker.StartTime = DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc); // Garantir que StartTime esteja em UTC
            timeTracker.EndTime = DateTime.SpecifyKind((DateTime)dto.EndTime, DateTimeKind.Utc); // Garantir que EndTime esteja em UTC

            // Verificar se a duração excede 24 horas
            if (timeTracker.GetDuration().HasValue && timeTracker.GetDuration().Value.TotalHours > 24)
            {
                throw new InvalidOperationException("A duração não pode exceder 24 horas.");
            }

            // Verificar se o colaborador já está rastreando uma tarefa
            var currentTracking = await _dbContext.TimeTrackers
                .Where(tt => tt.CollaboratorId == dto.CollaboratorId && tt.EndTime == null && tt.Id != id)
                .FirstOrDefaultAsync();

            if (currentTracking != null)
            {
                // Se o colaborador já está rastreando outra tarefa, encerre-a
                currentTracking.EndTime = DateTime.UtcNow;
                _dbContext.TimeTrackers.Update(currentTracking);
            }

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
