
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using Oredering.Application.Contracts.Infrasteucture;
using Oredering.Application.Contracts.Persistence;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegiteration
    {
        public static IServiceCollection AddInfrastructureSerivce(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
            });
            services.AddScoped(typeof(IAysyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
