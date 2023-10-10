using System.ComponentModel.DataAnnotations;

namespace Backend_Task.Models.User
{
    public class BaseUser
    {
        [Key]
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
