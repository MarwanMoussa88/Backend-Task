using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Task.Entities
{
    public class User:IdentityUser
    {
        public DateTime LastLogin { get; set; }
    }
}
