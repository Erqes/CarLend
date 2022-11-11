using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wypożyczalnia_samochodów.Entites;

namespace wypożyczalnia_samochodów.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WypozyczController : ControllerBase
    {
        float fuelPrice = 8;
        float lendPrice = 100;
        private readonly WypozyczalniaDbContext _dbContext;
        public WypozyczController(WypozyczalniaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
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

        [HttpPost("wylicz")]
        public string Wylicz([FromBody] LendParams lendParams)
        {
            var car = _dbContext.Cars.FirstOrDefault(c => c.Class == lendParams.carClass.ToString());
            var CarsCount = _dbContext.Cars.Count();
            var howLong = DateTime.Now - lendParams.driveLicense;
            var fhowLong = (float)howLong.TotalDays;
            var days = lendParams.to - lendParams.from;
            var x = days.TotalDays;
            var fdays = (float)x;//ilosc dni w int 
            var result = (car.Combustion * lendParams.km / 100 * fuelPrice + lendPrice * fdays);
            switch ((int)lendParams.carClass)
            {
                case 0:
                    result = result;
                    break;
                case 10:
                    result = result * 1.3f;
                    break;
                case 20:
                    result = result * 1.6f;
                    break;
                case 30:
                    result = result * 2;
                    break;
            }
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
