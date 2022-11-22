using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using CarRent.Services;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpPost("reserve")]
        public ActionResult Reservation([FromBody] ReservationParams reservationParams)
        {
            var result = _reservationService.Reservation(reservationParams);
            if (result == -1)
                return Ok(reservationParams);
            return BadRequest($"Nie można wypożyczyć auta o podanym id={result} w tym czasie");

        }
        [HttpDelete("{id}")]
        public ActionResult CarReturn([FromRoute] int id)
        {
            var isReturn = _reservationService.CarReturn(id);
            if (isReturn)
                return NoContent();
            return NotFound();
        }
    }
}
