namespace TaskManagementAPI.Models
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid AssignedToUserId { get; set; }
        public Guid TeamId { get; set; }

        public TaskDto(Task task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            DueDate = task.DueDate;
            Status = task.Status;
            CreatedAt = task.CreatedAt;
            AssignedToUserId = task.AssignedToUserId;
            TeamId = task.TeamId;
        }
    }
}
