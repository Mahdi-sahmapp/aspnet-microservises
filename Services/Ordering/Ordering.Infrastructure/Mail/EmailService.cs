using Oredering.Application.Contracts.Infrasteucture;
using Oredering.Application.Models;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    class EmailService : IEmailService
    {
        public async Task<bool> SendEmail(Email Email)
        {
            return true;
        }
    }
}
