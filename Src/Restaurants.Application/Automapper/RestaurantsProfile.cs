using AutoMapper;
using Restaurants.Application.Commands.Restaurant.Create;
using Restaurants.Application.Commands.Restaurant.Update;
using Restaurants.Application.DTOs;
using Model = Restaurants.Domain.Model;

namespace Restaurants.Application.Automapper
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile()
        {
            CreateMap<UpdateRestaurantCommand, Model.Restaurant>();

            // this is for HttpPost Src: CreateRestaurantDto and Dest: Restaurant
            CreateMap<CreateRestaurantCommand, Model.Restaurant>()
                .ForMember(d => d.Address, opt => opt.MapFrom(
                    src => new  Model.Address
                    {
                        City = src.City,
                        PostalCode = src.PostalCode,
                        Street = src.Street,
                    }));


            // this is for HttpGet Src: Restaurant and Dest: RestaurantDto
            CreateMap<Model.Restaurant, RestaurantDto>()
                .ForMember(d => d.City, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                 .ForMember(d => d.Street, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                  .ForMember(d => d.PostalCode, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                  .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));
        }
    }
}
