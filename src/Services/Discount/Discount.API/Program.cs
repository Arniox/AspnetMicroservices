using Discount.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Discount.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Return IHostBuilder object
            var host = CreateHostBuilder(args).Build();
            host.MigrateDatabase<Program>(); //Migrate database for any missing tables/data
            host.Run(); //Run
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
