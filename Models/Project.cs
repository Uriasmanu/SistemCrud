using System.ComponentModel.DataAnnotations;

namespace SistemCrud.Models
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<Tarefas>? Tarefas { get; set; }

        public ICollection<ProjectCollaborator>? ProjectCollaborators { get; set; }
    }
}