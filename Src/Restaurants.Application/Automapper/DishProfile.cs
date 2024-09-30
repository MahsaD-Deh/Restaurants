using AutoMapper;
using Restaurants.Application.Commands.Dish.Create;
using Restaurants.Application.DTOs;
using Model = Restaurants.Domain.Model;
namespace Restaurants.Application.Automapper
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<CreateDishCommand, Model.Dish>();
            CreateMap<Model.Dish, DishDto>();
        }
    }
}
