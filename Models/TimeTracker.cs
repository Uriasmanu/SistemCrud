using System.ComponentModel.DataAnnotations.Schema;

namespace SistemCrud.Models
{
    public class TimeTracker
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid TarefasId { get; set; }
        public Tarefas Tarefas { get; set; }
        public Guid CollaboratorId { get; set; }
        public Collaborator Collaborator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public TimeSpan? Duration => GetDuration();

        // Método para calcular a duração
        public TimeSpan? GetDuration()
        {
            if (EndTime.HasValue && EndTime.Value < StartTime)
            {
                throw new InvalidOperationException("A hora de término deve ser maior ou igual à hora de início.");
            }

            return EndTime.HasValue ? EndTime.Value - StartTime : (TimeSpan?)null;
        }


    }
}