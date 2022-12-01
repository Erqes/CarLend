using System;
using System.Collections.Generic;

namespace CarRent.Entites
{
    public class Rent
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

    }
}
