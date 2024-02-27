using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public class ShoppingListProfileInfrastructure : Profile
    {
        public ShoppingListProfileInfrastructure()
        {
            CreateMap<ShoppingList, ShoppingListDocument>().ReverseMap();
            CreateMap<Product, ProductDocument>().ReverseMap();
        }
    }
}
