using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repositories.Models;
using Services.ViewModels;

namespace Services.Mapper
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<MemberView, Member>().ReverseMap();
            CreateMap<MemberCreateView, Member>().ReverseMap();
            CreateMap<Member, MemberUpdateView>().ReverseMap();

            CreateMap<ProductView, Product>().ReverseMap();
            CreateMap<ProductCreateView, Product>().ReverseMap();
            CreateMap<Product, ProductUpdateView>().ReverseMap();

            CreateMap<CategoryView, Category>().ReverseMap();

            CreateMap<OrderView, Order>().ReverseMap();
            CreateMap<OrderCreateView, Order>().ReverseMap();
            CreateMap<Order, OrderUpdateView>().ReverseMap();

            CreateMap<OrderDetailView, OrderDetail>().ReverseMap();

            CreateMap<LoginView, Member>().ReverseMap();
        }
    }
}
