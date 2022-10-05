using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQuery:IRequest<List<OrdersVm>>        
    {
        public string UserName { get; set; }
        public GetOrderListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
