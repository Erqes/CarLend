using System.Collections.Generic;

namespace CarRent.Entites
{
    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public List<Car> cars { get; set; }
        public Employee employee { get; set; }
        public int employeeId { get; set; }

    }
}
