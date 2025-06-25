using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class CreateTeamRequest
    {
        [Required]
        public string Name { get; set; }
    }

}
