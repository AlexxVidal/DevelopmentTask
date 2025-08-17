using FullStackDeveloperTask.Core.Business;
using FullStackDeveloperTask.Core.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace FullStackDeveloperTask.Core.Extensions
{
    public static class BusinessServiceCollectionExtension
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IForkliftBusiness, ForkliftBusiness>();
            services.AddScoped<ICommandParser, CommandParser>();

            return services;
        }
    }
}
