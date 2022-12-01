using System.Collections.Generic;

namespace CarRent.Entites
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual List<Customer> Customers { get; set; }
        //public virtual CarRental carRental { get; set; }
        //public int carRentalId { get; set; }

    }
}
