using MessageService.API.Data;
using MessageService.API.Business.Abstract;
using MessageService.API.Business.Concrete;
using MessageService.API.Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MessageService.API.Mapping;
using MessageService.API.HttpService;
using MessageService.API.Config;

namespace MessageService.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<MessageRepository>();
            services.AddScoped<LogRepository>();
            services.AddScoped<ReplyRepository>();

            services.AddScoped<IMessageService,MessageManager>();
            services.AddScoped<IReplyService, ReplyManager>();
            services.AddScoped<ILogService, LogManager>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IHttpService, MessageService.API.HttpService.HttpService>();

            return services;
        }

       /* public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:44342")
                        .AllowCredentials();
                });
            });

            return services;
        }*/

        public static IServiceCollection AddSqlDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MessageServiceDBContext>(options => options.UseMySql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static void AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var securityConfig = configuration.GetSection("Security").Get<SecurityConfig>();

            if (securityConfig == null)
                securityConfig = new SecurityConfig();

            services.AddSingleton(securityConfig);
        }


        public static IServiceCollection AddAutomapperConfiguration(this IServiceCollection services)
        {
            return services.AddAutoMapper(conf => conf.AddProfile(new AutoMapperProfile()));
        }


    }
}
