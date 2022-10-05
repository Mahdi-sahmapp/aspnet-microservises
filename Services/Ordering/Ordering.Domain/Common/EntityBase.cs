using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string LastModifyBy { get; set; }
        public DateTime? MdifiedDate { get; set; }
    }
}
