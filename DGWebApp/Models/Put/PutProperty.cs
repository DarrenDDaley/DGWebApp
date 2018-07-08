using System;
using System.ComponentModel.DataAnnotations;

namespace DGWebApp.Models.Put
{
    public class PutProperty
    {
        [StringLength(10)]
        public string Housenumber { get; set; }

        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(50)]
        public string Town { get; set; }

        [StringLength(12)]
        public string PostCode { get; set; }

        public DateTime AvailableFrom { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        public int LandlordId { get; set; }
    }
}
