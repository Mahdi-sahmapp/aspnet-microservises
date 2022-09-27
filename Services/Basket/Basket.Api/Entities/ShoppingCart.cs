using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                if (Items!= null && Items.Any())                
                    foreach (ShoppingCartItem item in Items)
                    {
                        totalprice += item.Price * item.Quantity;
                    }

                return totalprice;
            }
        }

        public ShoppingCart()
        {

        }

        public ShoppingCart(string username)
        {
            UserName = username;
        }
    }
}
