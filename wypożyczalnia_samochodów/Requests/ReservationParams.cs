using MimeKit.Encodings;
using System;
using System.Collections.Generic;

namespace CarRent.Requests
{
    public class ReservationParams
    {
        public List<int> CarsId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
