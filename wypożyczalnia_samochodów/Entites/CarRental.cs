using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Entites
{
    public class CarRental
    {
        public int id { get; set; }
        public virtual List<Car> cars { get; set; }
        public virtual List<Employee> employees { get; set; }
    }
}
