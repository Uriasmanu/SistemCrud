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
            var timeTracker = new TimeTracker
            {
                Id = Guid.NewGuid(),
                StartTime = dto.StartTime,
                TarefasId = dto.TarefasId,
                CollaboratorId = dto.CollaboratorId,
                CreatedAt = dto.CreatedAt
            };

            _dbContext.Add(timeTracker);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

    }
}
