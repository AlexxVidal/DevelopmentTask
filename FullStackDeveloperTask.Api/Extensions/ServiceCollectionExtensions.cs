using FullStackDeveloperTask.Api.Services;

namespace FullStackDeveloperTask.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IForkliftService, ForkliftService>();

        return services;
    }
}
