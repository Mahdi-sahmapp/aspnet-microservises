using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Oredering.Application.Exceptions.ValidationException;

namespace Oredering.Application.Behaviours
{
    public class ValidationBehaviours<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest :IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;

        public ValidationBehaviours(IEnumerable<IValidator<TRequest>> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResualt = await Task.WhenAll(_validator.Select(a => a.ValidateAsync(context, cancellationToken)));

                var failures = validationResualt.SelectMany(a => a.Errors).Where(a => a != null).ToList();

                if (failures.Any())
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
