using AutoMapper;
using Backend_Task.Entities;
using Backend_Task.Models.Product;
using Backend_Task.Models.User;

namespace Backend_Task.Configurations
{
    public class AutomapperConfiguration:Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<User, GetUser>().ReverseMap();
            CreateMap<User, CreateUser>().ReverseMap();
            CreateMap<User, UpdateUser>().ReverseMap();

            CreateMap<Product, GetProduct>().ReverseMap();
            CreateMap<Product, CreateProduct>().ReverseMap();
            CreateMap<Product, UpdateProduct>().ReverseMap();
        }
    }
}
