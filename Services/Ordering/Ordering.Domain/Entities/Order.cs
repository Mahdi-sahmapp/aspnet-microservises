using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public class Order:EntityBase
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string EmailAdress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public string BankName { get; set; }
        public string RefCode { get; set; }
        public int PaymentMethod { get; set; }
    }
}
