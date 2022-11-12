using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Entites
{
    public class Car
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string Name { get; set; }
        public float Combustion { get; set; }
        public string Localization { get; set; }
        public int CarRentId { get; set; }
        public virtual CarRental CarRent { get; set; }

    }
}
