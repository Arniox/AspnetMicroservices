using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using System.Reflection;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        //Extenion Method
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Add all services
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); //Looks for custom Mapping Profiles (instance of Profile)
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); //Looks for any validator files (instances of AbstractValidator)
            services.AddMediatR(Assembly.GetExecutingAssembly()); //Looks for any Request files (instances of IRequest or IRequestHandler)

            //Add all pipeline behaviours
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
