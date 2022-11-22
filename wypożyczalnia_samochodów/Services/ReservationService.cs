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
using AutoMapper;

namespace CarRent.Services
{
    public interface IReservationService
    {
        int Reservation(ReservationParams reservationParams);
        bool CarReturn(int carId);
    }

    public class ReservationService : IReservationService
    {
        private readonly IConfiguration _configuration;
        private readonly CarRentDbContext _dbContext;
        private readonly IMapper _mapper;
        public ReservationService(IConfiguration configuration, CarRentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _mapper = mapper;
        }
        public int Reservation(ReservationParams reservationParams)
        {


            Car[] cars = new Car[reservationParams.carsId.Count];
            int[] noCars = new int[reservationParams.carsId.Count];
            for (int i = 0; i < reservationParams.carsId.Count; i++)
            {
                cars[i] = _dbContext.Cars.FirstOrDefault(c => c.id == reservationParams.carsId[i]);
                if (!cars[i].isCar)
                    return cars[i].id;
            }
            var employee = _dbContext.Employees.FirstOrDefault(e => e.customers.Count<3);
            var customer = _mapper.Map<Customer>(reservationParams);
            customer.employeeId=employee.id;
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            Rent[] rentsAdd = new Rent[cars.Length];
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i].isCar = false;
                rentsAdd[i] = _mapper.Map<Rent>(reservationParams);
                _dbContext.Rents.Add(rentsAdd[i]);
                rentsAdd[i].carId = cars[i].id;
                rentsAdd[i].customerId = customer.id;
            }
            _dbContext.SaveChanges();
            //for (int i = 0; i < cars.Length; i++)
            //{
            //    if ((reservationParams.to > cars[i].from || reservationParams.from < cars[i].to) && ((cars[i].to > new DateTime(2000, 01, 01) && (cars[i].from > new DateTime(2000, 01, 01)))))
            //    {
            //        return cars[i].id;
            //    }
            //}


            Send(reservationParams, _configuration.GetSection("EmailUserName").Value);
            Send(reservationParams, reservationParams.email);
            return -1;
        }
        public bool CarReturn(int carId)
        {
            var carReturn = _dbContext.Rents.FirstOrDefault(r => r.carId == carId);
            if (carReturn is null) { return false; }
            _dbContext.Rents.Remove(carReturn);
            _dbContext.SaveChanges();
            var isCar = _dbContext.Cars.FirstOrDefault(c => c.id == carId);
            isCar.isCar = true;
            _dbContext.SaveChanges();
            return true;
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
