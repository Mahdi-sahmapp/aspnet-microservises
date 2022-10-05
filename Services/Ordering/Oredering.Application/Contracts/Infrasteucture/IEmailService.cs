using Oredering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredering.Application.Contracts.Infrasteucture
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email Email);
    }
}
