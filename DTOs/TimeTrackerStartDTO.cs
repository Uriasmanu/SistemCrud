using SistemCrud.Models;

namespace SistemCrud.DTOs
{
    public class TimeTrackerStartDTO
    {

        public DateTime StartTime { get; set; } = DateTime.Now;

        public Guid TarefasId { get; set; }
        public Guid CollaboratorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
