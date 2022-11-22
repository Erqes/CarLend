using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Entites
{
    public class Car
    {
        public int id { get; set; }
        public string Class { get; set; }
        public string name { get; set; }
        public float combustion { get; set; }
        public string localization { get; set; }
        public bool isCar { get; set; }
        public List<Customer> customers { get; set; }
        //public virtual CarRental carRent { get; set; }
        //public int carRentId { get; set; }

    }
}
