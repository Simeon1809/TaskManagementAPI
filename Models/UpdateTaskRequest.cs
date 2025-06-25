using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class UpdateTaskRequest
    {
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }



}
