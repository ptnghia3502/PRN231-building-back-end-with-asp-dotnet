using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eBookStore.Domains.Models;
using eBookStore.Services.ViewModels;

namespace eBookStore.Services.Mapper
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<RoleViewModel, Role>().ReverseMap();
            CreateMap<RoleCreateModel, Role>().ReverseMap();

            CreateMap<UserCreateModel, User>().ReverseMap();
            CreateMap<UserUpdateModel, User>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();

            CreateMap<AuthorViewModel, Author>().ReverseMap();
            CreateMap<AuthorCreateModel, Author>().ReverseMap();
            CreateMap<AuthorUpdateModel, Author>().ReverseMap();

            CreateMap<PublisherViewModel, Publisher>().ReverseMap();
            CreateMap<PublisherUpdateModel, Publisher>().ReverseMap();
            CreateMap<PublisherCreateModel, Publisher>().ReverseMap();

            CreateMap<BookViewModel, Book>().ReverseMap();
            CreateMap<BookCreateModel, Book>().ReverseMap();
            CreateMap<BookUpdateModel, Book>().ReverseMap();

            CreateMap<BookAuthorViewModel, BookAuthor>().ReverseMap();
            CreateMap<BookAuthorUpdateModel, BookAuthor>().ReverseMap();
            CreateMap<BookAuthorCreateModel, BookAuthor>().ReverseMap();
        }
    }
}
