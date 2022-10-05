using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!await orderContext.orders.AnyAsync())
            {
                await orderContext.orders.AddRangeAsync(GetPreConfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Data Seed Done!");
            }
        }

        public static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    FirstName = "Mahdi",
                    UserName = "Mahdi",
                    EmailAdress = "Mahdi@yahoo.com",
                    City = "Tehran",
                    Country = "IRan",
                    TotalPrice =10000,            
                }
            };
        }
    }
}
