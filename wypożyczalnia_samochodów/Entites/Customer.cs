using System.Collections.Generic;

namespace CarRent.Entites
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public virtual List<Car> Cars { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual CarRental CarRental { get; set; }

    }
}
