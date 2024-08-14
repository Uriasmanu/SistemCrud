using Microsoft.EntityFrameworkCore;
using SistemCrud.Models;

namespace SistemCrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<TimeTracker> TimeTrackers { get; set; }
        public DbSet<Tarefas> Tarefas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:manarger-serve.database.windows.net,1433;Initial Catalog=api-Db;Persist Security Info=False;User ID=manoela;Password=sistem1#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=100;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurando a relação User -> Collaborator
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UUIDUserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Collaborator)
                .WithOne(c => c.User)
                .HasForeignKey<Collaborator>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurando a relação Project -> Collaborator usando a entidade de junção
            modelBuilder.Entity<ProjectCollaborator>()
                .HasKey(pc => new { pc.ProjectId, pc.CollaboratorId });

            modelBuilder.Entity<ProjectCollaborator>()
                .HasOne(pc => pc.Project)
                .WithMany(p => p.ProjectCollaborators)
                .HasForeignKey(pc => pc.ProjectId);

            modelBuilder.Entity<ProjectCollaborator>()
                .HasOne(pc => pc.Collaborator)
                .WithMany(c => c.ProjectCollaborators)
                .HasForeignKey(pc => pc.CollaboratorId);

            // Configurando a relação Tarefas -> Collaborator
            modelBuilder.Entity<Tarefas>()
                .HasOne(t => t.Collaborator)
                .WithMany(c => c.Tarefas)
                .HasForeignKey(t => t.CollaboratorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurando a relação Tarefas -> Project
            modelBuilder.Entity<Tarefas>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tarefas)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurando a relação TimeTracker -> Tarefas
            modelBuilder.Entity<TimeTracker>()
                .HasOne(tt => tt.Tarefas)
                .WithMany(t => t.TimeTrackers)
                .HasForeignKey(tt => tt.TarefasId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurando a relação TimeTracker -> Collaborator
            modelBuilder.Entity<TimeTracker>()
                .HasOne(tt => tt.Collaborator)
                .WithMany(c => c.TimeTrackers)
                .HasForeignKey(tt => tt.CollaboratorId) 
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
