using FullStackDeveloperTask.Core.Readers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackDeveloperTask.Core.Extensions
{
    public static  class ReadersServiceCollectionExtension
    {
        public static IServiceCollection AddReaders(this IServiceCollection services)
        {
            services.AddSingleton<CsvForkliftReader>();
            services.AddSingleton<JsonForkliftReader>();
            services.AddSingleton<ForkliftFileReader>();

            return services;
        }
    }
}
