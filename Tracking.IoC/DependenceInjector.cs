using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using Tracking.BusinessLogicLayer.Blls;
using Tracking.BusinessLogicLayer.Hubs;
using Tracking.BusinessLogicLayer.Services;
using Tracking.DataAccessLayer;
using Tracking.DataAccessLayer.Dals;

namespace Tracking.IoC
{
    public class DependenceInjector
    {
        public static void Register(IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddDbContext<TrackingContext>();
            services.AddSignalR();
            //https://www.milanjovanovic.tech/blog/how-to-use-rate-limiting-in-aspnet-core

            services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.AddFixedWindowLimiter("login", options =>
                {
                    options.PermitLimit = 3;
                    options.Window = TimeSpan.FromSeconds(10);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 2;
                });
                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            //https://learn.microsoft.com/pt-br/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddHttpClient<IDrivingService, DrivingService>(client =>
            {
                client.BaseAddress = new Uri(configuration["osrmApi"]);
            }); 
            services.AddHttpClient<ISearchAddressService, SearchAddressService>(client =>
            {
                client.BaseAddress = new Uri(configuration["addressApi"]);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtKey"]!)),
                        ClockSkew = TimeSpan.Zero
                    });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<OrderHub>();
            services.AddScoped<IAuthBLL, AuthBLL>();

            services.AddScoped<IOrderDAL, OrderDAL>();
            services.AddScoped<IOrderBLL, OrderBLL>();            
            
            services.AddScoped<IUserDAL, UserDAL>();
            services.AddScoped<IUserBLL, UserBLL>();
        }
    }
}
