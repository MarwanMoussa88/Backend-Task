using System.ComponentModel.DataAnnotations;

namespace Backend_Task.Models.User
{
    public class CreateUser:BaseUser
    {
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
    }
}
