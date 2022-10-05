using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredering.Application.Exceptions
{
   public  class NotFoundEx:ApplicationException
    {
        public NotFoundEx(string name, object key)
            :base($"Entity \"{name}\" and \"{key}|\" was not found")
        {
            
        }
    }
}
