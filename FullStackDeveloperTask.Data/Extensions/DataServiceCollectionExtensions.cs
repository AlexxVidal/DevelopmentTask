using FullStackDeveloperTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FullStackDeveloperTask.Data.Extensions
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RoboticsContext>(opt =>
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

             return services;
        }
    }
}
