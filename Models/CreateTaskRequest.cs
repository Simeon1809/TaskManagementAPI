using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class CreateTaskRequest
    {
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public Guid AssignedToUserId { get; set; }

        [Required]
        public Guid TeamId { get; set; }
    }

}
