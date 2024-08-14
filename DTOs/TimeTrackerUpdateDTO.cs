namespace SistemCrud.DTOs
{
    public class TimeTrackerUpdateDTO
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid TarefasId { get; set; }
        public Guid CollaboratorId { get; set; }
    }
}
