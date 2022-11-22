using AutoMapper;
using CarRent.Entites;
using CarRent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CarRent.Services
{
    public interface ICarRentService
    {
        string CountBy(LendParams lendParams);
        IEnumerable<CarRentDto> GetAll();
    }

    public class CarRentService : ICarRentService
    {
        float fuelPrice = 8;
        float lendPrice = 100;
        private readonly CarRentDbContext _dbContext;
        private readonly IMapper _mapper;
        public CarRentService(CarRentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public Car GetType(CarType carClass)
        {
            return _dbContext.Cars.FirstOrDefault(c => c.Class == carClass.ToString());
        }
        public int GetCount()
        {

            return _dbContext.Cars.Count();
        }
        public string CountBy(LendParams lendParams)
        {
            var car = GetType(lendParams.carClass);
            if (car is null) return null;
            var CarsCount = GetCount();
            var howLong = DateTime.Now - lendParams.driveLicense;
            var fhowLong = (float)howLong.TotalDays;
            var days = lendParams.to - lendParams.from;
            var x = days.TotalDays;
            var fdays = (float)x;//ilosc dni w int 
            var result = car.combustion * lendParams.km / 100 * fuelPrice + lendPrice * fdays;

            var res = (int)lendParams.carClass switch
            {
                0 => result = result,
                10 => result = result * 1.3f,
                20 => result = result * 1.6f,
                30 => result = result * 2

            };
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
            if (3 * 365 > fhowLong && lendParams.carClass.ToString() == "Premium")
                return "Nie można wypożyczyć samochodu";

            result = result + resultCount + resultYears;

            return $"Cena netto: {result}, Cena brutto: {result + result * 0.23}, Cena Całkowita= (Cena Wypożyczenia x ilość dni + koszt paliwa) x klasa samochodu" +
            $" + Koszt(jesli prawo jazdy posiadane jest mniej niż 5 lat) + Koszt(jeśli aut jest dostępnych mniej niż 3)=" +
            $"({100}x{fdays}+{car.combustion * lendParams.km / 100 * fuelPrice}) x {lendParams.carClass} + {resultYears} + {resultCount}";
        }
        public void reservation(ReservationParams reservationParams)
        {
        }
        public IEnumerable<CarRentDto> GetAll()
        {
            var cars = _dbContext.CarRents
                .Include(r => r.cars)
                .ToList();
            var carsDtos = _mapper.Map<List<CarRentDto>>(cars);
            return carsDtos;
        }
    }
}
