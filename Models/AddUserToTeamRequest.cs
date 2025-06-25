using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class AddUserToTeamRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [RegularExpression("Admin|Member", ErrorMessage = "Role must be either 'Admin' or 'Member'")]
        public string Role { get; set; }
    }


}
