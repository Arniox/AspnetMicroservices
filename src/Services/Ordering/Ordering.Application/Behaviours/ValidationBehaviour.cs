using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        //Get all validators for validation behaviour.
        //Two current validation files = UpdateOrderCommandValidation & DeleteOrderCOmmandValidation
        private readonly IEnumerable<IValidator<TResponse>> _validators;

        //Constructor
        public ValidationBehaviour(IEnumerable<IValidator<TResponse>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        //Handle
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            //Check if any validators exist
            if (_validators.Any())
            {
                //Perform Validation Handles
                var context = new ValidationContext<TRequest>(request);
                //Get all validators, and one by one, validate all context's with set token.
                //await all validators and get all results
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList(); //Get all errors

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            //Move to next pipeline
            return await next();
        }
    }
}
