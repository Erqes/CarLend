using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarRent.Entites;
using CarRent.Models;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using CarRent.Services;
using CarRent.Requests;

namespace CarRent.Controllers
{

    [ApiController]
    [Route("rents")]
    public class RentController : ControllerBase
    {
        private readonly ICarRentService _carRentService;
        public RentController(ICarRentService carRentService)
        {
            _carRentService = carRentService;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Car>> GetAll()
        //{
        //    var Cars = _dbContext.Cars.ToList();
        //    var carsDtos = Cars.Select(c => new Car()
        //    {
        //        Class = c.Class,
        //        Combustion = c.Combustion,
        //    });
        //    return Ok(Cars);
        //}

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        //[HttpPost("przywitaj")]
        //public string Hello([FromBody] string Name)
        //{
        //    return $"Hello {Name}";
        //}
        [HttpPost("count")]
        public async Task<string> Count([FromBody] LendParams lendParams)
        {
            var count =await Task.Run(()=> _carRentService.CountBy(lendParams));
            return count;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CarRentDto>> GetAll()
        {
            var cars = _carRentService.GetAll();
            if (cars == null)
                return NotFound();
            return Ok(cars);
        }
        [HttpGet("byparams")]
        public ActionResult<IEnumerable<CarRentDto>> GetByParams([FromBody] CarParams carParams)
        {
            // filtruj po:
            // do wyboru kolor, spalanie od-do, cena od-do, moc od-do, nazwa
            //sortuj po cenie od nw, od nm
            var cars=_carRentService.GetByParams(carParams);
            return Ok(cars);
        }
        
    }
}
