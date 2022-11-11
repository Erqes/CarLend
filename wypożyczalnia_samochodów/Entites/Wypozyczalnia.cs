using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wypożyczalnia_samochodów.Entites
{
    public class Wypozyczalnia
    {
        public int Id { get; set; }
        public virtual List<Car> Cars { get; set; }
    }
}
