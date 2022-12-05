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
        [HttpPost("count")]
        public async Task<string> Count([FromBody] LendParams lendParams)
        {
            var count =await _carRentService.CountBy(lendParams);
            return count;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetAll()
        {
            var cars = await _carRentService.GetAll();
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
