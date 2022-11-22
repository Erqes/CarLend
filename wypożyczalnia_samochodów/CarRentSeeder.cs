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
                    cars=new List<Car>()
                    {
                        new Car()
                        {
                            Class="Premium",
                            name="BWM",
                            localization="Warszawa",
                            combustion=9f,
                            //from=new DateTime(2022,6,11),
                            //to=new DateTime(2022, 6, 19)
                        },
                         new Car()
                         {
                             Class="Medium",
                             name="Skoda",
                             localization="Kraków",
                             combustion=7f,
                         },
                         new Car()
                         {
                             Class="Standard",
                             name="Opel",
                             localization="Nowy Sącz",
                             combustion=6f,
                         },
                         new Car()
                         {
                             Class="Basic",
                             name="Fiat",
                             localization="Warszawa",
                             combustion=5f,
                         },
                         new Car()
                         {
                             Class="Basic",
                             name="Alfa",
                             localization="Warszawa",
                             combustion=5f,
                         }

                    },
                    employees=new List<Employee>()
                    {
                        new Employee()
                        {
                            name="Bogdan",
                            lastName="Brzęczyszczykiewicz",
                            email="os@gmail.com",
                            phone="888777999",

                        },
                    },

                },
            new CarRental()

        };
            return carRents;
        }
    }
}
