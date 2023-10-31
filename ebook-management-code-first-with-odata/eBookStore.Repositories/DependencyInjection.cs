using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBookStore.Repositories.Data;
using eBookStore.Repositories.Repository;
using eBookStore.Services.Interface;
using eBookStore.Services.InterfaceRepo;
using eBookStore.Services.InterfaceSerivce;
using eBookStore.Services.Mapper;
using eBookStore.Services.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eBookStore.Repositories
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, string dbConnection)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(dbConnection));
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);

            #region  DI Repositories
            services.AddScoped<IAuthorRepository, AuthorRepository>()
                    .AddScoped<IBookRepository, BookRepository>()
                    .AddScoped<IBookAuthorRepository, BookAuthorRepository>()
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IRoleRepository, RoleRepository>()
                    .AddScoped<IPublisherRepository, PublisherRepository>()
                    .AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region DI Services
            services.AddScoped<IAuthorService, AuthorService>()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<IRoleService, RoleService>()
                    .AddScoped<IBookService, BookService>()
                    .AddScoped<IPublisherService, PublisherService>()
                    .AddScoped<IBookAuthorService, BookAuthorService>();
            #endregion
            return services;
        }
    }
}
