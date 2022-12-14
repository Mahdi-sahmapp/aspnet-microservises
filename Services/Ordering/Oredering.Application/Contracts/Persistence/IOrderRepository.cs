using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oredering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAysyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
