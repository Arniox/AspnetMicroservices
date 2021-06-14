using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ordering.API.EventBusConsumer;
using Ordering.Application;
using Ordering.Infrastructure;

namespace Ordering.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add services for business layer (Ordering.Application)
            services.AddApplicationServices();
            //Add services for infrastructure layer (Ordering.Infrastructure)
            services.AddInfrastructureServices(Configuration);

            //Add and Configure MassTransit
            services.AddMassTransit(configuration => {
                //Consume basket RabbitMQ
                //Set to subscriber of RabbitMQ Event
                configuration.AddConsumer<BasketCheckoutConsumer>();

                //Configure MassTransit with RabbitMQ
                configuration.UsingRabbitMq((context, config) =>
                {
                    //Configure RabbitMQ with Host
                    config.Host(Configuration.GetValue<string>("EventBusSettings:HostAddress"));

                    //Consume EventBus RabbitMQ.
                    config.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
                    {
                        //Endpoint being consumed
                        c.ConfigureConsumer<BasketCheckoutConsumer>(context);
                    });
                });
            });

            //Add MassTransit Hosted Services
            //Provided to MassTransit as hosted service
            services.AddMassTransitHostedService();

            //Register AutoMapper
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<BasketCheckoutConsumer>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.API", Version = "v1" });
            });

            //General config
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
