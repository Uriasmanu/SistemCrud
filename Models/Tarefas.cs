namespace SistemCrud.Models
{
    public class Tarefas
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descritiva { get; set; }
        public TaskStatus Status { get; set; }
        public Project Project { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<TimeTracker>? TimeTrackers { get; set; }
        public Collaborator? Collaborator { get; set; }
        public Guid? CollaboratorId { get; set; }
    }

    public enum Status
    {
        WaitingForActivation = 0,
        WaitingToRun = 1,
        Running = 2,
        CompletedSuccessfully = 3,
        Faulted = 4,
        Canceled = 5
    }
}