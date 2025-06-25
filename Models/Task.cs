namespace TaskManagementAPI.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; } // Enum
        public DateTime CreatedAt { get; set; }

        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public Guid AssignedToUserId { get; set; }
        public User AssignedToUser { get; set; }

        public Guid TeamId { get; set; }
        public Team Team { get; set; }
    }

}
