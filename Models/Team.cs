namespace TaskManagementAPI.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<TeamUser> TeamUsers { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }

}
