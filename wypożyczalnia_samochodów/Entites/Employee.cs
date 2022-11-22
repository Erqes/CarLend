using System.Collections.Generic;

namespace CarRent.Entites
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public virtual List<Customer> customers { get; set; }
        //public virtual CarRental carRental { get; set; }
        //public int carRentalId { get; set; }

    }
}
