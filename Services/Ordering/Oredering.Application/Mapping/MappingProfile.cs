using AutoMapper;
using Ordering.Domain.Entities;
using Oredering.Application.Features.Orders.Command.CheckoutOrder;
using Oredering.Application.Features.Orders.Command.UpdateOrder;
using Oredering.Application.Features.Orders.Queries.GetOrderList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredering.Application.Mapping
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
            CreateMap<Order, CheckoutOrderCammand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
