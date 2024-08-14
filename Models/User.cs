namespace SistemCrud.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UUIDUserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Collaborator Collaborator { get; set; }
    }
}
