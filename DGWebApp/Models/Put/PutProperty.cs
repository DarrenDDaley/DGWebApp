using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGWebApp.Models.Put
{
    public class PutProperty
    {
        public string Housenumber { get; set; }

        public string Street { get; set; }

        public string Town { get; set; }

        public string PostCode { get; set; }

        public DateTime AvailableFrom { get; set; }

        public string Status { get; set; }

        public int LandlordId { get; set; }
    }
}
