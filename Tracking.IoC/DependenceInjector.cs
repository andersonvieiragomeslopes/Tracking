using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.BusinessLogicLayer.Blls;
using Tracking.DataAccessLayer;
using Tracking.DataAccessLayer.Dals;

namespace Tracking.IoC
{
    public class DependenceInjector
    {
        public static void Register(IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddDbContext<TrackingContext>();
            services.AddScoped<IUserDAL, UserDAL>();
            services.AddScoped<IUserBLL, UserBLL>();
        }
    }
}
