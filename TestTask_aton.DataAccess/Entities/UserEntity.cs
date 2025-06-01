namespace TestTask_aton.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Gender { get; set; } = 2;
        public DateTime? BirthDay { get; set; } = null;
        public bool IsAdmin { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? ModifiedAt { get; set; } = null;
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime? RevokedAt { get; set; } = null;
        public string RevokeddBy { get; set; } = string.Empty;
    }
}
