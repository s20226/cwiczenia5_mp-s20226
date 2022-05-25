using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.Models
{
    public class ClientTripRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int IdTrip { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}

