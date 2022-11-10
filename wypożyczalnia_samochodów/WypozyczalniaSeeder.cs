using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wypożyczalnia_samochodów.Entites;

namespace wypożyczalnia_samochodów
{
    public class WypozyczalniaSeeder
    {
        private readonly WypozyczalniaDbContext _dbContext;
        public WypozyczalniaSeeder(WypozyczalniaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Wypozyczalnie.Any())
                {
                    var wypozyczalnie = GetWypozyczalnie();
                    _dbContext.Wypozyczalnie.AddRange(wypozyczalnie);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Wypozyczalnia> GetWypozyczalnie()
        {
            var wypozyczalnie = new List<Wypozyczalnia>()
            {
                new Wypozyczalnia()
                {
                    CarCount=4,
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
            new Wypozyczalnia()

        };
        return wypozyczalnie;
        }
    }
}
