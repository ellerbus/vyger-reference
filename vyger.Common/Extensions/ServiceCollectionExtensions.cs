using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using vyger.Common.Repositories;

namespace vyger.Common
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDatabaseContext>(opt => opt.UseLoggerFactory(MyLoggerFactory));
            services.AddDbContext<ApplicationDatabaseContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        //public static readonly LoggerFactory MyLoggerFactory
        //    = new LoggerFactory(new[] { new ConsoleLoggerProvider((a, b) => true, true) });
    }
}
