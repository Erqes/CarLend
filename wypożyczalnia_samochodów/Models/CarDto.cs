using System;

namespace CarRent.Models
{
    public class CarDto
    {
        public int id { get; set; }
        public string Class { get; set; }
        public string name { get; set; }
        public float combustion { get; set; }
        public string localization { get; set; }
        public int horsePower { get; set; }
        public string color { get; set; }
        public float price { get; set; }
        public int carRentId { get; set; }
    }
}
