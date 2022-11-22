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

namespace CarRent.Controllers
{

    [ApiController]
    [Route("[controller]")]
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
        //    var cars = _dbContext.cars.ToList();
        //    var carsDtos = cars.Select(c => new Car()
        //    {
        //        Class = c.Class,
        //        combustion = c.combustion,
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
        [HttpPost("count")]
        public string Count([FromBody] LendParams lendParams)
        {
            var count = _carRentService.CountBy(lendParams);
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
        
    }
}
