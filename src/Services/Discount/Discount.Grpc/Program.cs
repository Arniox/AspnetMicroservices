using Discount.Grpc.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Discount.Grpc
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

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
