using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarRent.Entites;

namespace CarRent.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RentController : ControllerBase, ICarsRepository
    {
        float fuelPrice = 8;
        float lendPrice = 100;


        //[HttpGet]
        //public ActionResult<IEnumerable<Car>> GetAll()
        //{
        //    var cars = _dbContext.Cars.ToList();
        //    var carsDtos = cars.Select(c => new Car()
        //    {
        //        Class = c.Class,
        //        Combustion = c.Combustion,
        //    });
        //    return Ok(cars);
        //}

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        //[HttpPost("przywitaj")]
        //public string Hello([FromBody] string name)
        //{
        //    return $"Hello {name}";
        //}
        public IEnumerable<Car> GetCars()
        {
            CarRentDbContext _dbContext = new CarRentDbContext();
            return _dbContext.Cars.FirstOrDefault(c => c.Class == lendParams.carClass.ToString());
        }
        public IEnumerable<Car> CarCount()
        {
            CarRentDbContext _dbContext = new CarRentDbContext();
            return _dbContext.Cars.Count();
        }
        [HttpPost("count")]
        public string Count([FromBody] LendParams lendParams)
        {
            var car = GetCars();
            var CarsCount = CarCount();
            var howLong = DateTime.Now - lendParams.driveLicense;
            var fhowLong = (float)howLong.TotalDays;
            var days = lendParams.to - lendParams.from;
            var x = days.TotalDays;
            var fdays = (float)x;//ilosc dni w int 
            var result = (car.Combustion * lendParams.km / 100 * fuelPrice + lendPrice * fdays);

            var res = (int)lendParams.carClass switch
            {
                0 => result = result,
                10 => result = result * 1.3f,
                20 => result = result * 1.6f,
                30 => result = result * 2

            };
            float resultYears, resultCount;
            //jeśli prawo jazdy mniej niż 5 lat 
            if (5 * 365 > fhowLong)
                resultYears = result + result * 0.2f;
            else
                resultYears = 0;
            //jesli aut jest mniej niż 3 
            if (CarsCount < 3)
                resultCount = result + result * 0.15f;
            else
                resultCount = 0;
            //jeśli prawo jazdy mniej niż 3 lata i klasa Premium
            if ((3 * 365 > fhowLong) && lendParams.carClass.ToString() == "Premium")
                return "Nie można wypożyczyć samochodu";

            result = result + resultCount + resultYears;

            return $"Cena netto: {result}, Cena brutto: {result + result * 0.23}, Cena Całkowita= (Cena Wypożyczenia x ilość dni + koszt paliwa) x klasa samochodu" +
            $" + Koszt(jesli prawo jazdy posiadane jest mniej niż 5 lat) + Koszt(jeśli aut jest dostępnych mniej niż 3)=" +
            $"({100}x{fdays}+{car.Combustion * lendParams.km / 100 * fuelPrice}) x {lendParams.carClass} + {resultYears} + {resultCount}";

        }
    }
}
