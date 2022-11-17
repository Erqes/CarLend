using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using CarRent.Entites;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CarRent.Services
{
    public interface IReservationService
    {
        int Reservation(ReservationParams reservationParams);
    }

    public class ReservationService : IReservationService
    {
        private readonly IConfiguration _configuration;
        private readonly CarRentDbContext _dbContext;
        public ReservationService(IConfiguration configuration, CarRentDbContext dbContext)
        {
            _dbContext= dbContext;
            _configuration = configuration;
        }
        public int Reservation(ReservationParams reservationParams)
        {
            Car[] cars= new Car[reservationParams.id.Count];
            for(int i=0; i<reservationParams.id.Count;i++)
            {
                cars[i] = _dbContext.Cars.FirstOrDefault(c => c.Id == reservationParams.id[i]);
            }
            //for(int i=0; i < cars.Length; i++)
            //{
            //    if (Datetime.Parse.(reservationParams.to) > cars[i].From || reservationParams.from < cars[i].To)
            //    {
            //        return cars[i].Id;
            //    }
            //}           
            Send(reservationParams, _configuration.GetSection("EmailUserName").Value);
            Send(reservationParams, reservationParams.email);
            return -1;
        }
        public void Send(ReservationParams reservationParams, string emailTo)
        {
            var result = reservationParams.ToString();
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(emailTo));
            if (email.From == email.To)
            {
                email.Subject = $"Rezerwacja od {reservationParams.email}";
                email.Body = new TextPart(TextFormat.Html) { Text = $"{result}" };
            }
            else
            {
                email.Subject = $"Rezerwacja Car Rental dokonana.";
                email.Body = new TextPart(TextFormat.Html) { Text = $"Dokonano rezerwacji samochodu." };
            }

            // email.Body = new TextPart(TextFormat.Html) { Text = body };
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
