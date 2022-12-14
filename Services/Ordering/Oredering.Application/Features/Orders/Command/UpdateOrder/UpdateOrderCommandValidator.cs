using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredering.Application.Features.Orders.Command.UpdateOrder
{
    class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(a => a.UserName).NotEmpty().WithMessage("UserName is required")
                 .NotNull().MaximumLength(50);

            RuleFor(a => a.EmailAdress).NotEmpty().WithMessage("Email is required")
                 .NotNull();

            RuleFor(a => a.TotalPrice)
                .NotEmpty().WithMessage("Total Price is required")
                .GreaterThan(0);
        }
    }
}
