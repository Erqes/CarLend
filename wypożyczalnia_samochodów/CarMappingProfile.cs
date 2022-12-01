using AutoMapper;
using CarRent.Entites;
using CarRent.Models;
using CarRent.Requests;

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
