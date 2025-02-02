namespace PersonalFinanceTracker.Entities
{
    public sealed class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string Username { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public string PasswordHash { get; set; } = String.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
