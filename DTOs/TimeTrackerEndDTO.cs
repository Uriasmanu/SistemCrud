namespace SistemCrud.DTOs
{
    public class TimeTrackerEndDTO
    {
        public DateTime EndTime { get; set; }

        public Guid TarefasId { get; set; }
        public Guid CollaboratorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
