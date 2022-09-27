using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Api.Extention
{
    public static class HostExtentions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host,int? retry=0)
        {
            // مربوط به جلسات 26 و 27 بود ادامه ندام
            int retryforAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService < ILogger<TContext>>();

                // migrate database
                try
                {
                    logger.LogInformation("migrating PostgreSql database");

                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DataBaseSetting:Connectionstring"));
                    connection.Open();


                }
                catch (Exception)
                {

                    throw;
                }
            }

            return host;
        }
    }
}
