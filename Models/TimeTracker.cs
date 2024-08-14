using System.ComponentModel.DataAnnotations.Schema;

namespace SistemCrud.Models
{
    public class TimeTracker
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid TarefasId { get; set; }
        public Tarefas Tarefas { get; set; }
        public Guid CollaboratorId { get; set; }
        public Collaborator Collaborator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [NotMapped]
        public TimeSpan Duration => GetDuration();

        // Method to calculate the duration
        public TimeSpan GetDuration()
        {
            if (EndTime < StartTime)
            {
                throw new InvalidOperationException("End time must be greater than or equal to start time.");
            }

            return EndTime - StartTime;
        }


    }
}