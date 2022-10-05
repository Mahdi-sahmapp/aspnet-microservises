using FluentValidation.Results;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredering.Application.Exceptions
{
    class ValidationException:ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; set; }
        public ValidationException():base("one or more Validation failures have occured")
        {
            Errors = new ConcurrentDictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures):this()
        {
            Errors = failures.GroupBy(a => a.PropertyName, e => e.ErrorMessage)
                .ToDictionary(b => b.Key, b => b.ToArray());
        }
    }
}
