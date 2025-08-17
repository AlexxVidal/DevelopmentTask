using FullStackDeveloperTask.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDeveloperTask.Core.Extensions
{
    public static class CoreServiceCollectionExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddReaders();
            services.AddBusiness();

            services.AddScoped<IRoboticsContext>(sp => sp.GetRequiredService<RoboticsContext>());
            return services;
        }
    }
}
