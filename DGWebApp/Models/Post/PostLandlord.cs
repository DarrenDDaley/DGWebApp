using System.ComponentModel.DataAnnotations;

namespace DGWebApp.Models.Post
{
    public class PostLandlord
    {
        [StringLength(100)]
        [Required(ErrorMessage = "A forename is required")]
        public string Forename { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "A surname is required")]
        public string Surname { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "A phone number is required")]
        public string Phone { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "A email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
