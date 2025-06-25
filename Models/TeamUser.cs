namespace TaskManagementAPI.Models
{
    public class TeamUser
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid TeamId { get; set; }
        public Team Team { get; set; }

        public string Role { get; set; } // e.g., "Admin", "Member"
    }

}
