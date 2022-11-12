using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Entites;

namespace CarRent
{
    public class CarRentSeeder
    {
        private readonly CarRentDbContext _dbContext;
        public CarRentSeeder(CarRentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.CarRents.Any())
                {
                    var carRents = GetCarRents();
                    _dbContext.CarRents.AddRange(carRents);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<CarRental> GetCarRents()
        {
            var carRents = new List<CarRental>()
            {
                new CarRental()
                {
                    Cars=new List<Car>()
                    {
                        new Car()
                        {
                            Class="Premium",
                            Name="BWM",
                            Localization="Warszawa",
                            Combustion=9f,
                        },
                         new Car()
                         {
                             Class="Medium",
                             Name="Skoda",
                             Localization="Kraków",
                             Combustion=7f,
                         },
                         new Car()
                         {
                             Class="Standard",
                             Name="Opel",
                             Localization="Nowy Sącz",
                             Combustion=6f,
                         },
                         new Car()
                         {
                             Class="Basic",
                             Name="Fiat",
                             Localization="Warszawa",
                             Combustion=5f,
                         },

                    },

                },
            new CarRental()

        };
            return carRents;
        }
    }
}
