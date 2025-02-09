namespace PersonalFinanceTracker.Entities
{
    public sealed class User
    {
        public int Id { get; set; }

        public string First_Name { get; set; } = String.Empty;

        public string Last_Name { get; set; } = String.Empty;

        public string Username { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public string Password_Hash { get; set; } = String.Empty;

        public DateTime Created_At { get; set; }

        public DateTime? Updated_At { get; set; }
    }
}
