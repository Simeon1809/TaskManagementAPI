using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class AddUserToTeamRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [RegularExpression("TeamAdmin|Member", ErrorMessage = "Role must be either 'TeamAdmin' or 'Member'")]
        public string Role { get; set; }
    }


}
