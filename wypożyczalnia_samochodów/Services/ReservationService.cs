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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Services
{
    public interface IReservationService
    {
        Task<string> Reservation(ReservationParams reservationParams);
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
        public async Task<Customer> MapCustomer(ReservationParams reservationParams)
        {
            var customer = new Customer();
            if (reservationParams != null)
            {
                customer.Name = reservationParams.name;
                customer.LastName = reservationParams.lastName;
                customer.Email = reservationParams.email;
                customer.Phone = reservationParams.phone;
                customer.Address = reservationParams.address;
                customer.PostalCode = reservationParams.postalCode;
            }
            return customer;
        }
        public async Task<Rent> MapRent(ReservationParams reservationParams)
        {
            var rent = new Rent();
            if (reservationParams != null)
            {
                rent.From = reservationParams.from;
                rent.To = reservationParams.to;
            }
            return rent;
        }
        public async Task<string> Reservation(ReservationParams reservationParams)//dodać uniklanych klientów 
        {
            List<Car> cars = new List<Car>();
            for (int i = 0; i < reservationParams.carsId.Count; i++)
            {
                cars.Add(_dbContext.Cars.FirstOrDefault(c => c.Id == reservationParams.carsId[i]));
                if (!cars[i].IsCar)
                    return $"nie można wypożyczyć auta o podanym id";
            }
            var employee = await _dbContext.Employees.OrderBy(e => e.Customers.Count).FirstOrDefaultAsync();
            var customer = await MapCustomer(reservationParams);

            customer.EmployeeId = employee.Id;
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            List<Rent> rents = new List<Rent>();
            for (int i = 0; i < cars.Count; i++)
            {

                cars[i].IsCar = false;
                rents.Add(await MapRent(reservationParams));
                await _dbContext.Rents.AddAsync(rents[i]);
                rents[i].CarId = cars[i].Id;
                rents[i].CustomerId = customer.Id;

            }

            await _dbContext.SaveChangesAsync();
            await Send(reservationParams, _configuration.GetSection("EmailUserName").Value);
            await Send(reservationParams, reservationParams.email);
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

        public async Task Send(ReservationParams reservationParams, string emailTo)
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
            await Task.Run(() => smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls));
            await Task.Run(() => smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value));
            await Task.Run(() => smtp.Send(email));
            await Task.Run(() => smtp.Disconnect(true));
        }
    }
}
