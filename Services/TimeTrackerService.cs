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


    }
}
