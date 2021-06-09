using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Models;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        //Register Infrastructure Services
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Add db context
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectingString")));

            //Add Repository
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            //Add custom functions for Repository inheritence
            services.AddScoped<IOrderRepository, OrderRepository>(); //AddScoped is request life cycle

            //Configure Email Service and add Email pipeline
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings")); //Get settings data
            services.AddTransient<IEmailService, EmailService>(); //AddTransient is used for pipeline on startup

            return services;
        }
    }
}
