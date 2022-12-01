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
        public List<Car> Cars { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

    }
}
