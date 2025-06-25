using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class UpdateTaskStatusRequest
    {
        [Required]
        [EnumDataType(typeof(TaskStatus))]
        public TaskStatus Status { get; set; }
    }

}
