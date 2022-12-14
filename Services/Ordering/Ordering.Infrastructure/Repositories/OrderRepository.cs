using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using Oredering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) :base(dbContext)
        {

        }
        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            return await _dbContext.orders
                .Where(a => a.UserName == userName).ToListAsync();
        }
    }
}
