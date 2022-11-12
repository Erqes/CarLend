using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Entites;

namespace CarRent
{
    interface ICarsRepository
    {
        IEnumerable<Car> GetCars();
        IEnumerable<Car> CarCount();
    }
}
