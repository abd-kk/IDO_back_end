using System.ComponentModel.DataAnnotations;

namespace IDO.Models
{
    public class AuthenticationModel
    {

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
