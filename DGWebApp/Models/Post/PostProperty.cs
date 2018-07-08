using System;
using System.ComponentModel.DataAnnotations;

namespace DGWebApp.Models.Post
{
    public class PostProperty
    {
        [StringLength(10)]
        [Required(ErrorMessage = "A house number is required")]
        public string Housenumber { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "A street is required")]
        public string Street { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "A town is required")]
        public string Town { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "A post code is required")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "A available from date is required")]
        public DateTime AvailableFrom { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "A status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "A landlord id is required")]
        public int LandlordId { get; set; }
    }
}
