using SistemCrud.Models;

namespace SistemCrud.DTOs
{
    public class TarefaDTO
    {
        public Guid Id { get; set; }
        public TaskStatus Status { get; set; }
        public string Name { get; set; }
        public string Descritiva { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ProjectId { get; set; }
        public ICollection<TimeTracker>? TimeTrackers { get; set; }

    }
}
