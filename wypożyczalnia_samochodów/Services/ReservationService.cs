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
using CarRent.DbContexts;
using CarRent.Requests;

namespace CarRent.Services
{
    public interface IReservationService
    {
        string Reservation(ReservationParams reservationParams);
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
        public Customer MapCustomer(ReservationParams reservationParams)
        {
            var customer = new Customer();
            if(reservationParams != null)
            {
                customer.Name = reservationParams.name;
                customer.LastName = reservationParams.lastName;
                customer.Email = reservationParams.email;
                customer.Phone = reservationParams.phone;
                customer.Address = reservationParams.address;
                customer.PostalCode= reservationParams.postalCode;
            }
            return customer;
        }
        public Rent MapRent(ReservationParams reservationParams)
        {
            var rent = new Rent();
            if(reservationParams != null)
            {
                rent.From = reservationParams.from;
                rent.To = reservationParams.to;
            }
            return rent;
        }
        public string Reservation(ReservationParams reservationParams)//dodać uniklanych klientów 
        {
            Car[] cars = new Car[reservationParams.carsId.Count];
            int[] noCars = new int[reservationParams.carsId.Count];
            for (int i = 0; i < reservationParams.carsId.Count; i++)
            {
                cars[i] = _dbContext.Cars.FirstOrDefault(c => c.Id == reservationParams.carsId[i]);
                if (!cars[i].IsCar)
                    return $"nie można wypożyczyć auta o podanym id";
            }
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Customers.Count < 10);
            var customer= MapCustomer(reservationParams);
            //var customer = _mapper.Map<Customer>(reservationParams);
            customer.EmployeeId = employee.Id;
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            Rent[] rentsAdd = new Rent[cars.Length];
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i].IsCar = false;
                rentsAdd[i] = MapRent(reservationParams);
                _dbContext.Rents.Add(rentsAdd[i]);
                rentsAdd[i].CarId = cars[i].Id;
                rentsAdd[i].CustomerId = customer.Id;
            }
            _dbContext.SaveChanges();
            //for (int i = 0; i < Cars.Length; i++)
            //{
            //    if ((reservationParams.To > Cars[i].From || reservationParams.From < Cars[i].To) && ((Cars[i].To > new DateTime(2000, 01, 01) && (Cars[i].From > new DateTime(2000, 01, 01)))))
            //    {
            //        return Cars[i].Id;
            //    }
            //}
            Send(reservationParams, _configuration.GetSection("EmailUserName").Value);
            Send(reservationParams, reservationParams.email);
            return "Zarezerwowano";
        }
        public bool CarReturn(int carId)
        {
            var carReturn = _dbContext.Rents.FirstOrDefault(r => r.CarId == carId);
            if (carReturn is null) { return false; }
            _dbContext.Rents.Remove(carReturn);
            _dbContext.SaveChanges();
            var isCar = _dbContext.Cars.FirstOrDefault(c => c.Id == carId);
            isCar.IsCar = true;
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
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
