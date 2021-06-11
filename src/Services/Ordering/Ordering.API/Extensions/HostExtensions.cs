using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        //Migrate database host extension
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder, int? retry = 0)
            where TContext : DbContext
        {
            //Retry a number of times
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                //Get services, logger and context
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                //Try catch to connect
                try
                {
                    //Migrating
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);
                    InvokeSeeder(seeder, context, services); //Run on each try of migration

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch(SqlException ex)
                {
                    //Try again up to retry amount
                    logger.LogError("--------------------------------------------Retry Count: {RetryCount}---------------------------------------\n\n", retryForAvailability);
                    logger.LogError(ex, "An error occurred while migrating the databased associated with context {DbContextName}", typeof(TContext).Name);
                    logger.LogError("\n\n------------------------------------------------------------------------------------------------------------");

                    //Retry
                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailability); //Migrate again
                    }
                }
            }
            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
            where TContext : DbContext
        {
            //Migrate database
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
