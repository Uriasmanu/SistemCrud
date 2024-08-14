namespace SistemCrud.Models
{
    public class Collaborator
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<ProjectCollaborator>? ProjectCollaborators { get; set; }
        public ICollection<Tarefas>? Tarefas { get; set; }
        public ICollection<TimeTracker>? TimeTrackers { get; set; }

    }
}