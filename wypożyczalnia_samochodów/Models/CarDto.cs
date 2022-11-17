using System;

namespace CarRent.Models
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string Name { get; set; }
        public float Combustion { get; set; }
        public string Localization { get; set; }
        public int CarRentId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
