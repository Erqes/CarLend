using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wypożyczalnia_samochodów.Models
{
    public class WypozyczalniaDto
    {
        public int Id { get; set; }
        public int CarCount { get; set; }
        public string Name { get; set; }
        public List<CarDto> Cars { get; set; }
    }
}
