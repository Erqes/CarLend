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
        public int carRentId { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
}
