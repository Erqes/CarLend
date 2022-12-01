using AutoMapper;
using CarRent.DbContexts;
using CarRent.Entites;
using CarRent.Models;
using CarRent.Requests;
using CarRent.Requests.Enums;
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
        IEnumerable<CarDto> GetByParams(CarParams carParams);
    }

    public class CarRentService : ICarRentService
    {

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
            var fuelPrice = 8;
            var lendPrice = 100;
            var car = GetType(lendParams.carClass);
            if (car is null) return null;
            var CarsCount = GetCount();
            var howLong = DateTime.Now - lendParams.driveLicense;
            var floatHowLong = (float)howLong.TotalDays;
            var days = lendParams.to - lendParams.from;
            var howManyDays = days.TotalDays;
            var floatDays = (float)howManyDays;//ilosc dni w int 
            var result = car.Combustion * lendParams.km / 100 * fuelPrice + lendPrice * floatDays;

            var res = (int)lendParams.carClass switch
            {
                0 => result = result,
                10 => result = result * 1.3f,
                20 => result = result * 1.6f,
                30 => result = result * 2,
                _ => throw new InvalidOperationException()
            };
            float resultYears, resultCount;
            //jeśli prawo jazdy mniej niż 5 lat 
            if (5 * 365 > floatHowLong)
                resultYears = result + result * 0.2f;
            else
                resultYears = 0;
            //jesli aut jest mniej niż 3 
            if (CarsCount < 3)
                resultCount = result + result * 0.15f;
            else
                resultCount = 0;
            //jeśli prawo jazdy mniej niż 3 lata i klasa Premium
            if (3 * 365 > floatHowLong && lendParams.carClass.ToString() == "Premium")
                return "Nie można wypożyczyć samochodu";

            result = result + resultCount + resultYears;

            return $"Cena netto: {result}, Cena brutto: {result + result * 0.23}, Cena Całkowita= (Cena Wypożyczenia howManyDays ilość dni + koszt paliwa) howManyDays klasa samochodu" +
            $" + Koszt(jesli prawo jazdy posiadane jest mniej niż 5 lat) + Koszt(jeśli aut jest dostępnych mniej niż 3)=" +
            $"({100}howManyDays{floatDays}+{car.Combustion * lendParams.km / 100 * fuelPrice}) howManyDays {lendParams.carClass} + {resultYears} + {resultCount}";
        }
        public void reservation(ReservationParams reservationParams)
        {
        }
        public IEnumerable<CarRentDto> GetAll()
        {
            var cars = _dbContext.CarRents
                .Include(r => r.Cars)
                .ToList();
            var carsDtos = _mapper.Map<List<CarRentDto>>(cars);
            return carsDtos;
        }
        public CarDto MapCars(Car cars)
        {
            var carsDto = new CarDto();
            carsDto.id = cars.Id;
            carsDto.name = cars.Name;
            carsDto.price = cars.Price;
            carsDto.Class = cars.Class;
            carsDto.combustion = cars.Combustion;
            carsDto.localization = cars.Localization;
            carsDto.color = cars.Color;
            carsDto.horsePower = cars.HorsePower;
            return carsDto;
        }
        public IEnumerable<CarDto> GetByParams(CarParams carParams)
        {
            var cars = _dbContext.Cars.Where(c => c.Name == carParams.name)
                .Where(c => c.Color == carParams.color)
                .Where(c => c.Price <= carParams.priceTo)
                .Where(c => c.Price >= carParams.priceFrom)
                .Where(c => c.Combustion <= carParams.combustionTo)
                .Where(c => c.Combustion >= carParams.combustionFrom)
                .Where(c => c.HorsePower <= carParams.horsePowerTo)
                .Where(c => c.HorsePower >= carParams.horsePowerTo).ToList();

            var carsDto = cars.Select(c => new CarDto()
            {
                id = c.Id,
                name = c.Name,
                color = c.Color,
                price = c.Price,
                combustion = c.Combustion,
                localization = c.Localization,
                Class = c.Class,
                horsePower = c.HorsePower,
            }).ToList();

            if (carParams.priceSort == "descending")
                carsDto.Sort();
            else
                carsDto.Reverse();
            return carsDto;
        }
    }
}
