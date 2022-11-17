using MimeKit.Encodings;
using System;
using System.Collections.Generic;

namespace CarRent
{
    public class ReservationParams
    {
        public List<int> id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
}
