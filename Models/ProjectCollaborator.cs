namespace SistemCrud.Models
{
    public class ProjectCollaborator
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public Guid CollaboratorId { get; set; }
        public Collaborator Collaborator { get; set; }
    }
}