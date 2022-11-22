using AutoMapper;
using CarRent.Entites;
using CarRent.Models;

namespace CarRent
{
    public class CarMappingProfile: Profile
    {
        public CarMappingProfile()
        {
            CreateMap<CarRental, CarRentDto>();
            CreateMap<Car, CarDto>();
            CreateMap<ReservationParams, Customer>();
            CreateMap<ReservationParams, Rent>();
        }
    }
}
