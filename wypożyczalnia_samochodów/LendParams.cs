using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent
{
    public class LendParams
    {
        public CarType carClass { get; set; }
        public float km { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public DateTime driveLicense { get; set; }
    }
}
