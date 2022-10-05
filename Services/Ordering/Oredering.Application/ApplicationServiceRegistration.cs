using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Oredering.Application.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oredering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            Services.AddMediatR(Assembly.GetExecutingAssembly());

            Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(UnHandledExceptionBehavior<,>));
            Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviours<,>));

            return Services;
        }
    }
}
