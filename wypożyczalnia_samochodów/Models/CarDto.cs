using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wypożyczalnia_samochodów.Models
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string Name { get; set; }
        public float Combustion { get; set; }
        public int WypozyczalniaId { get; set; }
    }
}
