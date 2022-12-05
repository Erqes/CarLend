using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using CarRent.Services;
using CarRent.Requests;
using System.Threading.Tasks;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpPost("reserve")]
        public async Task<string> Reservation([FromBody] ReservationParams reservationParams)
        {
            var result =await _reservationService.Reservation(reservationParams);
            return result;
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> CarReturn([FromRoute] int id)
        {
            var returnedSuccessfully = await _reservationService.CarReturn(id);
            if (returnedSuccessfully)
                return NoContent();
            return NotFound();
        }
    }
}
