using eBookStore.Api.Middlewares;
using eBookStore.Api.Service;
using eBookStore.Services.Interface;
using eBookStore.Services.Service;
using Microsoft.AspNetCore.OData;

namespace eBookStore.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPIServices(this IServiceCollection services)
        {
            services.AddControllers().AddOData(opt =>
            {
                opt.Filter().Select().OrderBy().SetMaxTop(100).Expand();
            });
            services.AddHttpContextAccessor();
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddScoped<ICurrentTime, CurrentTime>();
            return services;
        }
    }
}
