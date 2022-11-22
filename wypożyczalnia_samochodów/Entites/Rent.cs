using System;
using System.Collections.Generic;

namespace CarRent.Entites
{
    public class Rent
    {
        public int id { get; set; }
        public Car car { get; set; }
        public int carId { get; set; }
        public Customer customer { get; set; }
        public int customerId { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }

    }
}
