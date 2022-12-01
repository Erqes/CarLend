using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Entites
{
    public class Car
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string Name { get; set; }
        public int HorsePower { get; set; }
        public float Price { get; set; }
        public string Color { get; set; }
        public float Combustion { get; set; }
        public string Localization { get; set; }
        public bool IsCar { get; set; }
        public List<Customer> Customers { get; set; }
        //public virtual CarRental carRent { get; set; }
        //public int carRentId { get; set; }

    }
}
