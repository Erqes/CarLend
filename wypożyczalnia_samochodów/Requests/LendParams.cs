using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Requests.Enums;

namespace CarRent.Requests
{
    public class LendParams
    {
        public CarType CarClass { get; set; }
        public float Km { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime DriveLicense { get; set; }
    }
}
