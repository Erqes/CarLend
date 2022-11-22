using System.Collections.Generic;

namespace CarRent.Models
{
    public class CarRentDto
    {
        public int id { get; set; }
        public List<CarDto> cars { get; set; }
    }
}
