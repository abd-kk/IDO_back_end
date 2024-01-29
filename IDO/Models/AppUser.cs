using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IDO.Models
{
    public class AppUser : IdentityUser
    {
        [Key]

        public int Id { get; set; }

        

    }
}
