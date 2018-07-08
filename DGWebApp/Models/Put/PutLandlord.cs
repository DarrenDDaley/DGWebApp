using System.ComponentModel.DataAnnotations;

namespace DGWebApp.Models.Put
{
    public class PutLandlord
    {
        [StringLength(100)]
        public string Forename { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(150)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
